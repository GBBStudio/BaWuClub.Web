using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaWuClub.Web.App_Start
{
    public class AdminAuthorize:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return IsLogin(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectResult redirect = new RedirectResult("/bwum/account/login");
            filterContext.Result = redirect;
        }

        private bool IsLogin(HttpContextBase httpContext) {
            if (httpContext.Session["login"] == null || httpContext.Session["login"].ToString() != "login")
                return false;
            return true;
        }
    }
}