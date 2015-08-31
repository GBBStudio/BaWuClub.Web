using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using Newtonsoft.Json;
using System.Text;

namespace BaWuClub.Web.Areas.bwum.Controllers
{
    public class UmController : Controller
    {
        //
        // GET: /bwum/Um/
        private  ClubEntities club;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Loading() {
            return View("~/areas/bwum/views/um/loading.cshtml");
        }

        public ActionResult Main() {
            StringBuilder categories = new StringBuilder();
            StringBuilder series = new StringBuilder();
            using (club = new ClubEntities()) {
                var _views = (from v in club.Views orderby v.Id descending select v).Take<View>(7);
                var _v = _views.FirstOrDefault<View>();
                if (_v != null){
                    ViewBag.TodayView = _v.Today;
                    ViewBag.CountView = _v.Count;                    
                }
                else {
                    ViewBag.TodayView =0;
                    ViewBag.CountView = 0;
                }
                foreach (View view in _views.OrderBy(v=>v.Id)){
                    categories.Append(String.Format(@"{0}", view.Date.ToString("dd")) + ",");
                    series.Append(view.Today + ",");
                }
                if (categories.Length > 0)
                    categories.Remove(categories.Length-1,1);
                if (series.Length > 0)
                    series.Remove(series.Length - 1, 1);
            }
            ViewBag.Categories = categories.ToString();
            ViewBag.Series = series;
            return View("~/Areas/bwum/Views/Um/Main.cshtml");
        }
    }
}
