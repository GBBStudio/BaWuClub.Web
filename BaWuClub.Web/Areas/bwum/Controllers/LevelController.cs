using System;
using System.Collections;
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
    public class LevelController : Controller
    {
        //
        // GET: /bwum/Level/
        #region 定义变量
        private int tId = 0;
        private ClubEntities club;
        private Status status = Status.error;
        private string hitStr = string.Empty;
        #endregion

        #region 获取会员等级列表
        public ActionResult Index(int? id)
        {
            tId = id ?? 0;
            List<AdminType> adminTypes = new List<AdminType>();
            int count = 0;
            using (club = new ClubEntities()) {
                adminTypes = club.AdminTypes.ToList<AdminType>();
            }
            if (adminTypes != null)
                count = adminTypes.Count;
            ViewBag.PageHtmlStr = HtmlCommon.GetPageStr(ClubConst.AdminPageSize, tId, count);
            return View(adminTypes);
        }
        #endregion

        #region 创建、修改、会员的等级
        [HttpGet]
        public ActionResult Create() {
            return View(GetRole());
        }

        [HttpPost]
        public ActionResult Create(string name, string role) {
            if (string.IsNullOrEmpty(name))
            {
                hitStr = "名称不能为空！";
            }
            else {
                AdminType adminType = new AdminType { Name=name,Role=role,VarDate=DateTime.Now};
                using (club = new ClubEntities()) {
                    club.AdminTypes.Add(adminType);
                    if (club.SaveChanges() > 0)
                    {
                        hitStr = "创建成功！";
                        status = Status.success;
                    }
                    else {
                        hitStr = "系统异常，请稍后重试！";
                    }
                }
            }
            ViewBag.StatusStr = Common.HtmlCommon.GetHitStr(hitStr, status);
            return View(GetRole());
        }

        [HttpGet]
        public ActionResult Edit(int? id) {
            tId = id ?? 0;
            AdminType adminType = new AdminType();
            using (club = new ClubEntities()) {
                adminType = club.AdminTypes.Where(t => t.Id == tId).FirstOrDefault();
            }
            if (adminType == null)
                RedirectToAction("notfound","error");
            ViewBag.AdminType = adminType;
            return View("~/areas/bwum/views/level/create.cshtml",GetRole());
        }

        public ActionResult Edit(int id,string name,string role) {
            AdminType adminType = new AdminType();
            if (string.IsNullOrEmpty(name))
            {
                hitStr = "名称不能空！";
            }
            else {
                using (club = new ClubEntities())
                {
                    adminType = club.AdminTypes.Where(t => t.Id == id).FirstOrDefault();
                    adminType.Name = name;
                    adminType.Role = role;
                    if (club.SaveChanges() > 0)
                    {
                        status = Status.success;
                        hitStr = "更新成功！";
                    }
                    else
                    {
                        hitStr = "系统异常，更新失败！";
                    }
                }
                if (adminType == null)
                    RedirectToAction("notfound", "error");
                ViewBag.AdminType = adminType;
            }            
            ViewBag.StatusStr = Common.HtmlCommon.GetHitStr(hitStr, status);
            return View("~/areas/bwum/views/level/create.cshtml", GetRole());
        }
        #endregion

        #region 私有方法
        private Role GetRole (){
            Role r = new Role(Server.MapPath("~/app_data/role.xml"));
            Dictionary<string, string> operatesDic = new Dictionary<string, string>();
            Hashtable hashtable = new Hashtable();
            if (r.Operates.Count > 0)
            {
                foreach (var operate in r.Operates)
                {
                    operatesDic.Add(operate.Value.ToString(), operate.Name);
                }
            }
            ViewBag.OperatesDic = operatesDic;
            return r;
        }
        #endregion
    }
}
