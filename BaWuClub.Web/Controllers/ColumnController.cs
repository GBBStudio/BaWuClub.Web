using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;
using System.Text;

namespace BaWuClub.Web.Controllers
{
    public class ColumnController : BaseController
    {
        //
        // GET: /Column/
        //private BaWuClub.Web.Dal.User user;
        private BaWuClub.Web.Dal.ClubEntities club;
        private int tId = 0;

        #region get
        public ActionResult Index(int? id){
            tId = id ?? 1;
            List<ViewArticle> viewArticles = new List<ViewArticle>();
            using(club=new ClubEntities()){
                ViewBag.newArticles = club.Articles.OrderByDescending(a => a.VarDate).Take(7).ToList<Article>();
                ViewBag.hotQuestions = club.Questions.OrderByDescending(a => a.Views).Take(7).ToList<Question>();
                viewArticles = club.ViewArticles.OrderByDescending(a => a.Id).Where(a => a.Status > 0).Skip((tId - 1) * ClubConst.WebQuestionPageSize).Take(ClubConst.WebQuestionPageSize).ToList();
                ViewBag.PageStr = new PagingHelper(ClubConst.WebQuestionPageSize,tId,club.ViewArticles.Where(a=>a.Status>0).Count(),5).GetPageStringPro("/column/index/", false);
            }
            return View(viewArticles);
        }

        public ActionResult Show(int? id) {
            tId = id ?? 0;
            ViewArticle viewArticle = new ViewArticle();
            using (club = new ClubEntities()) {
                viewArticle = club.ViewArticles.Where(v => v.Id == tId).FirstOrDefault();
                Article article = club.Articles.Where(a => a.Id == tId).FirstOrDefault();
                if (viewArticle != null) {
                    ViewBag.otherArticles = club.Articles.Where(a => a.UserId == viewArticle.UserId&&a.Id!=viewArticle.Id && a.Status == (int)State.Enable).Take(10).ToList<Article>();
                }
                ViewBag.Reviews = club.ViewArticleReviews.Where(a => a.ArticleId == tId).ToList<ViewArticleReview>();
                ViewBag.ReviewsCount = club.ViewArticleReviews.Where(a => a.ArticleId == tId).Count();
                if (article != null) {
                    article.Views = (article.Views+ 1);
                }
                club.SaveChanges();
            }
            if (viewArticle == null)
                RedirectToAction("NotFound", new { Controller = "Error" });
            else if (viewArticle.Status == 0)
                return RedirectToAction("Unaudited","error");
            ViewBag.Title = viewArticle.Title;
            return View(viewArticle);
        }
        
        public ActionResult Tags(int? tid,int? p) {
            tId = tid ?? 0;
            int rowsCount = 0;
            int page = p ?? 1;
            List<ViewArticle> viewArticles = new List<ViewArticle>();
            Tag tag = new Tag();
            using (club = new ClubEntities()) {
                tag = club.Tags.Where(t => t.Id == tId).FirstOrDefault();
                if (tag == null) { 
                   return RedirectToAction("NotFound", new { Controller = "Error" });
                }
                ViewBag.tagName = tag.TagName;
                ViewBag.newArticles = club.Articles.OrderByDescending(a => a.VarDate).Take(7).ToList<Article>();
                ViewBag.hotQuestions = club.Questions.OrderByDescending(a => a.Views).Take(7).ToList<Question>();
                rowsCount = club.ViewArticles.Where(v => v.Tags.Contains(tag.TagName) && v.Status > 0).Count();
                viewArticles =club.ViewArticles.Where(v => v.Tags.Contains(tag.TagName)&&v.Status > 0).OrderByDescending(v=>v.VarDate).Skip((page - 1) * ClubConst.WebQuestionPageSize).Take(ClubConst.WebQuestionPageSize).ToList();
                ViewBag.PageStr = new PagingHelper(ClubConst.WebQuestionPageSize, page, rowsCount, 5).GetPageStringPro("/column/tags/"+tId+"-", false);
            }
            return View(viewArticles);
        }
        #endregion

        #region post
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Reviews(int id,string commentStr){
            BaWuClub.Web.Dal.User user = GetUser();
            ArticleReview review = new ArticleReview();
            Status status = Status.error;
            StringBuilder str = new StringBuilder();
            using (club = new ClubEntities()) {
                if (user==null||!User.Identity.IsAuthenticated)
                    return Json(new {status=Status.warning.ToString(),url="/account/login?returnurl=/colum/show/"+id });
                review.UserId = user.Id;
                review.ArticleId = id;
                review.ReviewText = HtmlCommon.ClearJavascript(commentStr);
                review.PutDate = DateTime.Now;
                review.Type = 1;
                review.IP = Request.UserHostAddress;
                club.ArticleReviews.Add(review);
                if (club.SaveChanges() >= 0) {
                    status = Status.success; 
                    str.Append("<div class=\"comment-item\">");
                    str.Append("<div class=\"comment-item-info\">");
                    str.Append("<a href=\"/member/u-" + user.Id + "/show/\" class=\"comment-item-info-name\">" + user.NickName + "</a>");
                    str.Append("<a href=\"/member/u-" + user.Id + "/show/\" class=\"comment-item-info-avatar\"><img src=\"" + (string.IsNullOrEmpty(user.Cover) ? "/content/images/no-img.jpg" : "/uploads/avatar/small/" + user.Cover) + "\"/>");
                    str.Append("</a>");
                    str.Append("</div>");
                    str.Append("<div class=\"comment-item-content\">" + HtmlCommon.ClearJavascript(commentStr) + "</div>");
                    str.Append("<div class=\"comment-item-meta\"><span>评论与" + HtmlCommon.GetAnswerTimeSpan(review.PutDate) + "</span></div>");
                    str.Append("</div>");
                }
            }
            return Json(new { status = status.ToString(), context = str.ToString() });
        }
        #endregion
    }
}
