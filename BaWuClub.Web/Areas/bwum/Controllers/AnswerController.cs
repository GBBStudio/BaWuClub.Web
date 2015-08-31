﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;

namespace BaWuClub.Web.Areas.bwum.Controllers
{
    public class AnswerController : Controller
    {
        //
        // GET: /bwum/Answer/
        #region 变量
        private ClubEntities club;
        #endregion

        #region 问题回答的列表
        public ActionResult Index(int? page)
        {
            int current = page ?? 1;
            List<ViewAnswer> viewAnswers = new List<ViewAnswer>();
            using (club = new ClubEntities())
            {
                viewAnswers = club.ViewAnswers.OrderBy(v => v.Id).Skip(ClubConst.AdminPageSize * (current - 1)).Take(ClubConst.AdminPageSize).ToList<ViewAnswer>();
                ViewBag.Count = club.ViewAnswers.Count();
            }
            ViewBag.PageHtmlStr = HtmlCommon.GetPageStr(ClubConst.AdminPageSize, current, ViewBag.Count);
            return View(viewAnswers);
        }
        #endregion

        #region 问题回答的展示
        public ActionResult Show() {
            return View();
        }
        #endregion

        #region 回答删除
        public JsonResult Del() {
            return Json(new { });
        }
        #endregion
    }
}
