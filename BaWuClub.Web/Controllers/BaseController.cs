using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using System.Web.Security;
using System.Web.Caching;

namespace BaWuClub.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SettingCache();
            base.OnActionExecuting(filterContext);
        }

        private void SettingCache()
        {
            if (HttpRuntime.Cache["setting"] == null)
            {
                List<Setting> list = new List<Setting>();
                using (ClubEntities club = new ClubEntities())
                {
                    list = club.Settings.ToList<Setting>();
                }
                Dictionary<string, object> dic = new Dictionary<string, object>();
                 string value=string.Empty;
                foreach (var s in list)
                {
                    value=s.SettingValue==null?"":s.SettingValue;
                    dic.Add(s.SettingName,value );
                }
                if (dic != null)
                    HttpRuntime.Cache.Insert("setting", dic);
                ViewBag.SettingDictionary = HttpRuntime.Cache["setting"];
            }
            else {
                var dic = HttpRuntime.Cache["setting"] as Dictionary<string, object>;
                ViewBag.SettingDictionary = HttpRuntime.Cache["setting"];
            }
        }

        protected User GetUser() {
            BaWuClub.Web.Dal.User user = null;
            using (ClubEntities c = new ClubEntities()) {
                if (Request.Cookies.AllKeys.Contains("bwusers") && !string.IsNullOrEmpty(Request.Cookies["bwusers"]["id"])&&User.Identity.IsAuthenticated){
                    int userId = Convert.ToInt32(Request.Cookies["bwusers"]["id"]);
                    user = c.Users.Single(u => u.Id == userId);
                }else{
                    FormsAuthentication.SignOut();
                }
            }
            return user;
        }
    }
}
