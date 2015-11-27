using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;

namespace BaWuClub.Web.Controllers
{
    public class ViewsStatisticsController : Controller
    {
        //
        // GET: /ViewsStatistics/

        private ClubEntities club;

        public void Index()
        {
            //Statistics();
            //return View();
        }

        /// <summary>
        /// 统计每日、总浏览的PV
        /// </summary>
        private void Statistics() {
            using (club = new ClubEntities()){
                var _v = (from v in club.Views orderby v.Id descending select v).FirstOrDefault<View>();
                if (IsToday(club))  {
                    _v.Today = _v.Today + 1;
                    _v.Count = _v.Count + 1;
                }
                else{
                    if(_v!=null)
                        club.Views.Add(new View() { Today = 1, Count = _v.Count + 1, Date = DateTime.Now });
                    else
                        club.Views.Add(new View() { Today = 1, Count = 1,Date=DateTime.Now });
                }
                club.SaveChanges();
            }
        }

        /// <summary>
        ///  判断是否是在当前日浏览
        /// </summary>
        /// <param name="club"></param>
        /// <returns>返回bool</returns>
        private bool IsToday(ClubEntities club) {
            string today = DateTime.Now.ToShortDateString();
            View view = new View();
            var _v = (from v in club.Views orderby v.Id descending select v).FirstOrDefault<View>();
            if (_v != null)
                view = _v;
            else
                return false;
            string _today = view.Date.ToShortDateString();
            if (today.Equals(_today))
                return true;
            return false;
        }
    }
}
