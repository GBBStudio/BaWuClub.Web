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
    public class DiscussController : Controller
    {
        #region 定义变量
        private int tId = 0;
        private ClubEntities club;
        private Status status = Status.error;
        private string hitStr = string.Empty;
        private TopicCategory topicCategory;
        #endregion

        #region 讨论区列表
        public ActionResult Index(int? id)
        {
            tId = id ?? 1;
            int count = 0;
            List<ViewTopicCategory> list = new List<ViewTopicCategory>();
            using (club = new ClubEntities())            {
                list = club.ViewTopicCategories.Where(v => v.FatherVariable == "sys-topic-discuss").OrderByDescending(a => a.Id).Skip((tId - 1) * ClubConst.AdminPageSize).Take(ClubConst.AdminPageSize).ToList<ViewTopicCategory>();
                count = club.ViewTopicCategories.Where(v=>v.FatherVariable=="sys-topic-discuss").Count();
            }
            ViewBag.PageHtmlStr = HtmlCommon.GetPageStr(ClubConst.AdminPageSize, tId, count);
            return View("~/areas/bwum/views/discuss/index.cshtml",list);
        }
        #endregion

        #region 讨论分类的创建
        [HttpGet]
        public ActionResult Create() {
            topicCategory = new TopicCategory();
            return View("~/areas/bwum/views/discuss/create.cshtml", topicCategory);
        }

        [HttpPost]
        public ActionResult Create(string name, string description, string pic,string icon, string variable)
        {
            topicCategory = new TopicCategory() { Name = name, Description = description, Cover = pic, Icon=icon,Variable = variable, VarDate = DateTime.Now,Type=1 };
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(variable))
            {
                hitStr = "分类的名称、变量缺一不可";
            }
            else {
                using (club = new ClubEntities()) {
                    club.TopicCategories.Add(topicCategory);
                    if (club.SaveChanges() >= 0)
                    {
                        status = Status.success;
                        hitStr = "分类名称创建成功！";
                    }
                    else {
                        hitStr = "系统异常，请稍后重试！";
                    }
                }
            }
            ViewBag.StatusStr = Common.HtmlCommon.GetHitStr(hitStr, status);
            return View("~/areas/bwum/views/discuss/create.cshtml",topicCategory);
        }
        #endregion

        #region 讨论分类的编辑
        [HttpGet]
        public ActionResult Edit(int? id) {
            tId = id ?? 1;
            using (club = new ClubEntities())
            {
                topicCategory = club.TopicCategories.Where(t => t.Id == tId).FirstOrDefault();
            }
            if (topicCategory == null)
                return RedirectToAction("notfound","error");
            return View("~/areas/bwum/views/discuss/create.cshtml", topicCategory);
        }

        [HttpPost]
        public ActionResult Edit(int id,string name,string description,string pic,string icon,string variable)
        {
            using(club=new ClubEntities()){
                topicCategory = club.TopicCategories.Where(t => t.Id == id).FirstOrDefault();
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(variable))
                {
                    hitStr = "分类的名称、变量缺一不可";
                }
                else {
                    if (topicCategory == null)
                    {
                        return RedirectToAction("notfound", "error");
                    }
                    else
                    {
                        topicCategory.Name = name;
                        topicCategory.Description = description;
                        topicCategory.Cover = pic;
                        topicCategory.Variable = variable;
                        topicCategory.Icon = icon;
                        if (club.SaveChanges() >= 0)
                        {
                            status = Status.success;
                            hitStr = "分类更新成功！";
                        }
                        else
                        {
                            hitStr = "系统异常，请稍后重试！";
                        }
                    }
                }                
            }
            ViewBag.StatusStr = Common.HtmlCommon.GetHitStr(hitStr, status);
            return View("~/areas/bwum/views/discuss/create.cshtml", topicCategory);
        }
        #endregion
    }
}
