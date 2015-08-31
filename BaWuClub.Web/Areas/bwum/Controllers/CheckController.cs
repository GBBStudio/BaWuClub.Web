using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;

namespace BaWuClub.Web.Areas.bwum.Controllers
{
    public class CheckController : Controller
    {
        //
        // GET: /bwum/Check/
        private static ClubEntities club;
        public static bool IsRepeate(CheckType t,string value)
        {
            switch(t){
                case CheckType.TagName:
                    return IsTagNameRepeate(value);
                default:
                    return false;
            }                
        }

        private static bool IsTagNameRepeate(string tagName) {
            using (club = new ClubEntities()) {
                var _tag = from t in club.Tags.Where(c => c.TagName == tagName) select t;
                if (_tag.Count() > 0)
                    return true;
            }
            return false;
        }
                
        public enum CheckType { 
            TagName,
            NickName
        }
    }
}
