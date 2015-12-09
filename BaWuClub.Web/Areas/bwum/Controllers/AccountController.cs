using BaWuClub.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaWuClub.Web.Areas.bwum.Controllers
{
    public class AccountController : Controller
    {

        #region get
        [HttpGet]
        public ActionResult Login(){
            return View("~/areas/bwum/views/um/login.cshtml");
        }

        [HttpGet]
        public ActionResult SignOut() {
            Session.Clear();
            return View("~/areas/bwum/views/um/signout.cshtml");
        }
        #endregion

        #region post
        public ActionResult Login(string name, string password) {
            Session["login"] = "login";
            return Redirect("/bwum#/um/main");
        }
        #endregion
    }
}
