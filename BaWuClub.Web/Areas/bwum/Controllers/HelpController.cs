using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;

namespace BaWuClub.Web.Areas.bwum.Controllers
{
    public class HelpController : Controller
    {
        //
        // GET: /bwum/Help/

        public ActionResult Index()
        {
            SystemArticle article = new SystemArticle();
            using (ClubEntities club = new ClubEntities()) {
                article = club.SystemArticles.Where(a => a.Variables == "sys-info-help-um").FirstOrDefault();
            }
            return View("~/areas/bwum/views/help/index.cshtml", article);
        }

    }
}
