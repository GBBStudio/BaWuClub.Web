using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using System.IO;
using BaWuClub.Web.Common;

namespace BaWuClub.Web.Controllers
{
    public class FileUploadController : Controller
    {
        //
        // GET: /FileUpload/

        [HttpPost]
        public JsonResult Index()
        {
            return Json(new {status="error",content="无法访问",file="" },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult  UploadImage(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode){
            string result = "";
            if (upload != null && upload.ContentLength > 0) {
                string _dir = Server.MapPath(ClubConst.EditorDirectory);
                DirectoryInfo dir = new DirectoryInfo(_dir);
                if (!dir.Exists)
                    dir.Create();
                upload.SaveAs( _dir+DateTime.Now.ToString("yyyyddMMmm") + upload.FileName);
                var imageUrl = Url.Content(_dir+DateTime.Now.ToString("yyyyddMMmm") + upload.FileName); 
                var vMessage = string.Empty;
                result = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + imageUrl + "\", \"" + vMessage + "\");</script></body></html>";
            }
            return Content(result);
        }

        [HttpPost]
        public JsonResult UploadAvatar(HttpPostedFileBase filedata) {
            string dirStr = Server.MapPath(ClubConst.AvatarDir);
            string bDirStr = Server.MapPath(ClubConst.AvatarBigDir);
            string sDirStr = Server.MapPath(ClubConst.AvatarSmallDir);
            string fileName = DateTime.Now.ToString("yyyyddMMmmss") ;
            if (filedata != null && filedata.ContentLength > 0) {
                FileInfo file=new FileInfo(filedata.FileName);
                fileName += file.Extension;
                DirectoryInfo dir = new DirectoryInfo(dirStr);
                if (!dir.Exists)
                    dir.Create();
                filedata.SaveAs(dirStr + fileName);
                ImageTools.CreateThumb(fileName, dirStr, bDirStr, ClubConst.AvatarBigWidth, ClubConst.AvatarBigHeight);
                ImageTools.CreateThumb(fileName, dirStr, sDirStr, ClubConst.AvatarSmallWidth, ClubConst.AvatarSmallHeight);
            }
            return Json(new { name = fileName });
        }

        [HttpPost]
        public JsonResult UploadBanner(HttpPostedFileBase filedata) {
            string dirStr = Server.MapPath(ClubConst.BannerDir);
            string fileName = DateTime.Now.ToString("yyyyddMMmmss");
            if (filedata != null && filedata.ContentLength > 0){
                FileInfo file = new FileInfo(filedata.FileName);
                fileName += file.Extension;
                DirectoryInfo dir = new DirectoryInfo(dirStr);
                if (!dir.Exists)
                    dir.Create();
                filedata.SaveAs(dirStr + fileName);
            }
            return Json(new { name = fileName });
        }

        [HttpPost]
        public JsonResult UploadActivity(HttpPostedFileBase filedata) {
            string dirStr = Server.MapPath(ClubConst.ActivityDir);
            string fileName ="activity_"+ DateTime.Now.ToString("yyyyddMMmmss");
            if (filedata != null && filedata.ContentLength > 0)
            {
                FileInfo file = new FileInfo(filedata.FileName);
                fileName += file.Extension;
                DirectoryInfo dir = new DirectoryInfo(dirStr);
                if (!dir.Exists)
                    dir.Create();
                filedata.SaveAs(dirStr + fileName);
            }
            return Json(new { name = fileName });
        }
    }
}
