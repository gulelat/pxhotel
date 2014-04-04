using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using PX.Business.Models.Medias;
using PX.Business.Models.Settings.SettingTypes;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.Controllers;
using PX.Business.Mvc.Enums;
using PX.Business.Services.Medias;
using PX.Business.Services.Settings;
using PX.Core.Configurations;
using PX.Core.Configurations.Constants;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class MediaController : PxController
    {
        private readonly IMediaServices _mediaServices;
        private readonly ISettingServices _settingServices;
        public MediaController(IMediaServices mediaServices, ISettingServices settingServices)
        {
            _mediaServices = mediaServices;
            _settingServices = settingServices;
        }

        public ActionResult MediaBrowser()
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
        
        #region Post

        [HttpPost]
        public ActionResult FileUpload(string qqfile, string dir)
        {
            var relativePath = _mediaServices.ToRelativePath(dir);
            var physicalPath = Server.MapPath(relativePath);
            string returnFile;
            try
            {
                var stream = Request.InputStream;
                string file;
                if (String.IsNullOrEmpty(Request["qqfile"]))
                {
                    // IE
                    var postedFile = Request.Files[0];
                    stream = postedFile.InputStream;
                    file = Path.Combine(physicalPath, Path.GetFileName(postedFile.FileName));
                    returnFile = postedFile.FileName;
                }
                else
                {
                    //Webkit, Mozilla
                    file = Path.Combine(physicalPath, qqfile);
                    returnFile = qqfile;
                }

                using (var img = System.Drawing.Image.FromStream(stream))
                {
                    var imageUploadSetting = (ImageUploadSetting)_settingServices.LoadSetting<ImageUploadSettingResolver>();
                    if (imageUploadSetting != null)
                    {
                        if (imageUploadSetting.MinWidth.HasValue && img.Width < imageUploadSetting.MinWidth)
                        {
                            return Json(new { success = false, message = string.Format("Image Width is less than {0}", imageUploadSetting.MinWidth) }, "text/html");
                        }
                        if (imageUploadSetting.MinHeight.HasValue && img.Height < imageUploadSetting.MinHeight)
                        {
                            return Json(new { success = false, message = string.Format("Image Height is less than {0}", imageUploadSetting.MinHeight) }, "text/html");
                        }
                        if (imageUploadSetting.MaxWidth.HasValue && img.Width > imageUploadSetting.MaxWidth)
                        {
                            return Json(new { success = false, message = string.Format("Image Width is greater than {0}", imageUploadSetting.MaxWidth) }, "text/html");
                        }
                        if (imageUploadSetting.MaxHeight.HasValue && img.Height > imageUploadSetting.MaxHeight)
                        {
                            return Json(new { success = false, message = string.Format("Image Height is greater than {0}", imageUploadSetting.MaxHeight) }, "text/html");
                        }
                    }
                    img.Save(file);
                }
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return Json(new { success = false, message = ex.Message }, "text/html");
            }

            var isImage = false;
            var position = returnFile.LastIndexOf(".", StringComparison.Ordinal);
            if (position > 0)
            {
                if (_mediaServices.Images.Contains(returnFile.Substring(position)))
                {
                    isImage = true;
                }
            }

            var location = string.Format("{0}/{1}", _mediaServices.ToMediaPath(relativePath), returnFile);
            return Json(new { success = true, fileLocation = location, isImage }, "text/html");
        }

        [HttpPost]
        public JsonResult GetTreeData(string dir)
        {
            if (string.IsNullOrWhiteSpace(dir))
            {
                var rootNode = new FileTreeModel { attr = new FileTreeAttribute { Id = _mediaServices.MediaDefaultPath, Rel = "home" }, state = "open", data = "Tenant" };
                var physicalPath = _mediaServices.ToRelativePath(_mediaServices.MediaDefaultPath);
                _mediaServices.PopulateTree(physicalPath, rootNode);
                return Json(rootNode);
            }
            return Json(_mediaServices.PopulateChild(_mediaServices.ToRelativePath(dir)));
        }

        [HttpPost]
        public ActionResult MoveData(string path, string destination, bool copy)
        {
            path = _mediaServices.ToRelativePath(path);
            destination = _mediaServices.ToRelativePath(destination);
            return Json(new { status = (int)_mediaServices.MoveData(path, destination, copy) });
        }

        [HttpPost]
        public JsonResult CreateFolder(string path, string folder)
        {
            var relativePath = string.Format("{0}/{1}", _mediaServices.ToRelativePath(path), folder);
            return _mediaServices.CreateFolder(relativePath) ? Json(new { status = true, path = _mediaServices.ToMediaPath(relativePath) }) : Json(new { status = false });
        }

        [HttpPost]
        public JsonResult Delete(string path)
        {
            var relativePath = _mediaServices.ToRelativePath(path);
            return Json(_mediaServices.DeletePath(relativePath) ? new { status = true } : new { status = false });
        }

        [HttpPost]
        public JsonResult Rename(string path, string name)
        {
            var relativePath = _mediaServices.ToRelativePath(path);
            var physicalPath = Server.MapPath(relativePath);
            var attPath = System.IO.File.GetAttributes(physicalPath);
            var isFolder = attPath == FileAttributes.Directory;
            string resultPath;
            var status = _mediaServices.Rename(relativePath, name, out resultPath);
            return Json(new { status = (int)status, path = relativePath, isFolder });
        }

        #endregion
    }
}
