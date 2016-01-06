using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;
using System.Web.Security;
using System.Text;

namespace BaWuClub.Web.Controllers
{
    public class AskController : BaseController
    {
        //
        // GET: /Ask/

        private ClubEntities club;
        private  Status status = Status.error;
        private string context = string.Empty;
        private BaWuClub.Web.Dal.User user;
        private AnswerVote answerVote;

        #region 问题列表
        public ActionResult Index() {
            List<ViewQuestion> vquestion = new List<ViewQuestion>();
            int rowsCount = 0;
            using (club = new ClubEntities()){
                rowsCount=club.ViewQuestions.Count();
                vquestion = club.ViewQuestions.OrderByDescending(q => q.VarDate).Take(ClubConst.WebQuestionPageSize).ToList<ViewQuestion>();
                ViewBag.newArticles = club.Articles.OrderByDescending(a => a.VarDate).Where(a=>a.Status>0).Take(7).ToList<Article>();
                ViewBag.hotQuestions = club.Questions.OrderByDescending(a => a.Views).Take(7).ToList<Question>();
            }
            ViewBag.PageStr = GetPageStr(ClubConst.WebQuestionPageSize, 1, rowsCount, ClubConst.WebQuestionPageShow, "/ask/list/", false);
            return View(vquestion);
        }
        
        [HttpGet]
        public ActionResult List(int? id) { 
            List<ViewQuestion> vquestion = new List<ViewQuestion>();
            int rowsCount = 0;
            int page = id ?? 1;
            using (club = new ClubEntities()){
                rowsCount = club.ViewQuestions.Count();
                ViewBag.newArticles = club.Articles.OrderByDescending(a => a.VarDate).Where(a=>a.Status>0).Take(7).ToList<Article>();
                ViewBag.hotQuestions=club.Questions.OrderByDescending(a=>a.Views).Take(7).ToList<Question>();
                vquestion = club.ViewQuestions.OrderByDescending(q=>q.VarDate).Skip((page-1)*ClubConst.WebPageSize).Take(ClubConst.WebPageSize).ToList<ViewQuestion>();
            }
            ViewBag.PageStr = GetPageStr(ClubConst.WebQuestionPageSize,page,rowsCount,ClubConst.WebQuestionPageShow,"/ask/list/",false);
            return View(vquestion);
        }
        #endregion

        #region 问题展示
        [HttpGet]
        public ActionResult Show(int? id,string sort) {
            ViewQuestion vq=new ViewQuestion();
            int qid = id ?? 0;
            ViewBag.CurrentUser = GetUser();
            using (club = new ClubEntities()) {
                vq = club.ViewQuestions.Where(q => q.Id == qid).FirstOrDefault();
                var question = club.Questions.Where(q => q.Id == qid).FirstOrDefault();
                if (question != null) {
                    question.Views += 1;
                }
                club.SaveChanges();
                ViewBag.AnswerCount = club.Answers.Where(a => a.QId == qid).Count();
                ViewBag.AnswerVote = club.AnswerVotes.Where(a => a.QId == qid).ToList<AnswerVote>();
                if(sort=="time")
                    ViewBag.ViewAnswers = club.ViewAnswers.OrderByDescending(q=>q.VarDate).Where(q => q.QId == qid).ToList<ViewAnswer>();
                else if (sort == "vote")
                    ViewBag.ViewAnswers = club.ViewAnswers.OrderByDescending(q =>(q.Agree+q.Oppose)).Where(q => q.QId == qid).ToList<ViewAnswer>();
                else
                    ViewBag.ViewAnswers = club.ViewAnswers.OrderByDescending(q => q.VarDate).Where(q => q.QId == qid).ToList<ViewAnswer>();
                if (vq == null)
                    return Redirect("/error/notfound");
                ViewBag.Title = vq.Title;
                ViewBag.OtherQuestions = club.Questions.Where(q => q.UserId == vq.UserId&&q.Id!=vq.Id).Take(6).ToList<Question>();
            }
            return View(vq);
        }

        public ActionResult Tags(int? tid,int? p) {
            int tId = tid ?? 0;
            List<ViewQuestion> vquestion = new List<ViewQuestion>();
            Tag tag = new Tag();
            int rowsCount = 0;
            int page = p ?? 1;
            using (club = new ClubEntities()){
                 tag = club.Tags.Where(t => t.Id == tid).FirstOrDefault();
                if (tag == null) { 
                   return RedirectToAction("NotFound", new { Controller = "Error" });
                }
                ViewBag.tagName = tag.TagName;
                ViewBag.newArticles = club.Articles.OrderByDescending(a => a.VarDate).Take(7).ToList<Article>();
                ViewBag.hotQuestions = club.Questions.OrderByDescending(a => a.Views).Take(7).ToList<Question>();
                rowsCount = club.ViewQuestions.OrderByDescending(q => q.VarDate).Where(t => t.Tags.Contains(tag.TagName)).Count();
                vquestion = club.ViewQuestions.OrderByDescending(q => q.VarDate).Where(t => t.Tags.Contains(tag.TagName)).Skip((page - 1) * ClubConst.WebPageSize).Take(ClubConst.WebPageSize).ToList<ViewQuestion>();
            }
            ViewBag.PageStr = GetPageStr(ClubConst.WebQuestionPageSize, page, rowsCount, ClubConst.WebQuestionPageShow, "/ask/tags/"+tId+"-", false);
            return View(vquestion);
        }
        #endregion

