using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;


namespace BaWuClub.Web.Areas.bwum.Controllers
{
    public class BannerController : Controller
    {
        //
        // GET: /bwum/Banner/
        #region 初始化定义常用变量
        private ClubEntities club;
        private Banner banner;
        string hitStr = string.Empty;
        Status status = Status.error;
        int tId = 0;
        #endregion
        
        #region 广告列表
        public ActionResult Index(int? id){
            tId = id ?? 1;
            List<ViewBanner> list = new List<ViewBanner>();
            int count = 0;
            using (club = new ClubEntities()) {
                list = club.ViewBanners.OrderBy(b => b.Id).Skip((tId - 1) * ClubConst.AdminPageSize).Take(ClubConst.AdminPageSize).ToList<ViewBanner>();
                count = club.Banners.Count();
            }
            ViewBag.PageHtmlStr = HtmlCommon.GetPageStr(ClubConst.AdminPageSize, tId, count);
            return View("~/areas/bwum/views/banner/index.cshtml", list);
        }
        #endregion

        #region 广告列表的创建操作
        [HttpGet]
        public ActionResult Create() {
            banner = new Banner();
            GetBannerType();
            return View("~/areas/bwum/views/banner/edit.cshtml", banner);
        }

        [HttpPost]
        public ActionResult Create(string title, string url, string pic, int type) {
            string redirectUrl = string.Empty;
            banner = new Banner();
            GetBannerType();
            if (string.IsNullOrEmpty(title)) {
                hitStr="标题不能为空！";
            }else if(string.IsNullOrEmpty(pic)){
                hitStr = "图片为上传,请先上传图片！";
            }
            else {
                using (club = new ClubEntities()) {
                    banner = new Banner { Title = HtmlCommon.ClearHtml(title), Url = HtmlCommon.ClearHtml(url), Pic = pic,Status=1,Type= Byte.Parse(type.ToString()),VarDate=DateTime.Now };
                    club.Banners.Add(banner);
                    if (club.SaveChanges() > 0) {
                        hitStr = "广告图片的添加成功！";
                        status = Status.success;
                    }else{
                        hitStr = "广告图片的添加失败,请稍后重试！";
                    }
                }
            }
            ViewBag.StatusStr = Common.HtmlCommon.GetHitStr(hitStr,status);
            return View("~/areas/bwum/views/banner/edit.cshtml",banner);
        }
        #endregion

        #region 广告编辑
        public ActionResult Show(int? id) {
            GetBannerType();
            tId = id ?? 0;
            using (club = new ClubEntities()) {
                banner = club.Banners.Where(b => b.Id == tId).FirstOrDefault();
            }
            return View("~/areas/bwum/views/banner/edit.cshtml",banner);
        }

        public ActionResult Edit(int? id,string title,string url,string pic,int type) {
            int bId = id ?? 0;
            GetBannerType();
            using (club = new ClubEntities()) {
                banner = club.Banners.Where(b => b.Id == bId).FirstOrDefault();
                if (banner == null)
                    Redirect("/bwum/error/notfound");
                banner.Title = title;
                banner.Url = url;
                banner.Pic = pic;
                banner.Type = Byte.Parse(type.ToString());
                if (club.SaveChanges() >= 0){
                    hitStr = "Banner 信息更新成功!";
                    status = Status.success;
                }
                else {
                    hitStr = "Banner 信息更新失败，请稍后重试！";
                }
            }
            ViewBag.StatusStr = Common.HtmlCommon.GetHitStr(hitStr,status);
            return View("~/areas/bwum/views/banner/edit.cshtml",banner);
        } 
        #endregion

        #region 广告删除
        public JsonResult Del(int? id) {
            int bId = id ?? 0;
            using (club = new ClubEntities()) {
                banner = club.Banners.Where(b => b.Id == bId).FirstOrDefault();
                if (banner == null)
                    Redirect("#/error/notfound");
                club.Banners.Remove(banner);
                if (club.SaveChanges() >= 0) {
                    hitStr = "广告删除成功！";
                    status = Status.success;
                }else{
                    hitStr = "广告删除失败！";
                }
            }
            return Json(new { status = status.ToString(), content = HtmlCommon.GetHitStr("删除成功！", status) });
        }

        public JsonResult MultiDel(string[] chk){
            if (chk.Length == 0) {
                hitStr = "未选中行,请选中行后再进行操作！";
            }else{
                using (club = new ClubEntities()) { 
                     foreach (string ck in chk) {
                         tId = Convert.ToInt32(ck);
                         banner = club.Banners.Where(b => b.Id == tId).FirstOrDefault();
                         club.Banners.Remove(banner);
                    }
                     if (club.SaveChanges() >= 0) {
                         hitStr = "广告删除成功！";
                         status = Status.success;
                     }else{
                         hitStr = "系统异常删除失败,请稍后重试！";
                     }
                }               
            }
            return Json(new { state=status.ToString(),context=hitStr.ToString(),url="/bwum/banner/"});
        }
        #endregion

