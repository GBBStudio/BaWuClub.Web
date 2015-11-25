using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;
using System.Transactions;
using System.Text;
using Newtonsoft.Json;
using System.Reflection;
using System.Linq.Expressions;

namespace BaWuClub.Web.Controllers
{
    public class ForumController : BaseController
    {
        #region Define
        private ClubEntities club;
        private int tId = 0;
        private string postTransfersUrl = "~/views/direct/transfers.cshtml";
        private IQueryable<ViewTopicIndex> queryable;
        private delegate void GetRightListEventHandler(ClubEntities c);
        private event GetRightListEventHandler GetRight;
        #endregion

        #region Get
        public ActionResult Index()
        {
            ViewBag.Categories=GetDiscussCategory();
            List<ViewTopicIndex> viewTopicsIndex = new List<ViewTopicIndex>();
            using (club = new ClubEntities()) {
                queryable = GetQueryable(club);
                GetCount(queryable);
                viewTopicsIndex = queryable.Take(ClubConst.TopicPageSize).Where(t => t.Area == 0).ToList<ViewTopicIndex>();
                int count = queryable.Where(t => t.Area == 0).Count();
                ViewBag.pageStr = HtmlCommon.GetPageStrPro("/forum/p/", 13, 1, count, ClubConst.TopicPageShow);
            }
            return View(viewTopicsIndex);
        }

        public ActionResult P(int? id)
        {
            tId = id ?? 1;
            List<TopicCategory> categories = GetDiscussCategory();
            ViewBag.Categories = categories;
            List<ViewTopicIndex> viewTopicsIndex = new List<ViewTopicIndex>();
            using (club = new ClubEntities()) {
                queryable = GetQueryable(club);
                GetCount(queryable);
                viewTopicsIndex = queryable.OrderByDescending(t => t.VarDate).Skip(ClubConst.TopicPageSize * (tId - 1)).Take(ClubConst.TopicPageSize).Where(t => t.Area == 0).ToList<ViewTopicIndex>();
                int count = queryable.Where(t => t.Area == 0).Count();
                ViewBag.pageStr = HtmlCommon.GetPageStrPro("/forum/p/", 13, tId, count, ClubConst.TopicPageShow);
            }
            return View("~/views/forum/index.cshtml", viewTopicsIndex);
        }

        public ActionResult Cliques() {
            List<TopicIndex> topics = new List<TopicIndex>();
            using (club = new ClubEntities()) {
                ViewBag.banners = club.ViewBanners.Where(b => b.Status == (int)State.Enable && b.Variables == "sys-bt-cliques-top").ToList<ViewBanner>();
                ViewBag.topics = club.TopicIndexes.Include("TopicActivity").Take(4).Where(t => t.Status != 0 && t.Type == ((int)TopicType.Activity)).ToList<TopicIndex>();
                ViewBag.activityings = club.TopicIndexes.Take(4).Where(t => t.Status != 0 && t.Type == ((int)TopicType.Activity) && t.TopicActivity.EndDate > DateTime.Now).ToList<TopicIndex>();
                ViewBag.activityeds = club.TopicIndexes.Take(4).Where(t => t.Status != 0 && t.Type == ((int)TopicType.Activity) && t.TopicActivity.EndDate < DateTime.Now).ToList<TopicIndex>();
                ViewBag.taskCount = club.ViewTopicTasks.Where(t => t.Status != 0 && t.Type == ((int)TopicType.Task)).Count();
                ViewBag.tasks = club.ViewTopicTasks.Take(5).Where(t => t.Status != 0 && t.Type == ((int)TopicType.Task)).ToList<ViewTopicTask>();
                ViewBag.tasked = club.ViewTopicTasks.Take(4).Where(t => t.Status == 2 && t.Type == ((int)TopicType.Task)).ToList<ViewTopicTask>();
            }
            return View("~/views/forum/cliques.cshtml");
        }

