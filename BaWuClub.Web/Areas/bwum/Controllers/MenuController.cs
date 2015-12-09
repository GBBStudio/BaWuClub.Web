using BaWuClub.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaWuClub.Web.Areas.bwum.Controllers
{
    [AdminAuthorize]
    public class MenuController : Controller
    {
        //
        // GET: /bwum/Menu/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit() {
            return View();
        }

    }
}
