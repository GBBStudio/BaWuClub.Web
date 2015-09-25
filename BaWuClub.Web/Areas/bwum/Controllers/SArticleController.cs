using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;

namespace BaWuClub.Web.Areas.bwum.Controllers
{
    public class SArticleController : Controller
    {
        #region 常用变量的定义
        private ClubEntities club;
        private int tId = 0;
        private Status status = Status.error;
        private SystemArticle sysArticle;
        private string hitStr = string.Empty;
        #endregion

        #region 系统文章列表
        public ActionResult Index(int? page)
        {
            tId = page ?? 1;
            List<SystemArticle> systemArticles = new List<SystemArticle>();
            using (club = new ClubEntities())
            {
                systemArticles = club.SystemArticles.OrderBy(s => s.Id).Skip(ClubConst.AdminPageSize * (tId - 1)).Take(ClubConst.AdminPageSize).ToList<SystemArticle>();
                ViewBag.Count = club.SystemArticles.Count();
            }
            ViewBag.PageHtmlStr = HtmlCommon.GetPageStr(ClubConst.AdminPageSize, tId, ViewBag.Count);
            return View(systemArticles);
        }
        #endregion

        #region 系统文章的编辑
        public ActionResult Edit(int? id) {
            tId = id ?? 1;
            sysArticle = new SystemArticle();
            using (club = new ClubEntities()) {
                sysArticle = club.SystemArticles.Where(s => s.Id == tId).FirstOrDefault();
            }
            if (sysArticle == null)
                return RedirectToAction("notfound","error");
            return View(sysArticle);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, string title, string variables, string context)
        {
            sysArticle = new SystemArticle();
            using (club = new ClubEntities()) {
                sysArticle = club.SystemArticles.Where(s => s.Id == id).FirstOrDefault();
                if (sysArticle != null) {
                    sysArticle.Title = title;
                    sysArticle.Text = context;
                    sysArticle.Variables = variables;
                }
                else
                {
                    return RedirectToAction("notfound","error");
                }
                if (club.SaveChanges() >= 0) {
                    status = Status.success;
                    hitStr = "文章更新成功！";
                }
                else
                {
                    hitStr = "文章更新失败，请稍后重试！";
                }
            }
            return View("~/areas/bwum/views/sarticle/edit.cshtml",sysArticle);
        }
        #endregion

        #region 设置审核的状态
        [HttpPost]
        public JsonResult SetEnable(string[] chk)
        {
            string contextStr = "状态修改成功！";
            if (chk == null)
            {
                contextStr = "未选中行.请先选中!";
            }
            else
            {
                if (!SetStatus(chk, 1))
                {
                    contextStr = "系统异常，操作失败！";
                }
                else
                {
                    status = Status.success;
                }
            }
            return Json(new { state = status.ToString(), context = contextStr });
        }

        [HttpPost]
        public JsonResult SetDisable(string[] chk)
        {
            string contextStr = "状态修改成功！";
            if (chk == null)
            {
                contextStr = "未选中行.请先选中!";
            }
            else
            {
                if (!SetStatus(chk, 0))
                {
                    contextStr = "系统异常，操作失败！";
                }
                else
                {
                    status = Status.success;
                }
            }
            return Json(new { state = status.ToString(), context = contextStr });
        }
        #endregion

        #region 修改状态的公用的私有方法
        private bool SetStatus(string[] chks, int sId)
        {
            using (club = new ClubEntities())
            {
                SystemArticle sysArticle = new SystemArticle();
                int aId = 0;
                foreach (string chk in chks)
                {
                    aId = Convert.ToInt32(chk);
                    sysArticle = club.SystemArticles.Where(a => a.Id == aId).FirstOrDefault();
                    sysArticle.Status = (byte)sId;
                    if (club.SaveChanges() < 0)
                        return false;
                }
                return true;
            }
        }
        #endregion

        #region 创建文章
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(string title, string context,string variables)
        {
            sysArticle = new SystemArticle() { Title = title, Text = context,PutDate=DateTime.Now,Variables=variables};
            if (!string.IsNullOrEmpty(title))
            {
                using (club = new ClubEntities())
                {
                    club.SystemArticles.Add(sysArticle);
                    if (club.SaveChanges() >= 0)
                    {
                        hitStr = "系统文章创建成功！";
                        status = Status.success;
                    }
                    else
                    {
                        hitStr = "系统文章创建失败，请稍后重试！";
                    }
                }
            }
            else {
                hitStr = "文章的标题不能为空！";
            }
            ViewBag.statusStr = HtmlCommon.GetHitStr(hitStr,status);
            return View("~/areas/bwum/views/sarticle/edit.cshtml",sysArticle);
        }

        [HttpGet]
        public ActionResult Create() {
            sysArticle = new SystemArticle();
            return View("~/areas/bwum/views/sarticle/edit.cshtml", sysArticle);
        }
        #endregion
    }
}