        public ActionResult Topics(string name,int? page,string sort){
            tId=page??1;
            if (string.IsNullOrEmpty(name)) {
                return RedirectToAction("notfound", "error");
            }
            var categories = GetDiscussCategory();
            var category = categories.Where(t => t.Variable == name).FirstOrDefault();
            if (category == null)
                return RedirectToAction("notfound", "error");
            ViewBag.Categories = categories;
            ViewBag.category = category;
            List<ViewTopicIndex> topics = new List<ViewTopicIndex>();
            using (club = new ClubEntities()) {
                queryable= GetQueryable(club);
                GetCount(queryable);
                if(!string.IsNullOrEmpty(sort)&&sort=="time")
                    topics = queryable.OrderByDescending(t => t.VarDate).Skip(ClubConst.TopicPageSize * (tId - 1)).Take(ClubConst.TopicPageSize).Where(t => t.Category == category.Id).ToList<ViewTopicIndex>();
                else
                    topics = queryable.OrderByDescending(t => t.Id).Skip(ClubConst.TopicPageSize * (tId - 1)).Take(ClubConst.TopicPageSize).Where(t => t.Category == category.Id).ToList<ViewTopicIndex>();
                int count = queryable.Where(t => t.Category == category.Id).Count();
                ViewBag.pageStr = HtmlCommon.GetPageStrPro("/forum/topics/" + name + "?sort="+sort+"&page=", ClubConst.TopicPageSize, tId, count,ClubConst.TopicPageShow);
            }
            return View("~/views/forum/index.cshtml",topics);
        }
        
        [HttpGet]
        public ActionResult TopicList(string type,int? page) {
            tId = page ?? 1;
            if (string.IsNullOrEmpty(type)) {
                return RedirectToAction("index", "forum");
            }
            type = type.Substring(0, 1).ToUpper() + type.Substring(1, type.Length - 1);
            TopicType tt = TopicType.Topic;
            var reslut=Enum.TryParse<TopicType>(type, out tt);
            if (!reslut)
            {
                return RedirectToAction("notfound", "error");
            }
            var categories = GetDiscussCategory();
            ViewBag.Categories = categories;
            List<ViewTopicIndex> topics = new List<ViewTopicIndex>();
            using (club = new ClubEntities()) {
                queryable= GetQueryable(club);
                GetCount(queryable);
                topics = queryable.OrderBy(t => t.VarDate).Skip(ClubConst.TopicPageSize * (tId - 1)).Take(ClubConst.TopicPageSize).Where(t => t.Type == ((int)tt)).ToList<ViewTopicIndex>();
                int count = queryable.Where(t => t.Type == ((int)tt)).Count();
                ViewBag.pageStr = HtmlCommon.GetPageStrPro("/forum/topiclist?type=" + tt.ToString() + "&page=", ClubConst.TopicPageSize, tId, count,ClubConst.TopicPageShow);
            }
            return View("~/views/forum/index.cshtml", topics);
        }   
        #endregion

        #region show
        public ActionResult Topic(int? id)
        {
            tId = id ?? 0;
            ViewTopic topic = new ViewTopic();
            using (club = new ClubEntities())
            {
                topic = club.ViewTopics.Where(t => t.Id == tId).FirstOrDefault();
                GetRightListManage();
                GetRight -= GetRightListTopic;
                GetRight(club);
                if (topic == null){
                    return RedirectToAction("notfound", "error");
                }
                SetViews(club, tId);
            }
            return View("~/views/forum/topic.cshtml", topic);
        }

        public ActionResult Task(int? id)
        {
            tId = id ?? 0;
            ViewTopicTask topic = new ViewTopicTask();
            BaWuClub.Web.Dal.User user = GetUser();
            using (club = new ClubEntities())
            {
                topic = club.ViewTopicTasks.Where(t => t.Id == tId).FirstOrDefault();
                GetRightListManage();
                GetRight -= GetRightListTask;
                GetRight(club);
                GetInvolvedList(club);
                ViewBag.isJoined = IsCanJoin(user, club, tId);
                if (topic == null)
                {
                    return RedirectToAction("notfound", "error");
                }
                SetViews(club, tId);
            }
            return View("~/views/forum/task.cshtml", topic);
        }