        #region 回答问题
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult AnswerQuestions(int id,string commentStr) {
            user = GetUser();
            Answer answer = new Answer();
            StringBuilder str = new StringBuilder();
            if (user == null || !User.Identity.IsAuthenticated)
                return Json(new {status=Status.warning.ToString(),url="/account/login?returnurl=/ask/show/"+id });
            using (club = new ClubEntities()) {
                answer.UserId = user.Id;
                answer.QId = id;
                answer.Answer1 = HtmlCommon.ClearJavascript(commentStr);
                answer.VarDate = DateTime.Now;
                answer.IP = Request.UserHostAddress;
                club.Answers.Add(answer);
                if (club.SaveChanges() >= 0) {
                    status = Status.success;
                    str.Append("<div class=\"comment-item\">");
                    str.Append("<div class=\"comment-item-info\">");
                    str.Append("<a href=\"/member/u-" + user.Id + "/show/\" class=\"comment-item-info-name\">" + user.NickName + "</a>");
                    str.Append("<a href=\"/member/u-" + user.Id + "/show/\" class=\"comment-item-info-avatar\"><img src=\""+(string.IsNullOrEmpty(user.Cover)?"/content/images/no-img.jpg":"/uploads/avatar/small/"+user.Cover)+"\"/>");
                    str.Append("</a>");
                    str.Append("</div>");
                    str.Append("<div class=\"comment-item-content\">"+HtmlCommon.ClearJavascript(commentStr)+"</div>");
                    str.Append("<div class=\"comment-item-meta\"><span>评论与"+HtmlCommon.GetAnswerTimeSpan(answer.VarDate)+"</span></div>");
                    str.Append("</div>");
                }
            }
            return Json(new { status = status.ToString(), context = str.ToString() });
        }

        #endregion

        #region 答案的投票
        [HttpGet]
        public JsonResult Vote(int qid,int aid,string vt) {
            Answer answer = new Answer();
            bool isCancel = false;
            int pro=0,con = 0;
            user = GetUser();
            if (user != null) { 
                using (club = new ClubEntities()) {
                    answer = club.Answers.Where(a => a.Id == aid).FirstOrDefault();
                    if (answer == null) {
                        context = "该问题不存在！";
                    }
                    else{
                        answerVote = club.AnswerVotes.Where(av => av.AId == aid && av.QId == qid && av.UId == user.Id).FirstOrDefault();
                        if (vt.Equals("up")) {
                            if (answerVote == null) {
                                club.AnswerVotes.Add(CreateVote(aid,qid,user.Id,1));
                                answer.Agree = answer.Agree + 1;
                            }
                            else {
                                if (answerVote.Vote == 0) {
                                    answerVote.Vote = 1;
                                    answer.Oppose = (answer.Oppose - 1) > 0 ? (answer.Oppose - 1) : 0;
                                    answer.Agree = answer.Agree + 1;
                                }
                                else {
                                    club.AnswerVotes.Remove(answerVote);
                                    answer.Agree = (answer.Agree - 1) > 0 ? (answer.Agree - 1) : 0;
                                    isCancel = true;
                                }
                            }
                        }
                        if (vt.Equals("down")) {
                            if (answerVote == null) {
                                club.AnswerVotes.Add(CreateVote(aid,qid,user.Id,0));
                                answer.Oppose = answer.Oppose + 1;
                            }
                            else {
                                if (answerVote.Vote == 1) {
                                    answerVote.Vote = 0;
                                    answer.Agree = (answer.Agree - 1) > 0 ? (answer.Agree - 1) : 0;
                                    answer.Oppose = answer.Oppose + 1;
                                }
                                else {
                                    club.AnswerVotes.Remove(answerVote);
                                    answer.Oppose = (answer.Oppose - 1) > 0 ? (answer.Oppose - 1) : 0;
                                    isCancel = true;
                                }
                            }
                        }
                        if(club.SaveChanges()>=0){
                            status=Status.success;
                            context="操作成功！";
                        }
                        pro = answer.Agree;
                        con = answer.Oppose;
                    }
                }
            }
            else {
                return Json(new {status=Status.warning.ToString(),url="/account/login?returnurl=/ask/show/"+qid },JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = status.ToString(), context = context,iscancel=isCancel,pro=pro,con=con,op=vt }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        private AnswerVote CreateVote(int aId,int qId,int uId,int vote) {
            return answerVote = new AnswerVote() { QId=qId,AId=aId,UId=uId,Vote=(byte)vote};
        }

        #region 分页
        private string GetPageStr(int pageSize, int currentPage, int rowsCount, int pageShow, string url, bool openAajx = true) {
            return new PagingHelper(pageSize, currentPage, rowsCount, pageShow).GetPageStringPro(url, openAajx);
        }
        #endregion
    }
}
