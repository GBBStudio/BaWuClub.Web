using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;

namespace BaWuClub.Web.Areas.bwum.Controllers
{
    public class TagController : Controller
    {
        //
        // GET: /bwum/Tag/
        private ClubEntities club;
        private Status state = Status.success;

        #region 标签列表
        [HttpGet]
        public ActionResult Index(int? page)
        {
            List<Tag> tags = new List<Tag>();
            if (string.IsNullOrEmpty(page.ToString()))
                page = 1;
            using (club = new ClubEntities()) { 
                var _tags = from tag in club.Tags.OrderBy(t => t.Id).Skip<Tag>((Convert.ToInt32(page) -1)* ClubConst.AdminPageSize).Take<Tag>(ClubConst.AdminPageSize)
                             select tag;
                tags = _tags.ToList();
                ViewBag.PageHtmlStr = HtmlCommon.GetPageStr(ClubConst.AdminPageSize,Convert.ToInt32(page),club.Tags.Count());
            }
            return View(tags);
        }
        #endregion

        #region 标签编辑
        [HttpGet]
        public ActionResult Edit(int? id) {
            Tag tag = new Tag();
            if (string.IsNullOrEmpty(id.ToString()))
                return View(tag);
            using (club = new ClubEntities()) {
                tag = club.Tags.Single(t => t.Id ==id);   
            }
            return View(tag);
        }
        
        [HttpPost]
        public ActionResult Edit(string tagName) {
            Tag tag = new Tag();
            if (string.IsNullOrEmpty(tagName)){
                ViewBag.statusStr = HtmlCommon.GetHitStr("标签不能空！",Status.error);
                return View(tag);
            }
            if (CheckController.IsRepeate(CheckController.CheckType.TagName,tagName))
            {
                ViewBag.statusStr = HtmlCommon.GetHitStr("该标签已重复！", Status.error);
                return View(tag);
            }
            using (club = new ClubEntities()) {
                tag.TagName= tagName;
                club.Tags.Add(tag);
                if (club.SaveChanges() > 0)
                    ViewBag.statusStr = HtmlCommon.GetHitStr("保存成功！", Status.error);
                else
                    ViewBag.statusStr = HtmlCommon.GetHitStr("系统异常，请稍后重试！", Status.error);
            }
            return View(tag);
        }

        
        [HttpPost]
        public ActionResult Modify(int id, string tagName) {
            Tag tag = new Tag();
            using (club = new ClubEntities()) {
                tag = club.Tags.Single(t => t.Id == id);
                tag.TagName = tagName;
                if (club.SaveChanges() > 0)
                    ViewBag.StatusStr = HtmlCommon.GetHitStr("标签更新成功！",Status.success);
                else
                    ViewBag.StatusStr = HtmlCommon.GetHitStr("标签未修改！", Status.error);
                return View("~/areas/bwum/views/tag/edit.cshtml",tag);
            }
        }
        #endregion

        #region 删除
        [HttpPost]
        public JsonResult Del(int? id){
            return DelTag(Convert.ToInt32(id));
        }
        
        private JsonResult DelTag(int id){
            Tag tag = new Tag();
            object obj;
            using (club = new ClubEntities()) {
                tag = club.Tags.Where(t => t.Id == id).FirstOrDefault();
                if (tag.Id > 0) {
                    club.Tags.Remove(tag);
                    club.SaveChanges();
                    obj = new { status = state.ToString(), content = HtmlCommon.GetHitStr("标签删除成功！", state) };
                }
                else {
                    state = Status.error;
                    obj = new { status = state, content = HtmlCommon.GetHitStr("标签删除失败，请稍后重试！", state) };
                }
            }
            return Json(obj,JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
