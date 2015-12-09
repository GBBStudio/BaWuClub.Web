using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;
using System.Reflection;
using System.Globalization;
using BaWuClub.Web.App_Start;

namespace BaWuClub.Web.Areas.bwum.Controllers
{
    [AdminAuthorize]
    public class SettingController : Controller
    {
        private ClubEntities club;
        private Dictionary<string, string> dic;
        
        public ActionResult Index()
        {
            return View(GetSettingDic());
        }

        public ActionResult Other()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ClearCache()
        {
            HttpRuntime.Cache.Remove("setting");
            return View("~/areas/bwum/views/setting/other.cshtml");
        }

        [HttpPost]
        public ActionResult Save(string title,string keywords,string description,string phone,string fax,string email,string qq,string copyright,string address) {
            Models.SettingModel s = new Models.SettingModel() { 
                Title=title,
                Keywords=keywords,
                Description=description,
                Phone=phone,
                Fax=fax,
                Email=email,
                QQ=qq,
                CopyRight=copyright,
                Address=address
            };
            Status status=Status.error;
            string hitStr=String.Empty;
            Type t=s.GetType();
            using (club = new ClubEntities())
            {
                var Properties = t.GetProperties();
                foreach (var p in Properties) {
                    var settting=club.Settings.Where(st => st.SettingName == p.Name).FirstOrDefault();
                    if (settting != null)
                        settting.SettingValue = p.GetValue(s, null).ToString();
                }
                if (club.SaveChanges() >= 0) {
                    status = Status.success;
                    hitStr = "网站信息保存成功！";
                }
                else{
                    hitStr = "网站信息保存失败，请稍后重试！";
                }
                ViewBag.StatusStr = HtmlCommon.GetHitStr(hitStr, status);
            }
            return View("~/areas/bwum/views/setting/index.cshtml",GetSettingDic());
        }

        private Dictionary<string, string> GetSettingDic() { 
            dic= new Dictionary<string, string>();
            List<Setting> list = new List<Setting>();
            using (club = new ClubEntities()) {
                list = club.Settings.ToList<Setting>();
            }
            if (list != null) {
                foreach (var s in list)
                {
                    var value = s.SettingValue == null ? "" : s.SettingValue;
                    dic.Add(s.SettingName,value);
                }
            }
            return dic;
        }
    }
}
