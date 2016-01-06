using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;
using System.Text.RegularExpressions;
using Wesen.OAuth;

namespace BaWuClub.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region 定义变量
        private ClubEntities club;
        private Status status = Status.error;
        private User user;
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
                    //System.Web.Security.FormsAuthentication.SetAuthCookie(user.NickName, true);
                    //HttpCookie cookie = new HttpCookie("bwusers");
                    //cookie.Values["id"] = _user.Id.ToString();
                    //cookie.Values["user"] = HttpUtility.UrlEncode(_user.NickName.ToString());
                    //cookie.Values["avatar"] = _user.Cover;
                    Response.Cookies.Add(SetCookies(_user));
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
            if (Request.IsAuthenticated) {
                user = GetUser();
                return RedirectUrl("/member/u-"+user.Id+"/show");
            }
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
                        if (UserReg(CreateUser(username, password, email,"",0),club))
                            return Login(username, password, "/member/u-"+user.Id+"/show");
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

        #region 第三方登录

       public ActionResult QQOAuth()
       {
           return Redirect(new QQOpenClient().GetCodeUrl());
       }

       public ActionResult QQLogin(string code) {
           if (!string.IsNullOrEmpty(code)) { 
               QQOpenClient openClient = new QQOpenClient(code);
               var user=openClient.GetUser();
               if (openClient.Exception == null) {
                   if (user.Ret == "0") {
                       Session["nickname"] =user.nickname;
                       Session["cover"] =user.figureurl_2;
                       Session["openid"] =openClient.OpenId.OpenId;
                       if (ThirdUserExist(openClient.OpenId.OpenId)) {
                           return ThirdLogin(openClient.OpenId.OpenId,"");
                       }
                   }
                   else{
                       //跳转至其他页面。并settimeout让用户重新登陆。
                   }
               }
           }
           return View("~/views/account/thirdaccount.cshtml");
        }

       public ActionResult SinaLogin(string code)
        {
            return Redirect("");
        }

       public ActionResult ThirdReg(string tid,string nickname) {
           if(string.IsNullOrEmpty(tid)){
               ViewBag.hint = "账号未授权，请<a href=\"/account/qqoauth\">点此</a>重新授权！";
               return View("~/views/account/thirdaccount.cshtml");
           }
           else if(string.IsNullOrEmpty(nickname)){
               ViewBag.hint = "还是给您自己起个霸气的名字吧！";
               return View("~/views/account/thirdaccount.cshtml");
           }
           else if (CheckName(nickname,null)){
               ViewBag.hint = "改名字已存在了，换个吧！";
               return View("~/views/account/thirdaccount.cshtml");
           }
           else {
               bool flag=false;
               if (!ThirdUserExist(tid)) {
                   using (club = new ClubEntities()) {
                       user = CreateUser(nickname, "", "", tid, 1);
                       if (UserReg(user,club)){
                           flag = true;
                           Response.Cookies.Add(SetCookies(user));
                       }

                   }
               }
               if (flag) { 
                   Session.Clear();
                   return Redirect("/home");
               }
               else { 
                ViewBag.hint = "登录账号失败，请稍后重试！";
                return View("~/views/account/thirdaccount.cshtml");
              }
           }
       }

       public ActionResult ThirdLogin(string tid,string nickname) {
            using (club = new ClubEntities()){
                user = club.Users.Where(u => u.Tid == tid).FirstOrDefault();
                Response.Cookies.Add(SetCookies(user));
                if (user != null) {
                    user.LastLoginDate = DateTime.Now;
                    user.LastLoginIP = Request.UserHostAddress;
                    club.SaveChanges();
                }
            }
            return Redirect("/home");
       }
        #endregion

        #region 私有方法
       private BaWuClub.Web.Dal.User CreateUser(string username,string password,string email,string tid,int type) {
           var u = new User
           {
               NickName = HtmlCommon.ClearHtml(username),
               Password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5"),
               Email = HtmlCommon.ClearHtml(email),
               RegDate = DateTime.Now,
               LastLoginDate = DateTime.Now,
               LastLoginIP = Request.UserHostAddress,
               Tid = HtmlCommon.ClearHtml(tid),
               Type = (byte)type,
               Status = 1,
               Membership = 0,
               Points = 0,
               Cover = ""
           };
           return u;
       }

        private bool ThirdUserExist(string tid) {
            using (club = new ClubEntities()) {
                int count = club.Users.Where(u => u.Tid != null&&u.Tid.ToString()==tid).Count();
                if (count == 1) {
                    return true;
                }
            }
            return false;
        }

        private bool UserReg(BaWuClub.Web.Dal.User user,ClubEntities c) {
            c.Users.Add(user);
            if(c.SaveChanges()<0)
                return false;
            return true;
        }

        private bool CheckName(string name,ClubEntities c) {
            List<BaWuClub.Web.Dal.User> users = new List<User>();
            if (c == null) {
                using (c = new ClubEntities()) { 
                   users = c.Users.Where(u => u.NickName == name).ToList();
                }
            }
            else { 
                   users = c.Users.Where(u => u.NickName == name).ToList();
            }
            if (users.Count() > 0)
                return true;
            return false;
        }

        private bool CheckEmail(string email,ClubEntities c) {
            var t = c.Users.Where(u => u.Email == email).ToList();
            if (t.Count()>0)
                return true;
            return false;
        }

        private HttpCookie SetCookies(BaWuClub.Web.Dal.User u) {
            System.Web.Security.FormsAuthentication.SetAuthCookie(u.NickName, true);
            HttpCookie cookie = new HttpCookie("bwusers");
            cookie.Values["id"] = u.Id.ToString();
            cookie.Values["user"] = HttpUtility.UrlEncode(u.NickName.ToString());
            cookie.Values["avatar"] = u.Cover;
            return cookie;
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
