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
    }
}
