using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;
using BaWuClub.Web.App_Start;

namespace BaWuClub.Web.Areas.bwum.Controllers
{
    [AdminAuthorize]
    public class ForumController : Controller
    {
        #region Define variable
        protected int tId = 0;
        protected TopicCategory topicCategory;
        protected ClubEntities club;
        protected Status status = Status.error;
        private string hitStr = string.Empty;
        #endregion

        #region get
        public ActionResult Index(int? id)
        {
            tId = id ?? 1;
            int count = 0;
            List<ViewTopicIndex> list = new List<ViewTopicIndex>();
            using (club = new ClubEntities()) {
                list = club.ViewTopicIndexes.OrderByDescending(a => a.Id).Skip((tId - 1) * ClubConst.AdminPageSize).Take(ClubConst.AdminPageSize).ToList<ViewTopicIndex>();
                count = club.ViewTopicIndexes.Count();
            }
            ViewBag.PageHtmlStr = HtmlCommon.GetPageStr(ClubConst.AdminPageSize, tId, count);
            return View("~/areas/bwum/views/forum/index.cshtml", list);
        }

       #endregion

        #region json
        public JsonResult Del(int id) {
            hitStr="删除失败，请稍后重试!";
            using (club = new ClubEntities()) {
                var topic = club.TopicIndexes.Where(t => t.Id == id).FirstOrDefault();
                if (topic.Type == (int)TopicType.Topic) {
                    var _topic=club.Topics.Where(t => t.Id == id).FirstOrDefault();
                    club.Topics.Remove(_topic);
                }
                else { 
                     if (topic.Type == (int)TopicType.Activity) {
                            var _topic=club.TopicActivities.Where(t => t.Id == id).FirstOrDefault();
                            club.TopicActivities.Remove(_topic);
                         }
                     else {
                             var _topic = club.TopicTasks.Where(t => t.Id == id).FirstOrDefault();
                             club.TopicTasks.Remove(_topic);
                         }
                }
                club.TopicIndexes.Remove(topic);
                if (club.SaveChanges() >= 0) {
                    status = Status.success;hitStr="删除成功！";
                }
            }
            return Json(new { status = status.ToString(), context = HtmlCommon.GetHitStr(status, hitStr) });
        }
        #endregion

        #region 模块编辑、创建
        [HttpGet]
        public ActionResult Create() {
            topicCategory = new TopicCategory();
            return View("~/areas/bwum/views/forum/create.cshtml", topicCategory);
        }

        [HttpPost]
        public ActionResult Create(string name, string variable, string description)
        {
            topicCategory = new TopicCategory() {Name=name,Variable=variable,VarDate=DateTime.Now,Description=description,Type=0 };
            if (string.IsNullOrEmpty(topicCategory.Name) || string.IsNullOrEmpty(topicCategory.Variable))
            {
                hitStr = "论坛模块添加失败，论坛名称或模块变量不能为空！";
            }
            else {
                using (club = new ClubEntities()) {
                    club.TopicCategories.Add(topicCategory);
                    if (club.SaveChanges() > 0)
                    {
                        status = Status.success;
                        hitStr = "论坛模块添加成功！";
                    }
                    else {
                        hitStr = "系统异常，论坛模块添加失败！";
                    }
                }
            }
            ViewBag.StatusStr = HtmlCommon.GetHitStr(hitStr,status);
            return View("~/areas/bwum/views/forum/create.cshtml", topicCategory);
        }

        [HttpGet]
        public ActionResult Edit(int? id) {
            tId = id ?? 0;
            using (club = new ClubEntities()) {
                topicCategory = club.TopicCategories.Where(t => t.Id == tId).FirstOrDefault();
                if (topicCategory == null)
                    return RedirectToAction("notfound", "error");
            }
            return View("~/areas/bwum/views/forum/create.cshtml", topicCategory);
        }

        [HttpPost]
        public ActionResult Edit(int? id, string name, string variable, string description)
        {
            tId=id??0;
            using (club = new ClubEntities()) {
                topicCategory = club.TopicCategories.Where(t => t.Id == tId).FirstOrDefault();
                if (topicCategory == null)
                    return RedirectToAction("notfound", "error");
                topicCategory.Name = name;
                topicCategory.Variable = variable;
                topicCategory.Description = description;
                if (club.SaveChanges() >= 0)
                {
                    status = Status.success;
                    hitStr = "模块更新成功！";
                }
                else {
                    hitStr = "系统异常。论坛模块更新失败！";
                }
            }
            ViewBag.StatusStr = HtmlCommon.GetHitStr(hitStr, status);
            return View("~/areas/bwum/views/forum/create.cshtml",topicCategory);
        }
        #endregion
    }
}
