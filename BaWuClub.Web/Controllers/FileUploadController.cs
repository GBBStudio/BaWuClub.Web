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
        private Status status = Status.error;

        [HttpPost]
        public JsonResult Index()
        {
            return Json(new { status = status.ToString(), content = "无法访问", file = "" }, JsonRequestBehavior.AllowGet);
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
     //   [Authorize]
        public JsonResult UploadFiles(HttpPostedFileBase filedata, string type)
        {
            string content = "未指定文件分类类型！";
            string fileName = string.Empty;
            string url=string.Empty;
            if (!string.IsNullOrEmpty(type)) {
                string dirStr= GetDir(type);
                string path = Server.MapPath(dirStr);
                fileName = type + "_" + DateTime.Now.ToString("yyyyddMMmmss");
                content = "未上传文件！";
                if (filedata != null && filedata.ContentLength > 0)
                {
                    FileInfo file = new FileInfo(filedata.FileName);
                    fileName += file.Extension;
                    DirectoryInfo dir = new DirectoryInfo(path);
                    if (!dir.Exists) {
                        dir.Create();
                    }
                    filedata.SaveAs(path + fileName);
                    content = "文件上传成功！";
                    status = Status.success;
                    url=dirStr+fileName;
                }
            }
            return Json(new { status = status.ToString(), content = content, name = fileName,url=url });
        }

        private string GetDir(string type) {
            string dir=ClubConst.UploadTmp;
            switch (type) { 
                case "banner":
                    dir=ClubConst.BannerDir;
                    break;
                case "activity":
                    dir = ClubConst.ActivityDir;
                    break;
                case "avatar":
                    dir=ClubConst.AvatarDir;
                    break;
                case "topicactivity":
                    dir = ClubConst.TopicAactivity;
                        break;
                case "files":
                    dir = ClubConst.FilesDir;
                        break;
                case "video":
                        dir = ClubConst.VideoDir;
                        break;
                case "videocover":
                        dir = ClubConst.VideoCoverDir;
                        break;
                default:
                    break;
            }
            return dir;
        }
    }
}
