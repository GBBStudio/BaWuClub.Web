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
        //
        // GET: /Home/
        private ClubEntities club;

        public ActionResult Index(){
            using (club = new ClubEntities()) {
                ViewBag.Questions = club.ViewQuestions.OrderByDescending(q => q.VarDate).ToList<ViewQuestion>();
                ViewBag.Contributes = club.ViewArticles.OrderByDescending(c => c.VarDate).Where(c => c.Status > 0).ToList<ViewArticle>();
                ViewBag.HotActivity = club.ViewActivities.OrderByDescending(a => a.Id).Take(5).ToList<ViewActivity>();
                ViewBag.HotQuestions = club.ViewQuestions.OrderByDescending(q => q.VarDate).Take(6).ToList<ViewQuestion>();
                ViewBag.Banners = club.ViewBanners.Where(b => b.Variables=="sys-bt-home-top" && b.Status == 1).ToList<ViewBanner>();
            }
            return View();
        }
    }
}
