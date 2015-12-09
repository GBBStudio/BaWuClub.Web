using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;

namespace BaWuClub.Web.Controllers
{
    public class ActivityController : BaseController
    {
        //
        // GET: /Activity/

        private ClubEntities club;
        private ViewActivity viewActivity;
        private int aId = 0;

        #region 活动列表
        public ActionResult Index(int? id){
            aId = id ?? 1;
            List<ViewActivity> list = new List<ViewActivity>();
            using (club = new ClubEntities()) {
                list = club.ViewActivities.OrderByDescending(a => a.VarDate).Skip((aId - 1) * ClubConst.WebPageSize).Take(ClubConst.WebPageSize).ToList<ViewActivity>();
                ViewBag.ActivityCount = club.ViewActivities.Count();
                ViewBag.ActivityHistory = club.ViewActivities.OrderByDescending(a => a.EndDate < DateTime.Now).Take(6).ToList<ViewActivity>();
                ViewBag.HotActivityBanners = club.ViewBanners.Where(b => b.Status == 1 && b.Variables == "sys-bt-activity-top").ToList<ViewBanner>();
                ViewBag.BannerActivityLeft = club.ViewBanners.Where(b => b.Status == 1 && b.Variables == "sys-bt-activity-right").FirstOrDefault();
            }
            PagingHelper pager=new PagingHelper(5,aId,ViewBag.ActivityCount,5);
            ViewBag.PageStr = pager.GetPageStringPro("/activity/index/",false);
            return View(list);
        }
        #endregion

        #region 活动详情页
        public ActionResult Show(int? id) {
            aId = id ?? 0;
            using (club = new ClubEntities()) {
                viewActivity = club.ViewActivities.Where(a => a.Id == aId).FirstOrDefault();
            }
            if (viewActivity == null)
                return Redirect("/error/notfound");
            return View(viewActivity);
        }
        #endregion

        #region 活动的参加
        [HttpGet]
        public ActionResult Join(int? id) {
            aId=id??0;
            using (club = new ClubEntities()) {
                viewActivity = club.ViewActivities.Where(a => a.Id == aId).FirstOrDefault();
            }
            if (viewActivity == null)
                return Redirect("/error/notfound");
            return View(viewActivity);
        }

        [HttpPost]
        public JsonResult JoinTo(int joinid,string name,string company,string post,string phone,string email,string site,string qq,string info) {
            Status status = Status.error;
            string hit = "系统异常，报名失败，请稍后重试！";
            using (club = new ClubEntities()) {
                ActivityOrder activityOrder = new ActivityOrder() { 
                    Name=HtmlCommon.ClearHtml(name),
                    Company=HtmlCommon.ClearHtml(company),
                    Post=HtmlCommon.ClearHtml(post),
                    Phone=HtmlCommon.ClearHtml(phone),
                    Email=HtmlCommon.ClearHtml(email),
                    Site=HtmlCommon.ClearHtml(site),
                    QQ=HtmlCommon.ClearHtml(qq),
                    Info=HtmlCommon.ClearHtml(info),
                    ActivityId=joinid,
                };
                club.ActivityOrders.Add(activityOrder);
                if (club.SaveChanges() > 0) {
                    status = Status.success;
                    hit = "报名成功！我们会尽快的联系您哦！";
                }
            }
            return Json(new { status = status.ToString(), context = hit });
        }
        #endregion
    }
}
