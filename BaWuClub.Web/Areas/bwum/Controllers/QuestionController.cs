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
    public class QuestionController : Controller
    {
        #region vars
        private ClubEntities club;
        private int tId = 0;
        protected ViewQuestion viewQuestion;
        private string hitStr = string.Empty;
        private Status status = Status.error;
        #endregion

        #region 问题列表
        [HttpGet]
        public ActionResult Index(int? page){
            int current = page ?? 1;
            List<ViewQuestion> viewQuestions=new List<ViewQuestion>();
            using (club = new ClubEntities()) {
                viewQuestions = club.ViewQuestions.OrderBy(v=>v.Id).Skip(ClubConst.AdminPageSize * (current-1)).Take(ClubConst.AdminPageSize).ToList<ViewQuestion>();
                ViewBag.Count=club.ViewQuestions.Count();
            }
            ViewBag.PageHtmlStr = HtmlCommon.GetPageStr(ClubConst.AdminPageSize, current, ViewBag.Count);
            return View(viewQuestions);
        }
        #endregion

        #region 问题删除
        public JsonResult Del(int? id)
        {
            tId = id ?? 0;
            using (club = new ClubEntities())
            {
                var question = club.Questions.Where(a => a.Id == tId).FirstOrDefault();
                if (question != null)
                {
                    club.Questions.Remove(question);
                    if (club.SaveChanges() >= 0)
                    {
                        hitStr = "删除成功！";
                        status = Status.success;
                    }
                }
                else
                {
                    hitStr = "要删除的数据不存在！";
                }
            }
            return Json(new { status = status.ToString(), context = HtmlCommon.GetHitStr(status, hitStr) });
        }

        public JsonResult MultiDel(string[] chk)
        {
            if (chk.Length == 0)
            {
                hitStr = "未选中行,请选中行后再进行操作！";
            }
            else
            {
                using (club = new ClubEntities())
                {
                    foreach (string ck in chk)
                    {
                        tId = Convert.ToInt32(ck);
                        var question = club.Questions.Where(b => b.Id == tId).FirstOrDefault();
                        club.Questions.Remove(question);
                    }
                    if (club.SaveChanges() >= 0)
                    {
                        hitStr = "信息删除成功！";
                        status = Status.success;
                    }
                    else
                    {
                        hitStr = "系统异常删除失败,请稍后重试！";
                    }
                }
            }
            return Json(new { state = status.ToString(), context = hitStr.ToString(), url = "/bwum/activity/" });
        }
        #endregion

        #region 查看
        public ActionResult Show(int? id) {
            tId = id ?? 0;
            using (club = new ClubEntities()) {
                viewQuestion = club.ViewQuestions.Where(v => v.Id == tId).FirstOrDefault();
            }
            if (viewQuestion == null)
                return RedirectToAction("notfound","error");
            return View(viewQuestion);
        }
        #endregion
    }
}
