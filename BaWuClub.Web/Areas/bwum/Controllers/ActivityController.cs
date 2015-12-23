using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;
using System.Text;
using BaWuClub.Web.App_Start;

namespace BaWuClub.Web.Areas.bwum.Controllers
{
    [AdminAuthorize]
    public class ActivityController : Controller
    {
        //
        // GET: /bwum/Activity/
        #region 定义变量
        private ClubEntities club;
        private Activity activity;
        private Status status = Status.error;
        string hitStr = string.Empty;
        int tId = 0;
        #endregion

        #region 获取活动的列表
        public ActionResult Index(int? id){
            tId=id??1;
            int count=0;
            List<ViewActivity> list = new List<ViewActivity>();
            using (club = new ClubEntities()) {
                list=club.ViewActivities.OrderByDescending(a => a.VarDate).Skip((tId - 1) * ClubConst.AdminPageSize).Take(ClubConst.AdminPageSize).ToList<ViewActivity>();
                count=club.ViewActivities.Count();
            }
            ViewBag.PageHtmlStr = HtmlCommon.GetPageStr(ClubConst.AdminPageSize,tId,count);
            return View("~/areas/bwum/views/activity/index.cshtml",list);
        }
        #endregion

        #region 活动的创建
        [HttpGet]
        public ActionResult Create() {
            activity = new Activity();
            using (club = new ClubEntities()) {
                GetProvince(club);
            }
            return View("~/areas/bwum/views/activity/edit.cshtml",activity);    
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(string title,int city,string address,string pic,string start,string end,string contact,string phone,string sponsor,string context,string cost,string limited) {
            activity = new Activity() { 
                    Title=HtmlCommon.ClearHtml(title),
                    CityId=city,
                    Address=address,
                    StartDate=DateTime.Parse(start), 
                    EndDate=DateTime.Parse(end),
                    Contact=contact,
                    Phone=phone,
                    Sponsor=sponsor,
                    Context=context,
                    Cover=pic,
                    Cost=cost,
                    Limited=limited
            };
            using (club = new ClubEntities()) {
                club.Activities.Add(activity);
                if (club.SaveChanges() >= 0) {
                    status = Status.success;
                    hitStr = "活动添加成功!";
                }
                else {
                    hitStr = "添加失败，请稍后重试！";
                }
            }
            ViewBag.StatusStr = HtmlCommon.GetHitStr(status,hitStr);
            return View("~/areas/bwum/views/activity/edit.cshtml",activity);
        }
        #endregion

        #region 活动编辑
        public ActionResult Show(int? id) {
            tId = id ?? 0;
            using (club = new ClubEntities()) {
                activity = club.Activities.Where(a => a.Id == tId).FirstOrDefault();
                if (activity == null) {
                    return Redirect("/error/notfound");
                }
                GetProvince(club);
                ViewBag.Area = club.Areas.Where(a => a.Id == activity.CityId).FirstOrDefault();
            }
            return View("~/areas/bwum/views/activity/edit.cshtml", activity);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id,string title, int city, string address, string pic, string start, string end, string contact, string phone, string sponsor, string context, string cost, string limited){
            using (club = new ClubEntities()) {
                activity = club.Activities.Where(a => a.Id == id).FirstOrDefault();
                activity.Title = title;
                activity.CityId = city;
                activity.Address = address;
                activity.Cover = pic;
                activity.StartDate = Convert.ToDateTime(start.Length > 0 ?start: DateTime.Now.ToString());
                activity.EndDate = Convert.ToDateTime(end.Length > 0 ? end:DateTime.Now.ToString() );
                activity.Contact = contact;
                activity.Phone = phone;
                activity.Sponsor = sponsor;
                activity.Cost = cost;
                activity.Limited = limited;
                activity.Context = context;
                if (club.SaveChanges() >= 0){
                    ViewBag.Area = club.Areas.Where(a => a.Id == activity.CityId).FirstOrDefault();
                    status = Status.success;
                    hitStr = "活动更新成功!";
                }
                else{
                    hitStr = "更新失败，请稍后重试！";
                }
            }
            ViewBag.StatusStr = HtmlCommon.GetHitStr(hitStr, status);
            return View("~/areas/bwum/views/activity/edit.cshtml", activity);
        }
        #endregion

        #region 活动删除
        public JsonResult Del(int? id) {
            tId=id??0;
            using (club = new ClubEntities()) {
               var activity = club.Activities.Where(a => a.Id == tId).FirstOrDefault();
                if (activity != null) {
                    club.Activities.Remove(activity);
                        if (club.SaveChanges() >= 0) {
                            hitStr = "删除成功！";
                            status = Status.success;
                        }
                    }else{
                        hitStr="要删除的数据不存在！";
                    }
                }
            return Json(new { status=status.ToString(),context=HtmlCommon.GetHitStr(status,hitStr)});
        }

        public JsonResult MultiDel(string[] chk){
            if (chk.Length == 0) {
                hitStr = "未选中行,请选中行后再进行操作！";
            }else{
                using (club = new ClubEntities()) { 
                     foreach (string ck in chk) {
                         tId = Convert.ToInt32(ck);
                         var activity = club.Activities.Where(b => b.Id == tId).FirstOrDefault();
                         club.Activities.Remove(activity);
                    }
                     if (club.SaveChanges() >= 0) {
                         hitStr = "信息删除成功！";
                         status = Status.success;
                     }else{
                         hitStr = "系统异常删除失败,请稍后重试！";
                     }
                }               
            }
            return Json(new { state=status.ToString(),context=hitStr.ToString(),url="/bwum/activity/"});
        }
        #endregion 

        [HttpGet]
        public JsonResult GetCity(int province) {
            StringBuilder listStr = new StringBuilder();
            List<Area> list = new List<Area>();
            using (club = new ClubEntities()) {
                list = club.Areas.Where(a => a.Node == province).ToList();
            }            
            if (list.Count > 0) {
                foreach (Area area in list) {
                    listStr.Append("<option value=\"" + area.Id + "\">" + area.Name + "</option>");
                }
                status = Status.success;
            }
            return Json(new {status=status.ToString(),context=listStr.ToString() },JsonRequestBehavior.AllowGet);
        }

        #region 活动编辑的私有方法
        private void GetProvince(ClubEntities c) {
            ViewBag.Province = c.Areas.Where(a => a.Node == 0).ToList<Area>();
        }
        #endregion
    }
}
