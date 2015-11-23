using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaWuClub.Web.Controllers
{
    public class GroupController : BaseController
    {
        //
        // GET: /Group/

        public ActionResult City(string city){
            return View("~/views/group/city.cshtml");
        }

        public ActionResult Apply() { 
            return View("~/views/group/city.cshtml");
        }
    }
}
