using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;
using System.Text.RegularExpressions;

namespace BaWuClub.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region 定义变量
        private ClubEntities club;
        private Status status = Status.error;
        #endregion

        #region 登录界面
        public ActionResult Index() {
            if (Request.IsAuthenticated)
                return RedirectUrl("/member/");
            else
                return RedirectUrl("/account/login");
        }

        [AllowAnonymous]
        public ActionResult Login(string returnurl) {
            if (Request.IsAuthenticated)
                return RedirectUrl("/member/");
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string username, string password, string returnurl) {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) { 
                    ViewBag.Status = "<span style=\"color:red\">用户名或密码不能为空！</span>";
            }
            BaWuClub.Web.Dal.User user = new User() { NickName=HtmlCommon.ClearHtml(username),Password= System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5")};
            using (club = new ClubEntities()) {
                var _user = club.Users.Where(u =>u.NickName ==user.NickName  && u.Password ==user.Password).FirstOrDefault();
                if (_user!=null){
                    if (_user.LastLoginDate < DateTime.Now) {
                        _user.Points += 2;
                    }
                    _user.LastLoginDate = DateTime.Now;
                    _user.LastLoginIP = Request.UserHostAddress;
                    club.SaveChanges();
                    System.Web.Security.FormsAuthentication.SetAuthCookie(user.NickName, true);
                    HttpCookie cookie = new HttpCookie("bwusers");
                    cookie.Values["id"] = _user.Id.ToString();
                    cookie.Values["user"] = _user.NickName.ToString();
                    Response.Cookies.Add(cookie);
                    return RedirectUrl(returnurl);
                }
                else { 
                    ViewBag.Status = "<span style=\"color:red\">用户名或密码不正确！</span>";}
            }
            return View();
        }
        #endregion

        #region 账号退出
        public ActionResult LoginOut(string returnurl) {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectUrl(returnurl);
        }
        #endregion

        #region 用户注册
        [AllowAnonymous]
        public ActionResult Register(){
            if (Request.IsAuthenticated)
                return RedirectUrl("/member/");
            return View();
        }
        [HttpPost]
        public ActionResult Register(string username,string password,string password1,string email) {
            if (string.IsNullOrEmpty(username)||string.IsNullOrEmpty(email))   {
                ViewBag.Status =HtmlCommon.GetHitStr("用户名或邮箱不能为空",status) ;
            }
            else if (!Regex.IsMatch(email.Trim(), @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$")) {
                ViewBag.Status = HtmlCommon.GetHitStr("邮箱格式不正确",status);
            }
            else if (string.IsNullOrEmpty(password)) {
                ViewBag.Status = HtmlCommon.GetHitStr("密码不能为空", status);
            }
            else if (password.Length < 6) {
                ViewBag.Status = HtmlCommon.GetHitStr("密码长度不能低于6位",status);
            }
            else if (password != password1)   {
                ViewBag.Status = HtmlCommon.GetHitStr("两次密码输入不一致", status);
            }
            else {
                using (club = new ClubEntities()) {
                    if(CheckName(HtmlCommon.ClearHtml(username).Trim(),club))
                        ViewBag.Status = HtmlCommon.GetHitStr("用户名已存在！", status);
                    else if(CheckEmail(HtmlCommon.ClearHtml(email).Trim(),club))
                        ViewBag.Status = HtmlCommon.GetHitStr("该邮箱已经注册过其他的用户！", status);
                    else
                        if (UserReg(username, password, email,club))
                            return Login(username, password, "/member/");
                        else
                            ViewBag.Status = HtmlCommon.GetHitStr("系统异常，用户注册失败，请稍后重试！", status);
                }
            }
            return View();
        }
        #endregion

        #region 忘记密码
        public ActionResult Forget(){
            if (Request.IsAuthenticated)
                return RedirectUrl("/member/");
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string username,string email) {
            if (username.Trim().Length == 0 || email.Trim().Length == 0)
            {
                ViewBag.Status = "账号与邮箱均不能为空！";
            }
            else
            {
                using (club = new ClubEntities())
                {
                    var user = club.Users.Where(u => u.NickName == username && u.Email == email).FirstOrDefault();
                    if (user == null)
                    {
                        ViewBag.Status = "账户与邮箱不匹配！";
                    }
                    else
                    {
                        //将邮箱的配置放如xml or database(???????)
                        Common.Mail mail = new Mail("mengwf@xiakexing.cn", "mwf0912", "smtp.exmail.qq.com", 25, 6000, email, "八五电商圈", "忘记密码，点击http://", "八五电商圈");
                        try
                        {
                            mail.SendEmailAsync();
                            ViewBag.SendCompleted = true;
                        }
                        catch (Exception e)
                        {
                            ViewBag.Status = "系统异常，请稍后重试!";
                        }
                    }
                }
            }
            return View("~/views/account/forget.cshtml");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Forget(string email, string username) {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username))
                ViewBag.Status = "<span style=\"color:red\">邮箱与用户名均不能为空！</span>";
            return View();
        }
        #endregion

        #region 私有方法
        private bool UserReg(string username,string password,string email,ClubEntities c) {
            User u=new User {NickName=HtmlCommon.ClearHtml(username),Password=System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password,"md5"),Email=HtmlCommon.ClearHtml(email),RegDate=DateTime.Now,Status=1,Membership=0,Points=0};
            c.Users.Add(u);
            if(c.SaveChanges()<0)
                return false;
            return true;
        }
        private bool CheckName(string name,ClubEntities c) {
            var t = c.Users.Where(u => u.NickName == name).ToList();
            if (t.Count()>0)
                return true;
            return false;
        }
        private bool CheckEmail(string email,ClubEntities c) {
            var t = c.Users.Where(u => u.Email == email).ToList();
            if (t.Count()>0)
                return true;
            return false;
        }
        private ActionResult RedirectUrl(string returnUrl){
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