        #region 设置广告状态
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
        private bool SetStatus(string[] chks,int sId) {
            using (club = new ClubEntities()) {
                banner = new Banner();
                foreach (string chk in chks) {
                   tId = Convert.ToInt32(chk);
                   banner = club.Banners.Where(a => a.Id == tId).FirstOrDefault();
                   banner.Status = (byte)sId;
                   if (club.SaveChanges() < 0)
                       return false;
                }
                return true;
            }
        }
        #endregion

        #region 获取广告分类列表
        public void GetBannerType() {
            using (club = new ClubEntities()){
                ViewBag.BannerTypes = club.BannerTypes.ToList<BannerType>();
            }
        }
        #endregion

        #region 广告图片分类管理
        public ActionResult BannerTypeIndex(int? id) {
            tId = id ?? 1;
            List<BannerType> list = new List<BannerType>();
            int count = 0;
            using (club = new ClubEntities())
            {
                list = club.BannerTypes.OrderBy(b => b.Id).Skip((tId - 1) * ClubConst.AdminPageSize).Take(ClubConst.AdminPageSize).ToList<BannerType>();
                count = club.BannerTypes.Count();
            }
            ViewBag.PageHtmlStr = HtmlCommon.GetPageStr(ClubConst.AdminPageSize, tId, count);
            return View("~/areas/bwum/views/banner/typeindex.cshtml",list);
        }

        [HttpGet]
        public ActionResult BannerTypeCreate()
        {
            BannerType bannertype = new BannerType();
            return View("~/areas/bwum/views/banner/typeedit.cshtml", bannertype);
        }

        [HttpPost]
        public ActionResult BannerTypeCreate(string name, string size, string variables)
        {
            BannerType bannerType = new BannerType() { Name = name, Size = size, Variables = variables, VarDate = DateTime.Now };
            if (string.IsNullOrEmpty(name))
            {
                hitStr = "分类的名称不能为空！";
            }
            else {
                //BannerType bannerType = new BannerType() { Name=name,Size=size,Variables=variables,VarDate=DateTime.Now};
                using (club = new ClubEntities())
                {
                    club.BannerTypes.Add(bannerType);
                    if (club.SaveChanges() >= 0) {
                        status = Status.success;
                        hitStr = "分类名称创建成功！";
                    }
                    else
                    {
                        hitStr = "系统异常，请稍后重试！";
                    }
                }   
            }
            ViewBag.StatusStr = Common.HtmlCommon.GetHitStr(hitStr, status);
            return View("~/areas/bwum/views/banner/typeedit.cshtml", bannerType);
        }

        public ActionResult BannerTypeShow(int? id) {
            tId = id ?? 0;
            BannerType bannerType=new BannerType();
            using (club = new ClubEntities()) {
                bannerType = club.BannerTypes.Where(b => b.Id == tId).FirstOrDefault();
                if (bannerType == null) {
                    return RedirectToAction("notfound","error");
                }
            }
            return View("~/areas/bwum/views/banner/typeedit.cshtml",bannerType);
        }
        
        [HttpPost]
        public ActionResult BannerTypeEdit(int? id,string name,string size,string variables)
        {
            tId = id ?? 0;
            BannerType bannerType = new BannerType();
            using (club = new ClubEntities()) { 
                bannerType = club.BannerTypes.Where(b => b.Id == tId).FirstOrDefault();
                if (bannerType == null)
                {
                    return RedirectToAction("notfound", "error");
                }
                else {
                    bannerType.Name = name;
                    bannerType.Size = size;
                    bannerType.Variables = variables;
                }
                if (club.SaveChanges() >= 0)
                {
                    hitStr = "广告分类更新成功！";
                    status = Status.success;
                }
                else {
                    hitStr = "系统异常，请稍后重试！";
                }
            }
            ViewBag.StatusStr = Common.HtmlCommon.GetHitStr(hitStr, status);
            return View("~/areas/bwum/views/banner/typeedit.cshtml", bannerType);
        }

        [HttpPost]
        public JsonResult BannerTypeDel(int? id)
        {
            BannerType bannertype = new BannerType();
            int bId = id ?? 0;
            using (club = new ClubEntities())
            {
                bannertype = club.BannerTypes.Where(b => b.Id == bId).FirstOrDefault();
                if (bannertype == null)
                    Redirect("#/error/notfound");
                club.BannerTypes.Remove(bannertype);
                if (club.SaveChanges() >= 0)
                {
                    hitStr = "广告分类删除成功！";
                    status = Status.success;
                }
                else
                {
                    hitStr = "广告分类删除失败！";
                }
            }
            return Json(new { status = status.ToString(), content = HtmlCommon.GetHitStr("删除成功！", status) });
        }
        #endregion

    }
}
