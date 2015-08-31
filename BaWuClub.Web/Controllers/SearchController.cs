using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaWuClub.Web.Controllers
{
    public class SearchController : BaseController
    {

        #region 搜索
        public ActionResult Index(string s)
        {
            ViewBag.SearchStr = s;
            return View();
        }
        #endregion
    }
}
