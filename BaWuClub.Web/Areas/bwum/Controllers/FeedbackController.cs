using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;
using BaWuClub.Web.App_Start;

namespace BaWuClub.Web.Areas.bwum.Controllers
{
    [AdminAuthorize]
    public class FeedbackController : Controller
    {
        //
        // GET: /bwum/Feedback/

        private ClubEntities club;
        private int tId = 0;
        string hitStr = string.Empty;
        private Status status = Status.error;

        #region 反馈信息列表展示
        public ActionResult Index(int? id){
            tId = id ?? 1;
            int count=0;
            List<Feedback> list = new List<Feedback>();
            using (club = new ClubEntities()) {
                list = club.Feedbacks.OrderBy(f => f.Id == tId).Skip((tId - 1) * ClubConst.AdminPageSize).Take(ClubConst.AdminPageSize).ToList<Feedback>();
                count = club.Feedbacks.Count();
            }
            ViewBag.PageHtmlStr = HtmlCommon.GetPageStr(ClubConst.AdminPageSize, tId, count);
            return View("~/areas/bwum/views/feedback/index.cshtml",list);
        }
        #endregion

        #region 反馈信息展示
        public ActionResult Show(int? id) {
            Feedback feedback = new Feedback();
            tId = id ?? 0;
            using (club = new ClubEntities()) {
                feedback = club.Feedbacks.Where(f => f.Id == tId).FirstOrDefault();
            }
            return View("~/areas/bwum/views/feedback/show.cshtml", feedback);
        }
        #endregion

        #region 反馈信息删除
        public JsonResult Del(int? id) {
            tId = id ?? 0;
            using (club = new ClubEntities()) {
                var feedback = club.Feedbacks.Where(a => a.Id == tId).FirstOrDefault();
                if (feedback != null) {
                        club.Feedbacks.Remove(feedback);
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
                         var feedback = club.Feedbacks.Where(b => b.Id == tId).FirstOrDefault();
                         club.Feedbacks.Remove(feedback);
                    }
                     if (club.SaveChanges() >= 0) {
                         hitStr = "信息删除成功！";
                         status = Status.success;
                     }else{
                         hitStr = "系统异常删除失败,请稍后重试！";
                     }
                }               
            }
            return Json(new { state=status.ToString(),context=hitStr.ToString(),url="/bwum/feedback/"});
        }
        #endregion
    }
}
