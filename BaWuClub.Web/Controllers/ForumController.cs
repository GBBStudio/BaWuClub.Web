using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;

namespace BaWuClub.Web.Controllers
{
    public class ForumController : BaseController
    {
        private ClubEntities club;
        private int tId = 0;

        public ActionResult Index()
        {
            List<TopicCategory> categories = GetDiscussCategory();
            ViewBag.Categories = categories;
            return View();
        }
        
        private List<TopicCategory> GetDiscussCategory() {
            List<TopicCategory> categories = new List<TopicCategory>();
            using (club = new ClubEntities()) {
                categories = club.TopicCategories.Where(t=>t.Type==1).ToList<TopicCategory>();
            }
            return categories;
        }

        public ActionResult Cliques() {
            return View("~/views/forum/cliques.cshtml");
        }

        public ActionResult SponsorActivity() {
            return View("~/views/forum/sponsoractivity.cshtml");
        }

        public ActionResult SponsorTask() {
            return View("~/views/forum/sponsortask.cshtml");
        }

        public ActionResult Topic(int? id) {
            tId=id??0;
            BaWuClub.Web.Dal.Topic topic = new BaWuClub.Web.Dal.Topic();
            using (club = new ClubEntities()) {
                topic = club.Topics.Where(t => t.Id == tId).FirstOrDefault();
                if (topic==null) {
                    //return RedirectToAction("notfound","error");
                }
            }

            return View("~/views/forum/topic.cshtml");
        }
        
        [HttpGet]
        public ActionResult PostTopic() {
            return View("~/views/forum/posttopic.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PostTopic(string title, int category, string context) {
            return View("~/views/forum/TopicTransfers.cshtml");
        }

        public ActionResult TaskShow(int? id) {
            return View("~/views/forum/taskshow.cshtml");
        }

        public ActionResult ActivityShow() {
            return View("~/views/forum/activityshow.cshtml");
        }

        public ActionResult List() {
            return View("~/views/forum/list.cshtml");
        }
    }
}
