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
    public class UserController : Controller
    {
        #region 定义变量
        private ClubEntities club;
        private string hitStr= string.Empty;
        private int tId = 0;
        private User user;
        private Status status = Status.error;
        #endregion

        #region 账号列表
        public ActionResult Index(string page)
        {
            List<User> users = new List<User>();
            int pagenumber = 1;
            if (!string.IsNullOrEmpty(page) && Int32.TryParse(page, out pagenumber))
                if (pagenumber == 0)
                    pagenumber = 1;
            using (club = new ClubEntities()) {
                var _users = from u in club.Users.OrderBy(u => u.Id).Skip<User>((pagenumber -1)* ClubConst.AdminPageSize).Take<User>(ClubConst.AdminPageSize)
                             select u;
                users = _users.ToList<User>();
                ViewBag.PageHtmlStr = HtmlCommon.GetPageStr(ClubConst.AdminPageSize, Convert.ToInt32(page), club.Users.Count());
            }
            return View(users);
        }
        #endregion

        #region 账号展示
        public ActionResult Show(int? id) {
            BaWuClub.Web.Dal.User user = new User() { Id=Convert.ToInt32(id)};
            using (club = new ClubEntities()) {
                user = club.Users.Single<BaWuClub.Web.Dal.User>(u => u.Id == user.Id);
            }
            return View(user);
        }
        #endregion

        #region 账号删除
        public JsonResult MultiDel(string[] chk)
        {
            if (chk.Length == 0) {
                hitStr = "未选中行,请选中行后再进行操作！";
            }
            else
            {
                using (club = new ClubEntities())
                {
                    foreach (string ck in chk)
                    {
                        tId = Convert.ToInt32(ck);
                        user = club.Users.Where(b => b.Id == tId).FirstOrDefault();
                        club.Users.Remove(user);
                    }
                    if (club.SaveChanges() >= 0)
                    {
                        hitStr = "账号删除成功！";
                        status = Status.success;
                    }
                    else
                    {
                        hitStr = "系统异常删除失败,请稍后重试！";
                    }
                }
            }
            return Json(new { state = status.ToString(), context = hitStr.ToString(), url = "/bwum/users/" });
        }
        #endregion

        #region 设置账号的状态
        [HttpPost]
        public JsonResult SetEnable(string[] chk)
        {
            string contextStr = "状态修改成功！";
            if (chk == null)
            {
                contextStr = "未选中行.请先选中!";
            }
            else
            {
                if (!SetStatus(chk, 1))
                {
                    contextStr = "系统异常，操作失败！";
                }
                else
                {
                    status = Status.success;
                }
            }
            return Json(new { state = status.ToString(), context = contextStr });
        }

        [HttpPost]
        public JsonResult SetDisable(string[] chk)
        {
            string contextStr = "状态修改成功！";
            if (chk == null)
            {
                contextStr = "未选中行.请先选中!";
            }
            else
            {
                if (!SetStatus(chk, 0))
                {
                    contextStr = "系统异常，操作失败！";
                }
                else
                {
                    status = Status.success;
                }
            }
            return Json(new { state = status.ToString(), context = contextStr });
        }
        #endregion

        #region 私有方法
        private void DelUser(int id)
        {
            using (club = new ClubEntities()){
                BaWuClub.Web.Dal.User user = new Dal.User();
                user = club.Users.Where(u => u.Id == id).FirstOrDefault();
                if (user != null) {
                    club.Users.Remove(user);
                }
                if (club.SaveChanges() > 0)
                    ViewBag.StatusStr = "删除成功!";
                else
                    ViewBag.StatusStr = "删除失败，请稍后重试";
            }
        }

        private bool SetStatus(string[] chks, int sId)
        {
            using (club = new ClubEntities())
            {
                user = new User();
                foreach (string chk in chks)
                {
                    tId = Convert.ToInt32(chk);
                    user = club.Users.Where(a => a.Id == tId).FirstOrDefault();
                    user.Status = (byte)sId;
                    if (club.SaveChanges() < 0)
                        return false;
                }
                return true;
            }
        }
        #endregion
    }
}
