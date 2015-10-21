using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;

namespace BaWuClub.Web.Controllers
{
    public class DownloadController : BaseController
    {
        #region
        private ClubEntities club;
        #endregion

        #region get
        public ActionResult Index()
        {
            List<ViewDocument> viewDocs = new List<ViewDocument>();
            using (club = new ClubEntities())
            {
                viewDocs = club.ViewDocuments.ToList<ViewDocument>();
            }
            return View();
        }

        [Authorize]
        public ActionResult UploadDocs() {
            return View("~/views/download/uploaddocs.cshtml");
        }

        public ActionResult P(int id) {
            List<ViewDocument> viewDocs = new List<ViewDocument>();
            using (club = new ClubEntities()) {
                viewDocs = club.ViewDocuments.ToList<ViewDocument>();
            }
            return View("~/views/download/index.cshtml",viewDocs);
        }
        #endregion

        #region post
        [Authorize]
        [HttpPost]
        public ActionResult Uploads(string title,string tags,string file,string context) {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(tags) || string.IsNullOrEmpty(file) || string.IsNullOrEmpty(context))
            {
                ViewBag.hitStr = "请填写要上传文件的信息完整";
            }
            else {
                BaWuClub.Web.Dal.User user = GetUser();
                Document doc = new Document()
                {
                    Title = HtmlCommon.ClearHtml(title),
                    Tags = HtmlCommon.ClearHtml(tags),
                    Url = file,
                    Description = HtmlCommon.ClearHtml(context),
                    UserId = user.Id,
                    Status = 0,
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
    }
}
