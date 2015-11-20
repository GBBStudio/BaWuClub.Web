using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;

namespace BaWuClub.Web.Areas.bwum.Controllers
{
    public class OnlineController : Controller
    {
        #region
        public ClubEntities club;
        private Video video;
        private Status status = Status.error;
        private string hitStr = "系统异常，操作失败，请重试！";
        #endregion

        #region get
        [HttpGet]
        public ActionResult Index(int? page)
        {
            int current = page ?? 1;
            List<Video> videos = new List<Video>();
            using (club = new ClubEntities()) {
                videos = club.Videos.OrderBy(v => v.Id).Skip(ClubConst.AdminPageSize * (current - 1)).Take(ClubConst.AdminPageSize).ToList<Video>();
                ViewBag.Count = club.Videos.Count();
            }
            ViewBag.PageHtmlStr = HtmlCommon.GetPageStr(ClubConst.AdminPageSize, current, ViewBag.Count);
            return View("~/areas/bwum/views/online/index.cshtml", videos);
        }
        public ActionResult Create(){
            video = new Video();
            return View("~/areas/bwum/views/online/edit.cshtml",video);
        }
        public ActionResult Show(int id) {
            video = new Video();
            using (club = new ClubEntities()) {
                video = club.Videos.Where(v => v.Id == id).FirstOrDefault();
            }
            return View("~/areas/bwum/views/online/show.cshtml",video);
        }
        public JsonResult Del(int id) {
            video = new Video();
            using (club = new ClubEntities()) {
                video = club.Videos.Where(v => v.Id == id).FirstOrDefault();
                if (video != null) {
                    club.Videos.Remove(video);
                    if (club.SaveChanges() >= 0) {
                        status = Status.success;
                        hitStr = "操作成功！";
                    }
                }
            }
            return Json(new { state = status.ToString(), context = hitStr });
        }
        public JsonResult MultiDel(string[] chk,int id) {
            if (chk.Length == 0){
                hitStr = "未选中行,请选中行后再进行操作！";
            }
            else{
                int vId = 0;
                using (club = new ClubEntities()){
                    foreach (string ck in chk){
                        vId = Convert.ToInt32(ck);
                        video = club.Videos.Where(v => v.Id == vId).FirstOrDefault();
                        if (video != null){
                            club.Videos.Remove(video); 
                        }
                    }
                    if (club.SaveChanges() >= 0){
                        hitStr = "删除成功！";
                        status = Status.success;
                    }
                }
            }
            return Json(new { state = status.ToString(), context = hitStr });
        }
        #endregion

        #region post
        [HttpPost]
        public ActionResult Create(string title,string teacher,int mode,string cover,string localurl,string url,string desc) {
            video = new Video();
            if (string.IsNullOrEmpty(title)) {
                hitStr = "标题不能为空！";
            }
            else { 
                using (club = new ClubEntities()) {
                    video.Title = HtmlCommon.ClearHtml(title);
                    video.Teacher = HtmlCommon.ClearHtml(teacher);
                    video.Mode = (byte)mode;
                    video.Url = url;
                    video.Cover = cover;
                    video.LocalUrl = localurl;
                    video.Views = 0;
                    video.Sort = 0;
                    video.Status = 0;
                    video.VarDate = DateTime.Now;
                    video.Description = HtmlCommon.ClearHtml(desc);
                    club.Videos.Add(video);
                    if (club.SaveChanges() >= 0){
                        status = Status.success;
                        hitStr = "在线视频创建添加成功！";
                    }
                }
            }
            ViewBag.StatusStr = Common.HtmlCommon.GetHitStr(hitStr, status);
            return View("~/areas/bwum/views/online/edit.cshtml", video);
        }
        [HttpPost]
        public ActionResult SetCheck(int id) {
            video = new Video();
            using (club = new ClubEntities()) {
                video = club.Videos.Where(v => v.Id == id).FirstOrDefault();
                if (video != null) {
                    video.Status = (byte)(((int)video.Status == 0) ? 1 : 0);
                    if (club.SaveChanges() >= 0) {
                        status = Status.success;
                        hitStr = "修改成功！";
                    }
                }
            }
            ViewBag.StatusStr = HtmlCommon.GetHitStr(hitStr,status);
            return View("~/areas/bwum/views/online/show.cshtml",video);
        }
        [HttpPost]
        public JsonResult SetEnable(string[] chk) {
            return SetState(chk, 1);
        }
        [HttpPost]
        public JsonResult SetDisable(string[] chk) {
            return SetState(chk, 0);
        }
        #endregion

        #region private method
        private JsonResult SetState(string[] chks,int sId) {
            if (chks == null) {
                hitStr = "未选中行，请先选中！";
            }else{
                using (club = new ClubEntities()){
                    video = new Video();
                    int vId = 0;
                    foreach (string chk in chks){
                        vId = Convert.ToInt32(chk);
                        video = club.Videos.Where(v => v.Id == vId).FirstOrDefault();
                        video.Status = (byte)sId;
                        if (club.SaveChanges() >= 0){
                            status = Status.success;
                            hitStr = "状态修改成功！";
                        }
                    }
                }
            }
            return Json(new { state = status.ToString(), context = hitStr });
        }
        #endregion
    }
}
