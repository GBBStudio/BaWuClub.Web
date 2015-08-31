using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;

namespace BaWuClub.Web.Controllers
{
    public class FeedBackController : BaseController
    {
        //
        // GET: /FeedBack/

        public ActionResult Index(){
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sub(string name,string context) {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(context)) {
                ViewBag.HitStr = "<span style=\"color:red\">姓名、内容均不能为空！</span>";
            }else{
                using (ClubEntities club = new ClubEntities()) {
                    Feedback feedback = new Feedback {Name=Common.HtmlCommon.ClearHtml(name),Context=Common.HtmlCommon.ClearHtml(context) };
                    club.Feedbacks.Add(feedback);
                    if (club.SaveChanges() >= 0) {
                        ViewBag.ResponseStr = "<span>你提交的意见反馈，我们已经收到。我们会尽快的处理！感谢您的支持！！</span>";
                    }
                }
            }
                return View("~/views/feedback/index.cshtml");
        }
    }
}
