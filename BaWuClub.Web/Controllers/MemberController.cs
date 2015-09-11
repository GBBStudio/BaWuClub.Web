using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;
using System.Text;

namespace BaWuClub.Web.Controllers
{
    public class MemberController : BaseController
    {
        //
        // GET: /Member/
        private ClubEntities club;
        private BaWuClub.Web.Dal.User user;
        private Status status = Status.error;

        [Authorize]
        public ActionResult Index(int? uid){
            using (club = new ClubEntities()) {
                user = GetUser(club);
            }
            ViewBag.User = user;
            return View();
        }
                
        #region 投稿
        [Authorize]
        public ActionResult Contribute() {
            using (club = new ClubEntities()) {
                user = GetUser(club);
            }
            ViewBag.User = user;
            return View();  
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public JsonResult Contribute(string title,string tags,string editor){
            string hinStr=string.Empty;
            string url = string.Empty;
            if (string.IsNullOrEmpty(title)||string.IsNullOrEmpty(editor)) { 
               hinStr= "投稿的标题和内容均不能为空，请填写标题后再提交！";
            }else{
                using (club = new ClubEntities()) {
                    Article article = new Article() {
                        Title=Common.HtmlCommon.ClearHtml(title),
                        Context = Common.HtmlCommon.ClearJavascript(editor),
                        Tags = Common.HtmlCommon.ClearHtml(tags),
                        UserId=Convert.ToInt32(Request.Cookies["bwusers"]["id"].ToString()),
                        PutDate=DateTime.Now
                    };
                    club.Articles.Add(article);
                    if(club.SaveChanges()>=0){
                        hinStr="投稿成功，请等待审核！";
                        status=Status.success;
                        url = "/member/u-" + article.UserId + "/contributelist";
                    }else{
                        hinStr="系统异常，请尝试稍后投稿！";        
                    }
                }
            }
            return Json(new{state=status.ToString(),context=hinStr,url=url});
        }

        public ActionResult ContributeList(int? id,int? uid){
            List<Article> list = new List<Article>();
            using (club = new ClubEntities()){
                user = GetUser(club, uid);
                if (user == null)
                    return Redirect("/error/notfound");
                ViewBag.User = user;
                ViewBag.ContributeAllCount = club.Articles.Where(a => a.UserId == user.Id).Count();
                ViewBag.ContributeCheckedCount = club.Articles.Where(a => (a.Status > 0 && a.UserId == user.Id)).Count();
                list = club.Articles.Where(a => a.UserId == user.Id).OrderBy(a => a.Id).Take(ClubConst.MemberPageSize).ToList<Article>();
                ViewBag.PageStr = new PagingHelper(ClubConst.MemberPageSize,1, ViewBag.ContributeAllCount, ClubConst.MemberPageShow).GetPageStringPro("/member/u-" + user.Id + "/getcontributelist/");
            }
            ViewBag.User = user;
            return View(list);
        }

        [HttpGet]
        public JsonResult GetContributeList(int? id,int? uid){
            StringBuilder str = new StringBuilder();
            string pageString = string.Empty;
            int current = id ?? 1;
            using (club = new ClubEntities()) {
                user = GetUser(club, uid);
                ViewBag.User = user;
                ViewBag.ContributeAllCount = club.Articles.Where(a => a.UserId == user.Id).Count();
                var articles = club.Articles.Where(a => a.UserId == user.Id).OrderBy(a => a.Id).Skip((current - 1) * ClubConst.MemberPageSize).Take(ClubConst.MemberPageSize).ToList<Article>();
                pageString = new PagingHelper(ClubConst.MemberPageSize, current, ViewBag.ContributeAllCount, ClubConst.MemberPageShow).GetPageStringPro("/member/u-" + user.Id + "/getcontributelist/");
                foreach (var article in articles)
                    str.Append("<li><a href=\"\">" + article.Title + "</a><span>" + article.PutDate + "</span></li>");
            }
            return Json(new { context = str.ToString(), pagestr = pageString }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetContributeApproveList(int? id,int? uid){
            StringBuilder str = new StringBuilder();
            string pageString = string.Empty;
            int current = id ?? 1;
            using (club = new ClubEntities()){
                int checkedCount = 0;
                user = GetUser(club,uid);
                ViewBag.ContributeAllCount = club.Articles.Where(a => a.UserId == user.Id).Count();
                checkedCount = club.Articles.Where(a => (a.UserId == user.Id && a.Status >0)).Count();
                var articles = club.Articles.Where(a => a.UserId == user.Id && a.Status > 0).OrderBy(a => a.Id).Skip((current - 1) * ClubConst.MemberPageSize).Take(ClubConst.MemberPageSize).ToList<Article>();
                pageString = new PagingHelper(ClubConst.MemberPageSize, current, checkedCount, ClubConst.MemberPageShow).GetPageStringPro("/member/getcontributeapprovelist/");
                foreach (var article in articles)
                    str.Append("<li><a href=\"\">" + article.Title + "</a><span>" + article.PutDate + "</span></li>");
            }
            return Json(new { context = str.ToString(), pagestr = pageString }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 提问
        [HttpGet]
        public ActionResult Asks(int? uid){
            List<Question> list = new List<Question>();
            using (club = new ClubEntities()){
                user = GetUser(club, uid);
                if (user == null)
                    return Redirect("/error/notfound");
                ViewBag.User = user;
                ViewBag.AskAllCount = club.Questions.Where(a => a.UserId == user.Id).Count();
                list = club.Questions.Where(a => a.UserId == user.Id).OrderBy(a => a.Id).Take(ClubConst.MemberPageSize).ToList<Question>();
                ViewBag.PageStr = new PagingHelper(ClubConst.MemberPageSize, 1, ViewBag.AskAllCount, ClubConst.MemberPageShow).GetPageStringPro("/member/u-" + user.Id + "/getasks/");
            }
            return View(list);
        }

        [HttpGet]
        public JsonResult GetAsks(int? id,int? uid){
            StringBuilder str = new StringBuilder();
            string pageString = string.Empty;
            int current = id ?? 1;
            using (club = new ClubEntities()){
                user = GetUser(club,uid);
                ViewBag.AskAllCount = club.Questions.Where(a => a.UserId == user.Id).Count();
                var questions = club.Questions.Where(a => a.UserId == user.Id).OrderBy(a => a.Id).Skip((current - 1) * ClubConst.MemberPageSize).Take(ClubConst.MemberPageSize).ToList<Question>();
                pageString = new PagingHelper(ClubConst.MemberPageSize, current, ViewBag.AskAllCount, ClubConst.MemberPageShow).GetPageStringPro("/member/u-" + user.Id + "/getasks/");
                foreach (var ask in questions)
                    str.Append("<li><a href=\"/ask/show/"+ask.Id+"\">" + HtmlCommon.ClearHtml(ask.Title) + "</a><span>" + ask.VarDate + "</span></li>");
            }
            return Json(new { context = str.ToString(), pagestr = pageString }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Ask(int? uid){
            using (club = new ClubEntities()) {
                user = GetUser(club,uid);
            }
            ViewBag.User = user;
            if (user == null)
                Redirect("/error/notfound");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Ask(string title, string description, string tags) {
            string hinStr = string.Empty;
            Status status = Status.error;
            if (string.IsNullOrEmpty(title)){
                    hinStr = "请输入你要提出的问题!";
                }
                else{
                    using (club = new ClubEntities()){
                        user = GetUser(club);
                        Question ask = new Question(){
                            UserId=user.Id,
                            Title = Common.HtmlCommon.ClearHtml(title),
                            Description = Common.HtmlCommon.ClearHtml(description),
                            Flags = Common.HtmlCommon.ClearHtml(tags),
                            VarDate=DateTime.Now
                        };
                        club.Questions.Add(ask);
                        if (club.SaveChanges() > 0) { 
                            status = Status.success;
                        }   
                    }
                }
            return Json(new { state = status.ToString(), context = hinStr.Length>0?hinStr:HtmlCommon.GetHitStr(status) ,url="/member/u-"+user.Id+"/asks"});
        }

        #endregion

        #region 回答
        public ActionResult Answers(int? uid){
            List<Answer> list = new List<Answer>();
            using (club = new ClubEntities()){
                user = GetUser(club, uid);
                if (user == null)
                    return Redirect("/error/notfound");
                ViewBag.User = user;
                ViewBag.AnswerAllCount = club.Answers.Where(a => a.UserId == user.Id).Count();
                list = club.Answers.Where(a => a.UserId == user.Id).OrderBy(a => a.Id).Take(ClubConst.MemberPageSize).ToList<Answer>();
                ViewBag.PageStr = new PagingHelper(ClubConst.MemberPageSize, 1, ViewBag.AnswerAllCount, ClubConst.MemberPageShow).GetPageStringPro("/member/u-" + user.Id + "/getanswers/");
            }
            ViewBag.User = user;
            return View(list);
        }

        public JsonResult GetAnswers(int? id,int? uid) {
            StringBuilder str = new StringBuilder();
            string pageString = string.Empty;
            int current = id ?? 1;
            using (club = new ClubEntities()){
                user = GetUser(club,uid);
                ViewBag.AnswerAllCount = club.Answers.Where(a => a.UserId == user.Id).Count();
                var answsers = club.Answers.Where(a => a.UserId == user.Id).OrderBy(a => a.Id).Skip((current - 1) * ClubConst.MemberPageSize).Take(ClubConst.MemberPageSize).ToList<Answer>();
                pageString = new PagingHelper(ClubConst.MemberPageSize, current, ViewBag.AnswerAllCount, ClubConst.MemberPageShow).GetPageStringPro("/member/u-" + user.Id + "/getanswers/");
                foreach (var answser in answsers)
                    str.Append("<li><a href=\"/ask/show/" + answser.QId+ "\">" +HtmlCommon.ClearHtml(answser.Answer1) + "</a><span>" + answser.VarDate + "</span></li>");
            }
            return Json(new { context = str.ToString(), pagestr = pageString }, JsonRequestBehavior.AllowGet);            
        }
        #endregion

        #region 共享
        public ActionResult Shared(int? uid) {
            using (club = new ClubEntities())
            {
                user = GetUser(club, uid);
                var shared = club.SystemArticles.Where(s => s.Variables == "sys-info-shared"&&s.Status==1).FirstOrDefault();
                if (shared != null)
                    ViewBag.SharedDesc = shared.Text;
            }
            ViewBag.User = user;
            return View();
        }
        #endregion

        #region 会员认证
        public ActionResult Certification(int? uid)
        {
            using (club = new ClubEntities())
            {
                user = GetUser(club, uid);
                if (user == null)
                    return Redirect("/error/notfound");
                var certificationDesc = club.SystemArticles.Where(s => s.Variables == "sys-info-certification" && s.Status == 1).FirstOrDefault();
                if (certificationDesc != null)
                    ViewBag.CertificationDesc = certificationDesc.Text;
                ViewBag.User = user;
            }
            ViewBag.User = user;
            return View();
        }
        #endregion

        #region 基本资料
        [Authorize]
        [HttpPost]
        public JsonResult MBase(string realName, string phone, string address, string company, string intro) {
            string context = string.Empty;
            using (club = new ClubEntities()){
                user = GetUser(club);
                user.RealName = HtmlCommon.ClearHtml(realName);
                user.Phone = HtmlCommon.ClearHtml(phone);
                user.Address = HtmlCommon.ClearHtml(address);
                user.Company = HtmlCommon.ClearHtml(company);
                user.Intro = HtmlCommon.ClearHtml(intro);
                if (club.SaveChanges()>=0)
                    status = Status.success;
            }
            return Json(new { state = status.ToString(), context = HtmlCommon.GetHitStr(status, "基本资料更新.") });
        }

        [Authorize]
        [HttpPost]
        public JsonResult MChanagePwd(string oldpwd, string pwd, string pwd1){
            string txt = string.Empty;
            string _url=string.Empty;
            using (club = new ClubEntities()) {
                user = GetUser(club);
                if (FormsAuthentication.HashPasswordForStoringInConfigFile(oldpwd, "MD5") != user.Password){
                    txt = "原密码不正确！";
                }
                else {
                    if (pwd != pwd1){
                        txt = "两次密码输入不一样！";
                    }
                    else {
                        user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
                        if (club.SaveChanges() < 0){
                            txt = "系统异常，请稍后重试!";
                        }
                        else{
                            _url = "/account/loginout";
                            status = Status.success;
                        }
                    }
                }
            }
            return Json(new {state=status,context=txt,url=_url});
        }

        [Authorize]
        [HttpPost]
        public JsonResult MSetCover(string cover) {
            string context = string.Empty;
            if (!string.IsNullOrEmpty(cover)) {
                using (club = new ClubEntities()){
                    user = GetUser(club);
                    user.Cover = HtmlCommon.ClearHtml(cover);
                    if (club.SaveChanges()>=0)
                        status = Status.success;
                }
            }
            return Json(new { state = status.ToString(), context = HtmlCommon.GetHitStr(status, "头像设置."),src=cover });
        }

        #endregion

        #region 用户账号展示
        public ActionResult Show(int? uid) {
            int id = uid ?? 0;
            using(club=new ClubEntities()){
                user = club.Users.Where(u => u.Id == id).FirstOrDefault();
            }
            if (user == null)
                return Redirect("/error/notfound");
            ViewBag.User = user;
            return View("~/views/member/index.cshtml");
        }
        #endregion

        #region 论坛
        public ActionResult Discuss(int? uid) {
            using (club = new ClubEntities())
            {
                user = GetUser(club, uid);
                if (user == null)
                    return Redirect("/error/notfound");
                ViewBag.User = user;
            }
            ViewBag.User = user;
            return View("~/views/member/discuss.cshtml");
        }

        #endregion

        #region 信息中心
        public ActionResult Message(int? uid) {
            using (club = new ClubEntities())
            {
                user = GetUser(club, uid);
                if (user == null)
                    return Redirect("/error/notfound");
                ViewBag.User = user;
            }
            ViewBag.User = user;
            return View("~/views/member/message.cshtml");
        }
        #endregion

        [ChildActionOnly]
        public ActionResult MemberAvatarWrap(int? uid,string action) {
            int useId = uid ?? 0;
            using (club = new ClubEntities()) {
                if (useId != 0)
                    user = club.Users.Where(u => u.Id == useId).FirstOrDefault();
                else
                    user = GetUser(club);
            }
            if (user == null)
                return Redirect("/error/notfound");
            ViewBag.User = user;
            return PartialView("~/Views/Shared/_PartialMember.cshtml");
        }
        
        public User GetUser(ClubEntities c, int? uid) {
            int userId = uid ?? 0;
            user = c.Users.Where(u => u.Id == userId).FirstOrDefault();
            return user;
        }
                
        private User GetUser(ClubEntities c) {
            user = new User();
            if (Request.Cookies.AllKeys.Contains("bwusers") && !string.IsNullOrEmpty(Request.Cookies["bwusers"]["id"])){
                int userId = Convert.ToInt32(Request.Cookies["bwusers"]["id"]);
                user = club.Users.Single(u => u.Id == userId);
            }else{
                FormsAuthentication.SignOut();
                RedirectToAction("login", new { controller = "account" });
            }
            return user;
        }
    }
}
