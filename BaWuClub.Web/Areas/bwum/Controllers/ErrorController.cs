using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaWuClub.Web.Areas.bwum.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /bwum/Error/

        public ActionResult Index(){
            return View();
        }
        public ActionResult NotFound() {
            return View();
        }

    }
}
