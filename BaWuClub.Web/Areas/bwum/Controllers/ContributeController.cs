using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Common;
using BaWuClub.Web.Dal;
using System.Text;

namespace BaWuClub.Web.Areas.bwum.Controllers
{
    public class ContributeController : Controller
    {
        #region 定义变量
        private ClubEntities club;
        private Status status = Status.error;
        private Article article;
        string hitStr=string.Empty;
        int vId=0;
        #endregion

        #region 投稿列表页面
        public ActionResult Index(int? page)
        {
            int current = page ?? 1;
            List<ViewArticle> viewArticles = new List<ViewArticle>();
            using (club = new ClubEntities()) {
                viewArticles = club.ViewArticles.OrderBy(v => v.Id).Skip(ClubConst.AdminPageSize * (current - 1)).Take(ClubConst.AdminPageSize).ToList<ViewArticle>();
                ViewBag.Count = club.ViewArticles.Count();
            }
            ViewBag.PageHtmlStr = HtmlCommon.GetPageStr(ClubConst.AdminPageSize, current, ViewBag.Count);
            return View(viewArticles);
        }
        #endregion

        #region 投稿列表的删除
        public JsonResult Del(int? id) { 
            vId = id ?? 0;
            hitStr = "系统异常，操作失败！";
            using (club = new ClubEntities()) {
                article = club.Articles.Where(v=> v.Id == vId).FirstOrDefault();
                if (article != null){
                    club.Articles.Remove(article);
                    if (club.SaveChanges() >= 0){
                        hitStr = "删除成功！";
                        status = Status.success;
                    }
                }    
            }
            return Json(new { status = status.ToString(), content = HtmlCommon.GetHitStr(hitStr, status) });
        }

        public JsonResult MultiDel(string[] chk) { 
            if (chk.Length == 0) {
                hitStr = "未选中行,请选中行后再进行操作！";
            }else{
                using (club = new ClubEntities()) { 
                     foreach (string ck in chk) {
                         vId = Convert.ToInt32(ck);
                         article = club.Articles.Where(v => v.Id == vId).FirstOrDefault();
                         if (article != null)
                         {
                             club.Articles.Remove(article);
                         }
                    }
                     if (club.SaveChanges() >= 0) {
                         hitStr = "删除成功！";
                         status = Status.success;
                     }else{
                         hitStr = "系统异常删除失败,请稍后重试！";
                     }
                }               
            }
            return Json(new { state=status.ToString(),context=hitStr.ToString(),url="/bwum/Contribute/"});
        }
        #endregion

        #region 投稿编辑展示
        public ActionResult Show(int? id) {
            vId = id ?? 0;
            using (club = new ClubEntities()) {
                article = club.Articles.Where(v => v.Id == vId).FirstOrDefault();
            }
            return View("~/areas/bwum/views/contribute/show.cshtml", article);
        }

        [HttpPost]
        public ActionResult SetCheck(int? id,string title,string context,string tags) {
            vId=id??0;
            StringBuilder str = new StringBuilder();
            Tag tag = new Tag();
            using (club = new ClubEntities()) {
                article = club.Articles.Where(t => t.Id == vId).FirstOrDefault();
                if (article != null) {
                    article.Tags = tags;
                    article.TagIds =SetTags(club, tags);
                    if (article.Status == 1)
                        article.Status = 0;
                    else
                        article.Status = 1;
                    if (club.SaveChanges() >= 0) {
                        hitStr = "状态修改成功！";
                        status = Status.success;
                    }
                    else {
                        hitStr = "系统异常，请稍后重试！";
                    }
                }
                else
                {
                    return RedirectToAction("notfound","error");
                }
            }
            ViewBag.StatusStr = HtmlCommon.GetHitStr(hitStr, status);
            return View("~/areas/bwum/views/contribute/show.cshtml", article);
        } 
        #endregion

        #region 设置审核的状态
        [HttpPost]
        public JsonResult SetEnable(string[] chk){
            string contextStr = "状态修改成功！";
            if (chk == null) {
                contextStr = "未选中行.请先选中!";
            }
            else {
                if (!SetStatus(chk, 1)){ 
                    contextStr = "系统异常，操作失败！";
                }else{
                    status = Status.success;
                }
            }
            return Json(new { state=status.ToString(),context=contextStr});
        }
        [HttpPost]
        public JsonResult SetDisable(string[] chk) {
            string contextStr = "状态修改成功！";
            if (chk == null){
                contextStr = "未选中行.请先选中!";
            }
            else{
                if (!SetStatus(chk, 0)){
                    contextStr = "系统异常，操作失败！";
                }else{
                    status = Status.success;
                }
            }
            return Json(new { state = status.ToString(), context = contextStr });
        }
        #endregion

        #region 修改状态的公用的私有方法
        private string SetTags(ClubEntities club,string tags){
            StringBuilder str = new StringBuilder();
            Tag tag;
            if (tags != null) {
                string[] tagArray = tags.Split(',');
                foreach (string t in tagArray) {
                    if (!string.IsNullOrEmpty(t)){
                        tag = club.Tags.Where(ta => ta.TagName == t).FirstOrDefault();
                        if (tag != null){
                            str.Append(tag.Id + ",");
                        }
                        else{
                            tag = new Tag() { TagName = t };
                            club.Tags.Add(tag);
                            club.SaveChanges();
                            str.Append(tag.Id + ",");
                        }
                    }
                }
            }
            str = str.Length > 0 ? str.Remove(str.Length - 1, 1) : str;
            return str.ToString();
        }
        private bool SetStatus(string[] chks,int sId) {
            using (club = new ClubEntities()) {
                 Article article= new Article();
                int aId=0;
                foreach (string chk in chks) {
                   aId=Convert.ToInt32(chk);
                   article = club.Articles.Where(a => a.Id == aId).FirstOrDefault();
                   article.Status = (byte)sId;
                   if (club.SaveChanges()< 0)
                       return false;
                }
                return true;
            }
        }
        #endregion
    }
}
