using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using Newtonsoft.Json;
using System.Text;
using BaWuClub.Web.App_Start;

namespace BaWuClub.Web.Areas.bwum.Controllers
{
    [AdminAuthorize]
    public class UmController : Controller
    {
        //
        // GET: /bwum/Um/
        private  ClubEntities club;
        private delegate void StatisticsEventHandler(ClubEntities c);
        private StatisticsEventHandler Statistics;

        #region get
        public ActionResult Index(){
            return View();
        }

        public ActionResult Loading() {
            return View("~/areas/bwum/views/um/loading.cshtml");
        }

        public ActionResult Main() {
            StringBuilder categories = new StringBuilder();
            StringBuilder series = new StringBuilder();
            using (club = new ClubEntities()) {
                OnStatistics(club);
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

        public ActionResult UserProfile() {
            return View();
        }
        #endregion

        #region post
        public ActionResult SaveUserProfile(string realname,string cover,string phone,string address,string email) {
            return View();
        }
        #endregion

        #region private
        private void OnStatistics(ClubEntities c) {
            Statistics += Accounts;
            Statistics += Articles;
            Statistics += Asks;
            Statistics += Answers;
            Statistics += Topics;
            if (Statistics != null) {
                Statistics(c);
            }
        }

        private void Accounts(ClubEntities c) {
            ViewBag.accounts = c.Users.Count();
            ViewBag.newAccounts = c.Users.Where(u => (DateTime)u.RegDate > DateTime.Today).Count();
        }
        private void Articles(ClubEntities c) {
            ViewBag.articles = c.Articles.Count();
            ViewBag.newArticles = c.Articles.Where(a => (DateTime)a.VarDate > DateTime.Today).Count();
        }
        private void Topics(ClubEntities c){
            ViewBag.topics = c.TopicIndexes.Count();
            ViewBag.newTopics = c.TopicIndexes.Where(v => v.VarDate> DateTime.Today).Count();
        }
        private void Asks(ClubEntities c){
            ViewBag.asks = c.Questions.Count();
            ViewBag.newAsks = c.Questions.Where(a => (DateTime)a.VarDate > DateTime.Today).Count();
        }
        private void Answers(ClubEntities c){
            ViewBag.answers = c.Answers.Count();
            ViewBag.newAnswers = c.Answers.Where(a => a.VarDate > DateTime.Today).Count();
        }
        #endregion
    }
}
