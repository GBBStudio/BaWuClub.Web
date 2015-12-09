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
    public class RoleController : Controller
    {
        #region
        private int tId = 0;
        private string hitStr = string.Empty;
        private Status status = Status.error;
        private ClubEntities club;
        private AdminAccount adminAccount;
        #endregion

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
        public ActionResult AccountCreate(){
            adminAccount = new AdminAccount();
            using(club=new ClubEntities()){
                ViewBag.adminTypes = GetAdminTypeList(club);
            }
            return View("~/areas/bwum/views/role/accountcreate.cshtml", adminAccount);
        }

        [HttpPost]
        public JsonResult AccountCreate(string username, string password, string confirmpassword, string phone, string email, string realname, string address, string cover) {
            if (string.IsNullOrEmpty(username)) {
                hitStr = "用户名不能为空！";
            }
            else if (string.IsNullOrEmpty(password)) {
                hitStr = "请输入姓名！";
            }
            else if (string.IsNullOrEmpty(confirmpassword)) {
                hitStr = "请输入确认密码！";
            }
            else if (password != confirmpassword) { 
                hitStr = "两次密码输入不一致！";
            }else{
                AdminAccount account = new AdminAccount() { 
                    UserName=username,
                    PassWord=System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password,"MD5"),
                    Phone=phone,
                    RealName=realname,
                    Address=address,
                    Cover=cover,
                    Email=email
                };
                using (club = new ClubEntities()) {
                    club.AdminAccounts.Add(account);
                    if (club.SaveChanges() > 0) {
                        status = Status.success;
                        hitStr = "账号创建成功！";
                    }
                }
            }
            return Json(new { status = status.ToString() ,context=hitStr});
        }
        #endregion

        #region 获取管理员分类的列表
        private List<AdminType> GetAdminTypeList(ClubEntities club) {
            List<AdminType> list = new List<AdminType>();
            list = club.AdminTypes.ToList<AdminType>();
            return list;
        }
        #endregion
    }
}
