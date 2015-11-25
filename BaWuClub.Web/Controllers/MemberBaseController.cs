using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaWuClub.Web.Controllers
{
    public class MemberBaseController : BaseController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.userAuthorize = GetUser();
            base.OnActionExecuting(filterContext);
        }
    }
}
