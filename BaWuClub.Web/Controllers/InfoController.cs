using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;

namespace BaWuClub.Web.Controllers
{
    public class InfoController : BaseController
    {
        //
        // GET: /Info/

        public ActionResult Index(){
            return RedirectToAction("about");
        }

        public ActionResult About() {
            GetSystemArticle("sys-info-about");
            return View("~/views/info/show.cshtml");
        }

        public ActionResult Contact()
        {
            GetSystemArticle("sys-info-contact");
            return View("~/views/info/show.cshtml");
        }

        public ActionResult Help()
        {
            GetSystemArticle("sys-info-help");
            return View("~/views/info/show.cshtml");
        }

        private void GetSystemArticle(string variables)
        {
            using(ClubEntities club=new ClubEntities()){
                var article=club.SystemArticles.Where(s=>s.Variables==variables&&s.Status==1).FirstOrDefault();
                if(article!=null){
                    ViewBag.Title=article.Title;
                    ViewBag.Text=article.Text;
                }
            }
        }
    }
}
