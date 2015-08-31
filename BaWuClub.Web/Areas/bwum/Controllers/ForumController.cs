using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;

namespace BaWuClub.Web.Areas.bwum.Controllers
{
    public class ForumController : Controller
    {
        //
        // GET: /bwum/Forum/
        #region Define variable
        protected int tId = 0;
        protected TopicCategory topicCategory;
        protected ClubEntities club;
        protected Status status = Status.error;
        private string hitStr = string.Empty;
        #endregion

        #region 论坛列表
        public ActionResult Index(int? id)
        {
            tId = id ?? 1;
            int count = 0;
            List<TopicCategory> list = new List<TopicCategory>();
            using (club = new ClubEntities())
            {
                list = club.TopicCategories.Where(t=>t.Type==0).OrderBy(a => a.Id).Skip((tId - 1) * ClubConst.AdminPageSize).Take(ClubConst.AdminPageSize).ToList<TopicCategory>();
                count = club.TopicCategories.Where(t=>t.Type==0).Count();
            }
            ViewBag.PageHtmlStr = HtmlCommon.GetPageStr(ClubConst.AdminPageSize, tId, count);
            return View("~/areas/bwum/views/forum/index.cshtml", list);
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