        public ActionResult Activity(int? id){
            tId = id ?? 0;
            ViewTopicActivity topic = new ViewTopicActivity();
            BaWuClub.Web.Dal.User user = GetUser();
            using (club = new ClubEntities()){
                topic = club.ViewTopicActivities.Where(t => t.Id == tId).FirstOrDefault();
                GetInvolvedList(club);
                GetRightListManage();
                GetRight -= GetRightListActivity;
                GetRight(club);
                ViewBag.isJoined = IsCanJoin(user, club, tId);
                if (topic == null)
                {
                    return RedirectToAction("notfound", "error");
                }
                SetViews(club, tId);
            }
            return View("~/views/forum/activity.cshtml", topic);
        }

        #endregion

        #region Post
        
        [HttpPost]
        public JsonResult Take(int id,int tId,string s) {
            Status status = Status.error;
            string context = string.Empty;
            BaWuClub.Web.Dal.User user = GetUser();
            BaWuClub.Web.Dal.User toUser = new User();
            if (user == null||!User.Identity.IsAuthenticated) {
                return Json(new { status=Status.warning.ToString(),context=context,url="/Account/login?returnurl="});
            }
            using (club = new ClubEntities()) {
                TopicInvolved topicInvolved=new TopicInvolved(){TopicId=id,UserId=user.Id,Ip=Request.UserHostAddress,Vardate=DateTime.Now};
                TopicIndex topic=club.TopicIndexes.Where(t=>t.Id==id).FirstOrDefault();
                if (club.TopicInvolveds.Where(t => t.TopicId == topicInvolved.TopicId && t.UserId == topicInvolved.UserId).Count() > 0) {
                    context = "您已经参加过了哦！";
                }else{
                    if(topic!=null){
                        toUser = club.Users.Where(u => u.Id == tId).FirstOrDefault();
                        club.TopicInvolveds.Add(topicInvolved);
                        if (club.SaveChanges() > 0) {
                            if (s == "activity") {
                                SendFormatMsg(user.Id, toUser.Id,"0", user.NickName, topic.Title, Request.UserHostAddress);
                                SendFormatMsg(toUser.Id, user.Id,"2", toUser.NickName, topic.Title, Request.UserHostAddress);
                            }
                            else {
                                SendFormatMsg(user.Id, toUser.Id, "1", user.NickName, topic.Title, Request.UserHostAddress);
                                SendFormatMsg(toUser.Id, user.Id, "3", toUser.NickName, topic.Title, Request.UserHostAddress);
                            }
                            status = Status.success;
                            object rsltUser= new { name = user.NickName, id = user.Id, cover = user.Cover };
                            context = JsonConvert.SerializeObject(rsltUser);
                        }
                    }
                    else { 
                        context = "操作异常，请稍后重试！";
                    }
                }
            }
            return Json(new {status=status.ToString(),context=context });
        }

        [Authorize]
        [HttpGet]
        public ActionResult SponsorActivity()
        {
            using (club = new ClubEntities()) {
                GetRightListManage();
                GetRight -= GetRightListActivity;
                GetRight(club);
            }
            return View("~/views/forum/sponsoractivity.cshtml");
        }

        [Authorize]
        public ActionResult SponsorTask()
        {            
            using (club = new ClubEntities()) {
                GetRightListManage();
                GetRight -= GetRightListTask;
                GetRight(club);
            }
            return View("~/views/forum/sponsortask.cshtml");
        }

