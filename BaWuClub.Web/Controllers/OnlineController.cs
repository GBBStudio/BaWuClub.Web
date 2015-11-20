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
    public class OnlineController : BaseController
    {
        #region
        public ClubEntities club;
        Status status = Status.error;
        #endregion

        public ActionResult Index(){
            using (club = new ClubEntities()) {
                var videoList = club.Videos.Take(6).ToList<BaWuClub.Web.Dal.Video>();
                ViewBag.rowCount = club.Videos.Count();
                ViewBag.videoList = videoList;
                ViewBag.videoTop = club.Videos.OrderByDescending(v => v.VarDate).Where(v => v.Status == (int)VideoStatus.Top).FirstOrDefault();
                ViewBag.videoRecommend = club.Videos.OrderByDescending(v => v.VarDate).Where(v => v.Status == (int)VideoStatus.Recommend).FirstOrDefault();
                if (ViewBag.videoTop == null)
                    ViewBag.videoTop = club.Videos.OrderByDescending(v => v.Views).FirstOrDefault();
                ViewBag.pageStr = new BaWuClub.Web.Common.PagingHelper(6, 1, ViewBag.rowCount, 5).GetPageStringPro("");
            }
            return View();
        }

        public JsonResult P(int p) {
            return Json(new { });
        }

        public ActionResult Course(int id) {
            Video video = new Video();
            using (club = new ClubEntities()) {
                video = club.Videos.Where(v => v.Id == id).FirstOrDefault();
                ViewBag.reviews = club.ViewVideoReviews.Where(v => v.VideoId == id).ToList<ViewVideoReview>();
                if (video != null) {
                    video.Views += 1;
                    club.SaveChanges();
                }
                else {
                    return RedirectToAction("notfound","error");
                }
            }
            return View("~/views/online/course.cshtml",video);
        }

        [ValidateInput(false)]
        public JsonResult reviews(int id, string commentStr) {
            BaWuClub.Web.Dal.User user = GetUser();
            string url="/account/login";
            string context = string.Empty;
            if (user != null) {
                VideoReview reviews = new VideoReview();
                reviews.UserId=user.Id;
                reviews.VideoId=id;
                reviews.Review=HtmlCommon.ClearJavascript(commentStr);
                reviews.VarDate=DateTime.Now;
                reviews.IP = Request.UserHostAddress;
                using (club = new ClubEntities()) {
                    club.VideoReviews.Add(reviews);
                    if (club.SaveChanges() >= 0){
                        status = Status.success;
                        StringBuilder str = new StringBuilder();
                        str.Append("<div class=\"comment-item\">");
                        str.Append("<div class=\"comment-item-info\">");
                        str.Append("<a href=\"/member/u-" + user.Id + "/show/\" class=\"comment-item-info-name\">" + user.NickName + "</a>");
                        str.Append("<a href=\"/member/u-" + user.Id + "/show/\" class=\"comment-item-info-avatar\"><img src=\"" + (string.IsNullOrEmpty(user.Cover) ? "/content/images/no-img.jpg" : "~/uploads/avatar/small/" + user.Cover) + "\"/>");
                        str.Append("</a>");
                        str.Append("</div>");
                        str.Append("<div class=\"comment-item-content\">" + HtmlCommon.ClearJavascript(reviews.Review) + "</div>");
                        str.Append("<div class=\"comment-item-meta\"><span>评论与" + HtmlCommon.GetAnswerTimeSpan((DateTime)reviews.VarDate) + "</span></div>");
                        str.Append("</div>");
                        context = str.ToString();
                    }
                    else{
                        status = Status.warning; context = "系统异常，操作失败，请稍后重试！";
                    }
                }                
            }
            return Json(new { status = status.ToString(), context = context, url = url });
        }
        #region private method
        #endregion
    }
}
