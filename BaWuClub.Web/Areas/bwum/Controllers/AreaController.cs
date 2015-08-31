using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using BaWuClub.Web.Common;

namespace BaWuClub.Web.Areas.bwum.Controllers 
{
    public class AreaController : Controller
    {
        //
        // GET: /bwum/Area/
        #region 定义变量
        private ClubEntities club;
        private Area area;
        private Status status=Status.error;
        private int aId = 0;
        private string hitStr = string.Empty;
        #endregion

        #region 区域列表
        public ActionResult Index(int? id){
            aId=id??1;
            List<Area> list = new List<Area>();
            using (club = new ClubEntities()) {
                list = club.Areas.OrderBy(a => a.Id).Skip((aId - 1) * ClubConst.AdminPageSize).Take(ClubConst.AdminPageSize).ToList<Area>();
            }
            ViewBag.PageHtmlStr = HtmlCommon.GetPageStr(ClubConst.AdminPageSize,aId,list.Count());
            return View("~/areas/bwum/views/area/index.cshtml",list);
        }
        #endregion

        #region 创建区域操作
        [HttpGet]
        public ActionResult Create() {
            area = new Area();
            using (club = new ClubEntities()) { 
                GetProvince(club);
            }
            return View("~/areas/bwum/views/area/edit.cshtml",area);
        }

        [HttpPost]
        public ActionResult Create(string name,int node) {
            area = new Area() { Name=name,Node=(Byte)node};
            if (!string.IsNullOrEmpty(name)) { 
                using (club = new ClubEntities()) {
                    GetProvince(club);
                    club.Areas.Add(area);
                    if (club.SaveChanges() >= 0) {
                        hitStr = "区域创建成功！";
                        status = Status.success;
                    }
                }
            }else{
                hitStr= "区域名称不能空！";
            }
            ViewBag.StatusStr = Common.HtmlCommon.GetHitStr(hitStr, status);
            return View("~/areas/bwum/Views/area/edit.cshtml",area);
        }
        #endregion

        #region 区域删除
        public JsonResult Del(int? id) {
            aId=id??0;
            using (club = new ClubEntities()) {
                area = club.Areas.Where(a => a.Id == aId).FirstOrDefault();
                if (area != null) {
                        club.Areas.Remove(area);
                        if (club.SaveChanges() >= 0) {
                            hitStr = "删除成功！";
                            status = Status.success;
                        }
                    }else{
                        hitStr="要删除的数据不存在！";
                    }
                }
            return Json(new { status=status.ToString(),context=HtmlCommon.GetHitStr(status,hitStr)});
        }

        public JsonResult MultiDel(string[] chk){
            if (chk.Length == 0) {
                hitStr = "未选中行,请选中行后再进行操作！";
            }else{
                using (club = new ClubEntities()) { 
                     foreach (string ck in chk) {
                         aId = Convert.ToInt32(ck);
                         var area = club.Areas.Where(b => b.Id == aId).FirstOrDefault();
                         club.Areas.Remove(area);
                    }
                     if (club.SaveChanges() >= 0) {
                         hitStr = "信息删除成功！";
                         status = Status.success;
                     }else{
                         hitStr = "系统异常删除失败,请稍后重试！";
                     }
                }               
            }
            return Json(new { state=status.ToString(),context=hitStr.ToString(),url="/bwum/area/"});
        }
        #endregion

        #region 区域的编辑
        [HttpPost]
        public ActionResult Edit(int? id,string name ,int node){
            aId = id ?? 0;
            using (club = new ClubEntities()) {
                GetProvince(club);
                area = club.Areas.Where(a => a.Id == aId).FirstOrDefault();
                area.Name = name;
                area.Node = (Byte)node;
                if (club.SaveChanges() >= 0) {
                    hitStr = "区域更新成功！";
                    status = Status.success;
                }
            }
            ViewBag.StatusStr = Common.HtmlCommon.GetHitStr(hitStr,status);
            return View("~/areas/bwum/Views/Area/edit.cshtml",area);
        }

        [HttpGet]
        public ActionResult Show(int? id) { 
            aId=id??0;
            using (club = new ClubEntities()) {
                GetProvince(club);
                area = club.Areas.Where(a=>a.Id==aId).FirstOrDefault();
            }
            if (area == null)
                Redirect("/error/notfound");
            return View("~/areas/bwum/Views/Area/edit.cshtml",area);            
        }
        #endregion

        #region 私有的方法(获取省份、直辖市)
        private void GetProvince(ClubEntities c) {
            ViewBag.Provinces = c.Areas.Where(a=>a.Node==0).ToList<Area>();
        }
        #endregion
    }
}