        [HttpGet]
        [Authorize]
        public ActionResult PostTopic()
        {
            using (club = new ClubEntities()) {
                GetRightListManage();
                GetRight -= GetRightListTopic;
                GetRight(club);
            }
            ViewBag.categoryList = GetDiscussCategory();
            return View("~/views/forum/posttopic.cshtml");
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PostTask(string title, string context)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(context)){
                ViewBag.tips = "标题与内容均不能为空！";
            }
            else
            {
                BaWuClub.Web.Dal.User user = GetUser();
                TopicIndex topicIndex = new TopicIndex()
                {
                    Title = HtmlCommon.ClearHtml(title),
                    UserId = user.Id,
                    Type = (int)TopicType.Task,
                    Status=(int)State.Enable,
                    VarDate = DateTime.Now
                };
                TopicTask task = new TopicTask()
                {
                    Context = HtmlCommon.ClearJavascript(context)
                };
                topicIndex.TopicTask = task;
                using (club = new ClubEntities())
                {
                    club.TopicIndexes.Add(topicIndex);
                    if (club.SaveChanges() < 0)
                    {
                        return Redirect("/error/exception");
                    }
                    else
                    {
                        return View("~/views/forum/topictransfers.cshtml");
                    }
                }
            }
            return View("~/views/forum/sponsortask.cshtml");
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PostTopic(string title, int category, string context) {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(context)){
                ViewBag.hitStr = "标题与内容不能为空！";
                ViewBag.categoryList = GetDiscussCategory();
                postTransfersUrl = "~/views/forum/posttopic.cshtml";
            }
            else if (category == 0) {
                ViewBag.hitStr = "请选择帖子分类！";
                ViewBag.categoryList = GetDiscussCategory();
                postTransfersUrl = "~/views/forum/posttopic.cshtml";
            }
            else
            {
                BaWuClub.Web.Dal.User user = GetUser();
                CheckUser(user);
                TopicIndex topicIndex = new TopicIndex() { Title = HtmlCommon.ClearHtml(title),  Status=(int)State.Enable,Category = category, UserId = user.Id, VarDate = DateTime.Now };
                BaWuClub.Web.Dal.Topic topic = new Topic() { Context = context };
                topicIndex.Topic = topic;
                using (club = new ClubEntities())
                {
                    club.TopicIndexes.Add(topicIndex);
                    if (club.SaveChanges() > 0)
                        ViewBag.url = "/forum/topic/" + topicIndex.Id;
                }
            }
            return View(postTransfersUrl);
        }

