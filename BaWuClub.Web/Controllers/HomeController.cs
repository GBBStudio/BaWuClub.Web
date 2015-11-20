using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;

namespace BaWuClub.Web.Controllers
{
    public class HomeController : BaseController
    {
        private ClubEntities club;

        public ActionResult Index(){
            using (club = new ClubEntities()) {
                ViewBag.questions = club.ViewQuestions.OrderByDescending(q => q.VarDate).Take(ClubConst.WebPageSize).ToList<ViewQuestion>();
                ViewBag.contributes = club.ViewArticles.OrderByDescending(c => c.VarDate).Take(ClubConst.WebPageSize).Where(c => c.Status > 0).ToList<ViewArticle>();
                ViewBag.topics = club.ViewTopicIndexes.Take(ClubConst.WebPageSize).Where(t => t.Area == 0).ToList<ViewTopicIndex>();
                ViewBag.hotActivity = club.ViewActivities.OrderByDescending(a => a.Id).Take(5).ToList<ViewActivity>();
                ViewBag.hotQuestions = club.ViewQuestions.OrderByDescending(q => q.VarDate).Take(6).ToList<ViewQuestion>();
                ViewBag.banners = club.ViewBanners.Where(b => b.Variables=="sys-bt-home-top" && b.Status == 1).ToList<ViewBanner>();
            }
            return View();
        }
    }
}
