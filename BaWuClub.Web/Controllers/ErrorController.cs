using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaWuClub.Web.Controllers
{
    public class ErrorController : BaseController
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound(){
            return View("~/views/error/404.cshtml");
        }

        public ActionResult Unaudited() {
            return View();
        }

        public ActionResult Exception(string url) {
            ViewBag.url = url;
            return View();
        }

        public ActionResult Underdevelopment() {
            return View();
        }
    }
}
