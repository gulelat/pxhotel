using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using PX.Core.Configurations;
using PX.Core.Configurations.Constants;

namespace PX.Web.Areas.Admin.Controllers
{
    public class FileManagerController : Controller
    {
        public ActionResult Browser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            var vImagePath = String.Empty;
            var vMessage = String.Empty;

            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var extension = Path.GetExtension(upload.FileName);
                    if (extension != null && DefaultConstants.ImageExtensions.Contains(extension.ToLower()))
                    {
                        var vFileName = DateTime.Now.ToString("yyyyMMdd-HHMMssff") +
                                        extension.ToLower();
                        var vFolderPath = Server.MapPath(Configurations.UploadFolder);

                        if (!Directory.Exists(vFolderPath))
                        {
                            Directory.CreateDirectory(vFolderPath);
                        }

                        string vFilePath = Path.Combine(vFolderPath, vFileName);
                        upload.SaveAs(vFilePath);

                        vImagePath = Url.Content(Configurations.UploadFolder + vFileName);
                        vMessage = "Image was saved correctly";
                    }
                    else
                    {
                        vMessage = "Wrong input file type";
                    }
                }
            }
            catch
            {
                vMessage = "There was an issue uploading";
            }
            var vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + vImagePath + "\", \"" + vMessage + "\");</script></body></html>";

            return Content(vOutput);
        }

    }
}
