using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;
using System.Linq.Expressions;

namespace BaWuClub.Web.Controllers
{
    public class DownloadController : BaseController
    {
        #region
        private ClubEntities club;
        private DateTime today = DateTime.Now.Date;
        private int current = 1;
        #endregion

        #region get
        public ActionResult Index(string sort)
        {
            List<ViewDocument> viewDocuments = new List<ViewDocument>();
            Expression<Func<ViewDocument, string>> expression = GetExpression(sort);
            using (club = new ClubEntities()){
                IQueryable<ViewDocument> queryable = club.ViewDocuments.Where(d => d.Status == (int)State.Enable);
                viewDocuments = GetViewDocuments(queryable, expression, null, current);
                ViewBag.docCount = queryable.Count();
                ViewBag.todayDocCount = queryable.Where(v => v.VarDate >= today).Count();
                ViewBag.pageStr = HtmlCommon.GetPageStrPro("/download/p?sort=" + (sort == null ? "" : sort) + "&id=", ClubConst.WebPageSize, current, ViewBag.docCount, ClubConst.WebPageShow);
            }
            return View(viewDocuments);
        }

        public ActionResult Search(string keyword,string sort,int? id) {
            current = id ?? 1;
            List<ViewDocument> viewDocuments = new List<ViewDocument>();
            Expression<Func<ViewDocument, string>> expression = GetExpression(sort);
            using (club = new ClubEntities()){
                ViewBag.keyword = keyword;
                IQueryable<ViewDocument> queryable = club.ViewDocuments.Where(d => d.Status == (int)State.Enable);
                ViewBag.todayDocCount = queryable.Where(v => v.VarDate >= today).Count();
                ViewBag.docCount = queryable.Count();
                queryable = (!string.IsNullOrEmpty(keyword)) ? queryable.Where(v => v.Tags.Contains(keyword)) : queryable;
                ViewBag.searchCount = queryable.Count();
                viewDocuments = GetViewDocuments(queryable, expression, keyword, current);
                ViewBag.pageStr = HtmlCommon.GetPageStrPro("/download/search?keyword=" + keyword + "&sort=" + (sort == null ? "" : sort) + "&id=", ClubConst.WebPageSize, current, ViewBag.searchCount, ClubConst.WebPageShow);
            }
            return View("~/views/download/index.cshtml",viewDocuments);
        }

        [Authorize]
        public ActionResult UploadDocs()
        {
            using (club = new ClubEntities()) {
                var shared = club.SystemArticles.Where(s => s.Variables == "sys-info-shared" && s.Status == 1).FirstOrDefault();
                ViewBag.SharedDesc = shared != null ? shared.Text : "";
            }
            return View("~/views/download/uploaddocs.cshtml");
        }

        public ActionResult P(int? id,string sort) {
            int current = id ?? 1;
            ViewBag.page = current;
            List<ViewDocument> viewDocuments = new List<ViewDocument>();
            Expression<Func<ViewDocument, string>> expression = GetExpression(sort);
            using (club = new ClubEntities()) {
                IQueryable<ViewDocument> queryable = club.ViewDocuments.Where(d => d.Status == (int)State.Enable);
                viewDocuments = GetViewDocuments(queryable, expression, "", current);
                ViewBag.todayDocCount = queryable.Where(v => v.VarDate >= today).Count();
                ViewBag.docCount = queryable.Count();
            }
            ViewBag.pageStr = HtmlCommon.GetPageStrPro("/download/p?sort=" + (sort == null ? "" : sort) + "&id=", ClubConst.WebPageSize, current, ViewBag.docCount, ClubConst.WebPageShow);
            return View("~/views/download/index.cshtml", viewDocuments);
        }

        public ActionResult Item(int id) {
            ViewDocument viewDoc = new ViewDocument();
            using (club = new ClubEntities()) {
                viewDoc = club.ViewDocuments.Where(v => v.Id == id).FirstOrDefault();
               var doc=club.Documents.Where(v => v.Id == id).FirstOrDefault();
                if(doc!=null){
                    doc.Views += 1;
                    club.SaveChanges();
                }
            }
            if (viewDoc == null)
                return RedirectToAction("notfound","error");
            return View("~/views/download/show.cshtml",viewDoc);
        }

        public ActionResult DownloadFiles(int id) {
            string files = string.Empty;
            using (club = new ClubEntities()) {
               var doc=club.Documents.Where(v => v.Id == id).FirstOrDefault();
                if(doc!=null){
                    doc.Downs += 1;
                    files = doc.Url;
                    club.SaveChanges();
                }
            }
            return Redirect( "/uploads/files/"+files);
        }
        #endregion

        #region post
        [Authorize]
        [HttpPost]
        public ActionResult Uploads(string title,int? type,string tags,string file,string context) {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(tags) || string.IsNullOrEmpty(file) || string.IsNullOrEmpty(context))
            {
                ViewBag.hitStr = "请填写要上传文件的信息完整";
            }
            else {
                int tmpId = type ?? 0;
                BaWuClub.Web.Dal.User user = GetUser();
                Document doc = new Document()
                {
                    Title = HtmlCommon.ClearHtml(title),
                    Tags = HtmlCommon.ClearHtml(tags),
                    Url = file,
                    Description = HtmlCommon.ClearHtml(context),
                    UserId = user.Id,
                    Status = 0,
                    Type=(byte)type,
                    VarDate=DateTime.Now,
                };
                using (club = new ClubEntities())
                {
                    club.Documents.Add(doc);
                    if (club.SaveChanges() >= 0){
                        return RedirectToAction("transfers", "direct", new { url = "/member/u-" + user.Id + "/shared", directPage = "个人中心分享页" });
                    }
                }
            }
            
            return View("~/views/download/uploaddocs.cshtml");
        }
        #endregion

        #region private
        private List<ViewDocument> GetViewDocuments(IQueryable<ViewDocument> queryable, Expression<Func<ViewDocument, string>> express,string tag,int current) {
            List<ViewDocument> viewDocuments = new List<ViewDocument>();          
            if(express!=null)
                viewDocuments = queryable.OrderByDescending(express).Skip(ClubConst.WebPageSize * (current - 1)).Take(ClubConst.WebPageSize).ToList<ViewDocument>();
            else
                viewDocuments = queryable.OrderBy(d => d.Id).Skip(ClubConst.WebPageSize * (current - 1)).Take(ClubConst.WebPageSize).ToList<ViewDocument>();
            return viewDocuments;
        }

        private Expression<Func<ViewDocument, string>> GetExpression(string sort) {
            Expression<Func<ViewDocument, string>> expression = null;
            switch (sort){
                case "views":
                    expression = v => v.Views.ToString();
                    break;
                case "downs":
                    expression = v => v.Downs.ToString();
                    break;
                case "time":
                    expression = v => v.VarDate.ToString();
                    break;
                default:
                    break;
            }
            return expression;
        }

        #endregion
    }
}
