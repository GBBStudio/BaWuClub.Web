using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;
using System.Transactions;
using System.Text;

namespace BaWuClub.Web.Controllers
{
    public class ForumController : BaseController
    {
        #region define
        private ClubEntities club;
        private int tId = 0;
        private string postTransfersUrl = "~/views/direct/transfers.cshtml";
        #endregion

        #region Get
        public ActionResult Index()
        {
            ViewBag.Categories=GetDiscussCategory();
            List<ViewTopicIndex> viewTopicsIndex = new List<ViewTopicIndex>();
            using (club = new ClubEntities()) {
                viewTopicsIndex = club.ViewTopicIndexes.Take(ClubConst.TopicPageSize).Where(t => t.Area == 0).ToList<ViewTopicIndex>();
                int count = club.ViewTopicIndexes.Where(t => t.Area == 0).Count();
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
                viewTopicsIndex = club.ViewTopicIndexes.OrderByDescending(t=>t.VarDate).Skip(ClubConst.TopicPageSize*(tId-1)).Take(ClubConst.TopicPageSize).Where(t => t.Area == 0).ToList<ViewTopicIndex>();
                int count = club.ViewTopicIndexes.Where(t => t.Area == 0).Count();
                ViewBag.pageStr = HtmlCommon.GetPageStrPro("/forum/p/", 13, tId, count, ClubConst.TopicPageShow);
            }
            return View("~/views/forum/index.cshtml", viewTopicsIndex);
        }

        public ActionResult Cliques() {
            List<TopicIndex> topics = new List<TopicIndex>();
            using (club = new ClubEntities()) {
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
                if(!string.IsNullOrEmpty(sort)&&sort=="time")
                    topics = club.ViewTopicIndexes.OrderByDescending(t=>t.VarDate).Skip(ClubConst.TopicPageSize *( tId-1)).Take(ClubConst.TopicPageSize).Where(t => t.Category == category.Id).ToList<ViewTopicIndex>();
                else
                    topics = club.ViewTopicIndexes.OrderByDescending(t => t.Id).Skip(ClubConst.TopicPageSize * (tId - 1)).Take(ClubConst.TopicPageSize).Where(t => t.Category == category.Id).ToList<ViewTopicIndex>();
                int count=club.ViewTopicIndexes.Where(t=>t.Category==category.Id).Count();
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
                topics = club.ViewTopicIndexes.OrderBy(t => t.VarDate).Skip(ClubConst.TopicPageSize * (tId - 1)).Take(ClubConst.TopicPageSize).Where(t => t.Type == ((int)tt)).ToList<ViewTopicIndex>();
                int count = club.ViewTopicIndexes.Where(t => t.Type == ((int)tt)).Count();
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
            using (club = new ClubEntities())
            {
                topic = club.ViewTopicTasks.Where(t => t.Id == tId).FirstOrDefault();
                if (topic == null)
                {
                    return RedirectToAction("notfound", "error");
                }
                SetViews(club, tId);
            }
            return View("~/views/forum/task.cshtml", topic);
        }

        public ActionResult Activity(int? id)
        {
            tId = id ?? 0;
            ViewTopicActivity topic = new ViewTopicActivity();
            using (club = new ClubEntities())
            {
                topic = club.ViewTopicActivities.Where(t => t.Id == tId).FirstOrDefault();
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

        [Authorize]
        [HttpGet]
        public ActionResult SponsorActivity()
        {
            return View("~/views/forum/sponsoractivity.cshtml");
        }

        [Authorize]
        public ActionResult SponsorTask()
        {
            return View("~/views/forum/sponsortask.cshtml");
        }

        [HttpGet]
        [Authorize]
        public ActionResult PostTopic()
        {
            ViewBag.categoryList = GetDiscussCategory();
            return View("~/views/forum/posttopic.cshtml");
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PostTask(string title, string context)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(context))
            {
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
                TopicIndex topicIndex = new TopicIndex() { Title = HtmlCommon.ClearHtml(title), Category = category, UserId = user.Id, VarDate = DateTime.Now };
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
                    str.Append("<div class=\"reviews-cover fleft\"><a href=\"/member/u-" + user.Id + "/show\"><img src=\"" + (user.Cover.Length > 0 ? "/uploads/avatar/small/" + user.Cover : "~/Content/Images/no-img.jpg") + "\" /></a> </div>");
                    str.Append("<div class=\"reviews-content\"><div class=\"reviews-info\"><a href=\"/member/u-" + user.Id + "/show\">" + user.NickName + "</a><span>" + ((DateTime)review.VarDate).ToString("yyyy年mm月dd日") + "</span></div>");
                    str.Append("<div class=\"reviews-text\">" + review.Reviews + "</div>");
                    str.Append("<div class=\"reviews-btns\">");
                    str.Append("<a href=\"#\">回复</a>");
                    str.Append("<a href=\"#\">赞</a></div></div>");
                    str.Append("</li>");
                    status = Status.success;               
                }
            }
            return Json(new { status = status.ToString(),context=str.ToString()});
        }
        #endregion

        #region private
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
