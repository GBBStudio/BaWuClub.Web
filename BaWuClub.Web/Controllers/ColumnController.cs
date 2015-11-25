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

        #region 专栏列表
        public ActionResult Index(int? id){
            tId = id ?? 1;
            List<ViewArticle> viewArticles = new List<ViewArticle>();
            using(club=new ClubEntities()){
                viewArticles = club.ViewArticles.OrderByDescending(a => a.Id).Where(a => a.Status > 0).Skip((tId-1)*ClubConst.WebQuestionPageSize).Take(ClubConst.WebQuestionPageSize).ToList();
                ViewBag.HotQuestions = club.Questions.OrderByDescending(q => (club.Answers.Where(a => a.QId == q.Id).Count())).Take(8).ToList<Question>();
                ViewBag.PageStr = new PagingHelper(ClubConst.WebQuestionPageSize,tId,club.ViewArticles.Where(a=>a.Status>0).Count(),5).GetPageStringPro("/column/index/", false);
            }
            return View(viewArticles);
        }
        #endregion

        #region 专栏展示
        public ActionResult Show(int? id) {
            tId = id ?? 0;
            ViewArticle viewArticle = new ViewArticle();
            using (club = new ClubEntities()) {
                viewArticle = club.ViewArticles.Where(v => v.Id == tId).FirstOrDefault();
                Article article = club.Articles.Where(a => a.Id == tId).FirstOrDefault();
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
        #endregion

        #region 文章评论
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
                    str.Append("<a href=\"/member/u-" + user.Id + "/show/\" class=\"comment-item-info-avatar\"><img src=\"" + (string.IsNullOrEmpty(user.Cover) ? "/content/images/no-img.jpg" : "~/uploads/avatar/small/" + user.Cover) + "\"/>");
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
