using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace BaWuClub.Web.Controllers
{
    public class MemberController : MemberBaseController
    {
        #region
        private ClubEntities club;
        private BaWuClub.Web.Dal.User user;
        private Status status = Status.error;
        private string hintStr = string.Empty;
        private string pageString = string.Empty;
        StringBuilder str = new StringBuilder();
        #endregion

        #region get
        public ActionResult Show(int? uid)
        {
            int id = uid ?? 0;
            using (club = new ClubEntities()){
                user=GetUser(club,uid);
            }
            if (user == null)
                return Redirect("/error/notfound");
            ViewBag.user = user;
            return View("~/views/member/index.cshtml");
        }

        [Authorize]
        public ActionResult Contribute(int? uid){
            using (club = new ClubEntities()) {
                user = GetUser(club, uid);
            }
            ViewBag.user = user;
            return View();
        }

        public ActionResult ContributeList(int? uid){
            List<Article> list = new List<Article>();
            using (club = new ClubEntities()){
                user = GetUser(club, uid);
                if (user == null)
                    return Redirect("/error/notfound");
                ViewBag.ContributeAllCount = club.Articles.Where(a => a.UserId == user.Id).Count();
                ViewBag.ContributeCheckedCount = club.Articles.Where(a => (a.Status > 0 && a.UserId == user.Id)).Count();
                list = club.Articles.Where(a => a.UserId == user.Id).OrderBy(a => a.Id).Take(ClubConst.MemberPageSize).ToList<Article>();
                ViewBag.PageStr = new PagingHelper(ClubConst.MemberPageSize, 1, ViewBag.ContributeAllCount, ClubConst.MemberPageShow).GetPageStringPro("/member/u-" + user.Id + "/getlist?tstr=column&page=");
            }
            ViewBag.user = user;
            return View(list);
        }

        public ActionResult Certification(int? uid)
        {
            using (club = new ClubEntities()){
                user = GetUser(club, uid);
                var certificationDesc = club.SystemArticles.Where(s => s.Variables == "sys-info-certification" && s.Status == 1).FirstOrDefault();
                if (certificationDesc != null)
                    ViewBag.CertificationDesc = certificationDesc.Text;
            }
            if (user == null)
                return Redirect("/error/notfound");
            ViewBag.user = user;
            return View();
        }

        [HttpGet]
        public ActionResult AskAndAnswer(int? uid)
        {
            List<Question> list = new List<Question>();
            using (club = new ClubEntities()){
                user = GetUser(club, uid);
                ViewBag.AskAllCount = club.Questions.Where(a => a.UserId == user.Id).Count();
                list = club.Questions.Where(a => a.UserId == user.Id).OrderBy(a => a.Id).Take(ClubConst.MemberPageSize).ToList<Question>();
                ViewBag.PageStr = new PagingHelper(ClubConst.MemberPageSize, 1, ViewBag.AskAllCount, ClubConst.MemberPageShow).GetPageStringPro("/member/u-" + user.Id + "/getlist?tstr=ask&page=");
            }
            if (user == null)
                return Redirect("/error/notfound");
            ViewBag.user = user;
            return View(list);
        }

        public ActionResult Shared(int? uid)
        {
            List<Document> list = new List<Document>();
            using (club = new ClubEntities()){
                user = GetUser(club, uid);
                var shared = club.SystemArticles.Where(s => s.Variables == "sys-info-shared" && s.Status == (int)State.Enable).FirstOrDefault();
                ViewBag.docsCount = club.Documents.Where(d => d.UserId == uid).Count();
                ViewBag.docsCheckedCount = club.Documents.Where(d => d.UserId == uid&&d.Status==(int)State.Enable).Count();
                list = club.Documents.Where(d => d.UserId == uid).Take(ClubConst.MemberPageSize).ToList<Document>();
                ViewBag.SharedDesc =shared != null? shared.Text:"";
                ViewBag.PageStr = new PagingHelper(ClubConst.MemberPageSize, 1, ViewBag.docsCount, ClubConst.MemberPageShow).GetPageStringPro("/member/u-" + user.Id + "/getlist?tstr=shared&page=");
            }
            ViewBag.user = user;
            return View(list);
        }

        public ActionResult Discuss(int? uid){
            using (club = new ClubEntities()){
                user = GetUser(club, uid);
            }
            if (user == null)
                return Redirect("/error/notfound");
            ViewBag.user = user;
            return View("~/views/member/discuss.cshtml");
        }

        [Authorize]
        public ActionResult Message(int? uid)
        {
            user = (User)ViewBag.userAuthorize;
            List<Message> list = new List<Message>();
            using (club = new ClubEntities()) {
                ViewBag.msgCount = club.Messages.Where(d => d.ToId== uid).Count();
                ViewBag.unReadMsgCount = club.Messages.Where(d => d.ToId == uid && d.Status == 0).Count();
                list = club.Messages.Where(m => m.ToId == user.Id).Take(ClubConst.MemberPageSize).ToList<Message>();
                ViewBag.PageStr = new PagingHelper(ClubConst.MemberPageSize, 1, ViewBag.msgCount, ClubConst.MemberPageShow).GetPageStringPro("/member/u-" + user.Id + "/getlist?tstr=message&page=");
            }
            ViewBag.user = user;
            return View("~/views/member/message.cshtml",list);
        }
        #endregion

        #region post

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public JsonResult Contribute(string title,string tags,string editor){
            string url = string.Empty;
            if (string.IsNullOrEmpty(title)||string.IsNullOrEmpty(editor)) {
                hintStr = "投稿的标题和内容均不能为空，请填写标题后再提交！";
            }else{
                using (club = new ClubEntities()) {
                    Article article = new Article() {
                        Title=Common.HtmlCommon.ClearHtml(title),
                        Context = Common.HtmlCommon.ClearJavascript(editor),
                        Tags = Common.HtmlCommon.ClearHtml(tags),
                        UserId=Convert.ToInt32(Request.Cookies["bwusers"]["id"].ToString()),
                        VarDate=DateTime.Now
                    };
                    club.Articles.Add(article);
                    if(club.SaveChanges()>=0){
                        hintStr = "投稿成功，请等待审核！";
                        status=Status.success;
                        url = "/member/u-" + article.UserId + "/contributelist";
                    }else{
                        hintStr = "系统异常，请尝试稍后投稿！";        
                    }
                }
            }
            return Json(new{state=status.ToString(),context=hintStr,url=url});
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Ask(string title, string description, string tags) {
            user = (BaWuClub.Web.Dal.User)ViewBag.userAuthorize;
            if (string.IsNullOrEmpty(title))
            {
                hintStr = "请输入你要提出的问题!";
            }
            else
            {
                using (club = new ClubEntities())
                {
                    Question ask = new Question()
                    {
                        UserId = user.Id,
                        Title = Common.HtmlCommon.ClearHtml(title),
                        Description = Common.HtmlCommon.ClearHtml(description),
                        Flags = Common.HtmlCommon.ClearHtml(tags),
                        VarDate = DateTime.Now
                    };
                    club.Questions.Add(ask);
                    if (club.SaveChanges() > 0)
                    {
                        status = Status.success;
                    }
                }
            }
            return Json(new { state = status.ToString(), context = hintStr.Length > 0 ? hintStr : HtmlCommon.GetHitStr(status), url = "/member/u-" + user.Id + "/asks" });
        }

        [Authorize]
        [HttpPost]
        public JsonResult MChanagePwd(string oldpwd, string pwd, string pwd1){
            string _url = string.Empty;
            user = (BaWuClub.Web.Dal.User)ViewBag.userAuthorize;
            using (club = new ClubEntities()) {
                if (FormsAuthentication.HashPasswordForStoringInConfigFile(oldpwd, "MD5") != user.Password){
                    hintStr = "原密码不正确！";
                }
                else {
                    if (pwd != pwd1){
                        hintStr = "两次密码输入不一样！";
                    }
                    else {
                        user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
                        if (club.SaveChanges() < 0){
                            hintStr = "系统异常，请稍后重试!";
                        }
                        else{
                            _url = "/account/loginout";
                            status = Status.success;
                        }
                    }
                }
            }
            return Json(new { state = status, context = hintStr, url = _url });
        }

        [Authorize]
        [HttpPost]
        public JsonResult MSetCover(string cover) {
            user = (BaWuClub.Web.Dal.User)ViewBag.userAuthorize;
            if (!string.IsNullOrEmpty(cover)) {
                using (club = new ClubEntities()){
                    user.Cover = HtmlCommon.ClearHtml(cover);
                    if (club.SaveChanges()>=0)
                        status = Status.success;
                }
            }
            return Json(new { state = status.ToString(), context = HtmlCommon.GetHitStr(status, "头像设置."),src=cover });
        }

        [Authorize]
        [HttpPost]
        public JsonResult MBase(string realName, string phone, string address, string company, string intro){
            user = (BaWuClub.Web.Dal.User)ViewBag.userAuthorize;
            using (club = new ClubEntities()) {
                user.RealName = HtmlCommon.ClearHtml(realName);
                user.Phone = HtmlCommon.ClearHtml(phone);
                user.Address = HtmlCommon.ClearHtml(address);
                user.Company = HtmlCommon.ClearHtml(company);
                user.Intro = HtmlCommon.ClearHtml(intro);
                if (club.SaveChanges() >= 0)
                    status = Status.success;
            }
            return Json(new { state = status.ToString(), context = HtmlCommon.GetHitStr(status, "基本资料更新.") });
        }
        #endregion

        #region jsonresult
        public JsonResult SetMsgRead(int id) {
            string context = string.Empty;
            string url = "";
            user = (BaWuClub.Web.Dal.User)ViewBag.userAuthorize;
            if (user == null) {
                status = Status.warning;
                context = "账号未登录！";
                url="/account/login";;
            }
            else {
                using (club = new ClubEntities()) {
                    var msg = club.Messages.Where(m => m.Id == id).FirstOrDefault();
                    msg.Status = 1;
                    if (club.SaveChanges() >= 0) {
                        status = Status.success;
                    }
                }
            }
            return Json(new { status=status.ToString(),context=context,url=url},JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetMsgDel(int id) {
            string context = string.Empty;
            string url = "";
            user = (BaWuClub.Web.Dal.User)ViewBag.userAuthorize;
            if (user == null) {
                status = Status.warning;
                context = "账号未登录！";
                url="/account/login";;
            }
            else {
                using (club = new ClubEntities()) {
                    var msg = club.Messages.Where(m => m.Id == id).FirstOrDefault();
                    club.Messages.Remove(msg);
                    if (club.SaveChanges() >= 0) {
                        status = Status.success;
                    }
                }
            }
            return Json(new { status=status.ToString(),context=context,url=url},JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region getlist
        public JsonResult GetList(string tstr,int? uid,int? page,string st) {
            string json = string.Empty;
            int tId = uid ?? 0;
            int currentId = page ?? 1;
            string url = "/"+tstr+"/show/";
            using (club = new ClubEntities()) {
                switch (tstr) { 
                    case "ask":
                        var alist = GetList<Question>(club.Questions, currentId, q => q.UserId == tId, q => q.Id, out pageString, "/member/u-"+tId+"/getlist?tstr=ask&page=");
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(alist);
                        break;
                    case "column":
                        Expression<Func<Article,bool>> Aexpression=null;
                        if (!string.IsNullOrEmpty(st) && st == "1")
                            Aexpression = a => a.Status == 1 && a.UserId == tId;
                        var clist = GetList<Article>(club.Articles, currentId, (Aexpression != null ? Aexpression : (a => a.UserId == tId)), a => a.Id, out pageString, "/member/u-" + tId + "/getlist?st="+st+"&tstr=column&page=");
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(clist);
                        break;
                    case "shared":
                        url = "/download/item/";
                        Expression<Func<Document, bool>> Sexpression = null;
                        if (!string.IsNullOrEmpty(st) && st == "1")
                            Sexpression = s=> s.Status == 1 && s.UserId == tId;
                        var slist = GetList<Document>(club.Documents, currentId, (Sexpression != null ? Sexpression : (s => s.UserId == tId)), s => s.Id, out pageString, "/member/u-" + tId + "/getlist?st="+st+"&tstr=shared&page=");
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(slist);
                        break;
                    case "message":
                        Expression<Func<Message, bool>> Mexpression = null;
                        if (!string.IsNullOrEmpty(st) && st == "0")
                            Mexpression = m => m.ToId == tId&&m.Status==0;
                        var mlist = GetList<Message>(club.Messages, currentId, (Mexpression != null ? Mexpression : (s => s.ToId == tId)), s => s.Id, out pageString, "/member/u-" + tId + "/getlist?st="+st+"&tstr=message&page=");
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(mlist);
                        break;
                    case "forum":
                        Expression<Func<ViewTopicIndex, bool>> Texpression = null;
                        int sint=0;
                        if (!string.IsNullOrEmpty(st) && Int32.TryParse(st, out sint)){                            
                            Texpression = t => t.Type == sint && t.UserId == tId;
                            url = "/forum/" + ((TopicType)sint).ToString().ToLower() + "/";
                        }
                        var tlist = GetList<ViewTopicIndex>(club.ViewTopicIndexes, currentId, (Texpression != null ? Texpression : (s => s.UserId == tId)), s => s.Id, out pageString, "/member/u-" + tId + "/getlist?st=" + st + "&tstr=forum&page=");
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(tlist);
                        break;
                }
            }
            ViewBag.pageStr = pageString;
            status = Status.success;
            return Json(new { status = status.ToString(), pagestr = pageString, url = url, context = json }, JsonRequestBehavior.AllowGet);
        }

        private List<T> GetList<T>(DbSet<T> ds, int page, Expression<Func<T, bool>> express, Expression<Func<T,int>> orderByExpress,out string pageStr,string url) where T : class
        {
            var list = ds.Where(express).OrderBy(orderByExpress).Skip((page - 1) * ClubConst.MemberPageSize).Take(ClubConst.MemberPageSize).ToList<T>();
            pageStr = new PagingHelper(ClubConst.MemberPageSize, page, ds.Where(express).Count(), ClubConst.MemberPageShow).GetPageStringPro(url);
            return list;
        }
        #endregion

        #region private
        [ChildActionOnly]
        public ActionResult MemberAvatarWrap(int? uid,string action) {
            int useId = uid ?? 0;
            using (club = new ClubEntities()) {
                if (useId != 0)
                    user = club.Users.Where(u => u.Id == useId).FirstOrDefault();
                else
                    user = GetUser();
            }
            if (user == null)
                return Redirect("/error/notfound");
            ViewBag.User = user;
            return PartialView("~/Views/Shared/_PartialMember.cshtml");
        }
        
        private User GetUser(ClubEntities c, int? uid) {
            int userId = uid ?? 0;
            user = c.Users.Where(u => u.Id == userId).FirstOrDefault();
            return user;
        }
        #endregion
    }
}