        [ValidateInput(false)]
        [HttpPost]
        [Authorize]
        public ActionResult PostActivity(string title, int city, string address, string pic, string cost, string contact, string phone, string sponsor, string context, DateTime start, DateTime end)
        {
            BaWuClub.Web.Dal.User user = GetUser();
            CheckUser(user);
            TopicIndex topicIndex = new TopicIndex()
            {
                Title = HtmlCommon.ClearHtml(title),
                VarDate = DateTime.Now,
                Type = (Int32)TopicType.Activity,//
                Status=(int)State.Enable,
                UserId = user.Id
            };
            TopicActivity topicActivity = new TopicActivity()
            {
                Address = HtmlCommon.ClearHtml(address),
                Context = context,
                City = city,
                Cover = pic,
                Cost = HtmlCommon.ClearHtml(cost),
                Contact = HtmlCommon.ClearHtml(contact),
                Phone = HtmlCommon.ClearHtml(phone),
                Sponsor = HtmlCommon.ClearHtml(sponsor),
                StartDate = start,
                EndDate = end
            };
            topicIndex.TopicActivity = topicActivity;
            using (club = new ClubEntities())
            {
                club.TopicIndexes.Add(topicIndex);
                if (club.SaveChanges() < 0)
                    return RedirectToAction("error", "error");
                ViewBag.url = "/forum/";
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult PostReviews(int id,string context) {
            Status status = Status.error;
            BaWuClub.Web.Dal.User user=GetUser();
            if (user == null){
                return Json(new { status = Status.warning.ToString(), url = "/account/login" });
            }
            TopicReview review = new TopicReview(){TopicId=id,UserId=user.Id, Reviews=HtmlCommon.ClearJavascript(context),VarDate=DateTime.Now};
            StringBuilder str=new StringBuilder();
            using (club = new ClubEntities()) {
                club.TopicReviews.Add(review);
                if (club.SaveChanges()>=0) {
                    str.Append("<li>");
                    str.Append("<div class=\"reviews-cover fleft\"><a href=\"/member/u-" + user.Id + "/show\"><img src=\"" + (!string.IsNullOrEmpty(user.Cover)? "/uploads/avatar/small/" + user.Cover : "/Content/Images/no-img.jpg") + "\" /></a> </div>");
                    str.Append("<div class=\"reviews-content\"><div class=\"reviews-info\"><a href=\"/member/u-" + user.Id + "/show\">" + user.NickName + "</a><span>" + ((DateTime)review.VarDate).ToString("yyyy年mm月dd日") + "</span></div>");
                    str.Append("<div class=\"reviews-text\">" + review.Reviews + "</div>");
                    str.Append("<div class=\"reviews-btns\">");
                    str.Append("<a href=\"#\" style=\"display:none\">回复</a>");
                    str.Append("<a href=\"#\"  style=\"display:none\">赞</a></div></div>");
                    str.Append("</li>");
                    status = Status.success;               
                }
            }
            return Json(new { status = status.ToString(),context=str.ToString()});
        }
        #endregion

        #region private

        private void GetRightListManage() {
            GetRight+= GetRightListActivity;
            GetRight += GetRightListTask;
            GetRight += GetRightListTopic;
        }

        private List<T> GetRightList<T>(IQueryable<T> queryable,Expression<Func<T,int>> expression,Expression<Func<T,bool>> whereExpression) {
            List<T> list = new List<T>();
            if(whereExpression!=null)
                list = queryable.Where(whereExpression).OrderBy(expression).Take(6).ToList<T>();
            else
                list=queryable.OrderBy(expression).Take(6).ToList<T>();
            return list;
        }

        private void GetRightListTask(ClubEntities c) {
            ViewBag.taskRightList = GetRightList<TopicIndex>(c.TopicIndexes, t => t.Id,t=>t.Type==(int)TopicType.Task);
        }

        private void GetRightListTopic(ClubEntities c) { 
            ViewBag.topicRightList = GetRightList<TopicIndex>(c.TopicIndexes, t => t.Id,t=>t.Type==(int)TopicType.Topic);
        }

         private void GetRightListActivity(ClubEntities c) { 
            ViewBag.activityRightList = GetRightList<TopicIndex>(c.TopicIndexes, t => t.Id,t=>t.Type==(int)TopicType.Activity);
        }

        private void GetInvolvedList(ClubEntities c){
            ViewBag.involveds = c.ViewTopicInvolveds.Where(t => t.TopicId == tId).ToList<ViewTopicInvolved>();
        }

        private bool IsCanJoin(User user,ClubEntities c,int tId) {
            if (user==null||!User.Identity.IsAuthenticated)
                return false;
            if ( c.TopicInvolveds.Where(t => t.UserId == user.Id && t.TopicId == tId).Count() > 0)
                return true;
            return false;
        }

        private void SendFormatMsg(int fId,int tId,string formatName,string formatParam1,string formatparam2,string ip) {
            string url=Server.MapPath(ClubConst.TextFormatDataUrl);
            string formatStr = App_Start.CommonMethod.GetMsgFormat(formatName, url);
            string t=string.Format(formatStr,formatParam1,formatparam2);
            App_Start.CommonMethod.SendMessge(fId,tId,t,ip);
        }

        private IQueryable<ViewTopicIndex> GetQueryable(ClubEntities club) {
            return club.ViewTopicIndexes.Where(vt => vt.Status == (int)State.Enable);
        }

        private void GetCount(IQueryable<ViewTopicIndex>queryable) {
            DateTime today = DateTime.Now.Date;
            DateTime yesterday = DateTime.Now.AddDays(-1).Date;
            ViewBag.count = queryable.Count();
            ViewBag.todayCount = queryable.Where(t => t.VarDate >= today).Count();
            ViewBag.yesterdayCount = queryable.Where(t => t.VarDate < today && t.VarDate > yesterday).Count();
        }
        
        private void SetViews(ClubEntities club,int id) {
            Views(club,id);
            GetRviews(club,id);
        }

        private void Views(BaWuClub.Web.Dal.ClubEntities club, int id) {
            var ts = club.TopicIndexes.Where(t => t.Id == tId).FirstOrDefault();
            ts.Views += 1;
            club.SaveChanges();
        }

        private void CheckUser(BaWuClub.Web.Dal.User user)
        {
            if (user == null && !User.Identity.IsAuthenticated)
            {
                RedirectToAction("login", "account");
            }
        }

        private void GetRviews(ClubEntities club,int id) {
            ViewBag.reviews=club.ViewTopicReviews.Where(t => t.TopicId == id).ToList<ViewTopicReview>();
        }

        private List<TopicCategory> GetDiscussCategory()
        {
            List<TopicCategory> categories = new List<TopicCategory>();
            using (club = new ClubEntities())
            {
                categories = club.TopicCategories.Where(t => t.Type == 1).ToList<TopicCategory>();
            }
            return categories;
        }
        
        #endregion
    }
}
