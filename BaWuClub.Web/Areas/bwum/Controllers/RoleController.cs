using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;

namespace BaWuClub.Web.Areas.bwum.Controllers
{
    public class RoleController : Controller
    {
        private int tId = 0;
        private string hitStr = string.Empty;
        private ClubEntities club;
        private AdminAccount adminAccount;

        #region 管理员账号列表
        public ActionResult Index(int? id) {
            tId = id ?? 1;
            List<AdminAccount> accounts = new List<AdminAccount>();
            using (club = new ClubEntities()) {
                accounts = club.AdminAccounts.OrderBy(t=>t.Id).Skip((tId - 1) * ClubConst.AdminPageSize).Take(ClubConst.AdminPageSize).ToList<AdminAccount>();
                 ViewBag.Count=club.ViewQuestions.Count();
            }
            ViewBag.PageHtmlStr = HtmlCommon.GetPageStr(ClubConst.AdminPageSize, tId, ViewBag.Count);
            return View("~/areas/bwum/views/role/account.cshtml",accounts);
        }
        #endregion

        #region 管理员账号的创建
        public ActionResult AccountCreate()
        {
            adminAccount = new AdminAccount();
            return View("~/areas/bwum/views/role/accountcreate.cshtml", adminAccount);
        }
        #endregion

        #region 获取会员分类的列表
        private List<AdminType> GetAdminTypeList(ClubEntities club) {
            List<AdminType> list = new List<AdminType>();
            list = club.AdminTypes.ToList<AdminType>();
            return list;
        }
        #endregion
    }
}
