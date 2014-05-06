using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using PX.Business.Models.Media;
using PX.Business.Models.Settings.SettingTypes;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.Localizes;
using PX.Business.Services.Media;
using PX.Business.Services.Settings;
using PX.Core.Configurations;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.MvcResults;
using PX.Core.Framework.Mvc.MvcResults.Image;
using PX.Core.Ultilities.Files;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class MediaController : AdminController
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly IMediaFileManager _mediaFileManager;
        private readonly IMediaServices _mediaServices;
        private readonly ISettingServices _settingServices;
        public MediaController(IMediaServices mediaServices, ISettingServices settingServices
            , IMediaFileManager mediaFileManager, ILocalizedResourceServices localizedResourceServices)
        {
            _mediaFileManager = mediaFileManager;
            _mediaServices = mediaServices;
            _settingServices = settingServices;
            _localizedResourceServices = localizedResourceServices;
        }

        public ActionResult MediaBrowser(string imageUrl)
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
                    if (extension != null && _mediaServices.IsImage(upload.FileName))
                    {
                        var vFileName = DateTime.Now.ToString(Configurations.DateTimeFormat) +
                                        extension.ToLower();
                        var vFolderPath = Server.MapPath(Configurations.UploadFolder);

                        if (!Directory.Exists(vFolderPath))
                        {
                            Directory.CreateDirectory(vFolderPath);
                        }

                        string vFilePath = Path.Combine(vFolderPath, vFileName);
                        upload.SaveAs(vFilePath);

                        vImagePath = Url.Content(Configurations.UploadFolder + vFileName);
                        vMessage = _localizedResourceServices.T("AdminModule:::Media:::Messages:::SaveImageSuccessfully:::Image was saved successfully.");
                    }
                    else
                    {
                        vMessage = _localizedResourceServices.T("AdminModule:::Media:::Messages:::WrongFileType:::Wrong input file type.");
                    }
                }
            }
            catch
            {
                vMessage = _localizedResourceServices.T("AdminModule:::Media:::Messages:::UploadFailure:::There was an issue while uploading. Please try again.");
            }
            var vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + vImagePath + "\", \"" + vMessage + "\");</script></body></html>";

            return Content(vOutput);
        }

        #region Post

        [HttpPost]
        public ActionResult FileUpload(string qqfile, string dir)
        {
            var physicalPath = Server.MapPath(dir);
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
                            return Json(new { Success = false, Message = string.Format(_localizedResourceServices.T("AdminModule:::Media:::Upload:::ValidationMessages:::InvalidMinWidth:::Image Width is less than {0}"), imageUploadSetting.MinWidth) }, "text/html");
                        }
                        if (imageUploadSetting.MinHeight.HasValue && img.Height < imageUploadSetting.MinHeight)
                        {
                            return Json(new { Success = false, Message = string.Format(_localizedResourceServices.T("AdminModule:::Media:::Upload:::ValidationMessages:::InvalidMinHeight:::Image Height is less than {0}"), imageUploadSetting.MinHeight) }, "text/html");
                        }
                        if (imageUploadSetting.MaxWidth.HasValue && img.Width > imageUploadSetting.MaxWidth)
                        {
                            return Json(new { Success = false, Message = string.Format(_localizedResourceServices.T("AdminModule:::Media:::Upload:::ValidationMessages:::InvalidMaxWidth:::Image Width is greater than {0}"), imageUploadSetting.MaxWidth) }, "text/html");
                        }
                        if (imageUploadSetting.MaxHeight.HasValue && img.Height > imageUploadSetting.MaxHeight)
                        {
                            return Json(new { Success = false, Message = string.Format(_localizedResourceServices.T("AdminModule:::Media:::Upload:::ValidationMessages:::InvalidMaxHeight:::Image Height is greater than {0}"), imageUploadSetting.MaxHeight) }, "text/html");
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
                return Json(new { Success = false, Message = ex.Message }, "text/html");
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

            var location = string.Format("{0}/{1}", dir, returnFile);
            return Json(new { Success = true, Message = _localizedResourceServices.T("AdminModule:::Media:::Upload:::Messages:::Upload successfully."), fileLocation = location, isImage }, "text/html");
        }

        [HttpPost]
        public JsonResult GetTreeData(string dir)
        {
            if (string.IsNullOrWhiteSpace(dir))
            {
                var rootNode = new FileTreeModel { attr = new FileTreeAttribute { Id = _mediaServices.MediaDefaultPath, Rel = "home" }, state = "open", data = "MEDIA" };
                var physicalPath = _mediaServices.MediaDefaultPath;
                _mediaServices.PopulateTree(physicalPath, rootNode);
                return Json(rootNode);
            }
            return Json(_mediaServices.PopulateChild(dir));
        }

        [HttpPost]
        public ActionResult MoveData(string path, string destination, bool copy)
        {
            return Json(_mediaServices.MoveData(path, destination, copy));
        }

        [HttpPost]
        public JsonResult CreateFolder(string path, string folder)
        {
            var relativePath = Path.Combine(path, folder);
            return Json(_mediaServices.CreateFolder(relativePath));
        }

        [HttpPost]
        public JsonResult Delete(string path)
        {
            return Json(_mediaServices.DeletePath(path));
        }

        [HttpPost]
        public JsonResult Rename(string path, string name)
        {
            var physicalPath = Server.MapPath(path);
            var attPath = System.IO.File.GetAttributes(physicalPath);
            var isFolder = attPath == FileAttributes.Directory;
            string resultPath;
            var response = _mediaServices.Rename(path, name, out resultPath);
            return Json(new { Success = response.Success, path = resultPath, isFolder, message = response.Message });
        }

        [HttpPost]
        public JsonResult GetFileInfo(string path)
        {
            try
            {
                path = _mediaServices.MapPath(path);
                // get the file attributes for file or directory
                var attr = System.IO.File.GetAttributes(path);
                FileInfoModel model;
                //detect whether its a directory or file
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    var info = new DirectoryInfo(path);
                    model = new FileInfoModel
                    {
                        FileName = info.Name,
                        Created = info.CreationTimeUtc.ToLongDateString(),
                        LastUpdated = info.LastWriteTimeUtc.ToLongDateString(),
                        FileSize = string.Empty
                    };
                }
                else
                {
                    var info = new FileInfo(path);
                    model = new FileInfoModel
                    {
                        FileName = info.Name,
                        Created = info.CreationTimeUtc.ToLongDateString(),
                        LastUpdated = info.LastWriteTimeUtc.ToLongDateString(),
                        FileSize = string.Format("{0} Bytes", info.Length),
                    };
                }
                return Json(new ResponseModel
                    {
                        Success = true,
                        Data = model
                    });
                
            }
            catch(Exception exception)
            {
                return Json(new ResponseModel
                {
                    Success = true,
                    Message = exception.Message
                });
            }
        }

        #endregion

        public ActionResult ImageEditor(string virtualPath)
        {
            if (!virtualPath.StartsWith("/"))
            {
                virtualPath = "/" + virtualPath;
            }
            return View((object)virtualPath);
        }

        [HttpPost]
        public ActionResult ImageEditor(string virtualPath, string data, string newname, bool overwrite = false)
        {
            try
            {
                if (data == null)
                {
                    return Json(new { result = MediaEnums.EditImageEnums.SaveFail, message = "no data" });
                }
                if (!virtualPath.StartsWith("/"))
                {
                    virtualPath = "/" + virtualPath;
                }

                var imageFormat = FileInfoUtilities.GetImageFormatFromName(virtualPath);

                var physicalPath = _mediaFileManager.GetPhysicalPathFromVirtualPath(virtualPath);
                if (newname != null)
                {
                    var filename = Path.GetFileName(physicalPath);
                    if (filename != null)
                    {
                        physicalPath = physicalPath.Substring(0, physicalPath.Length - filename.Length) + newname;
                        if (!overwrite && System.IO.File.Exists(physicalPath))
                        {
                            return Json(new { result = MediaEnums.EditImageEnums.OverWriteConfirm });
                        }
                    }
                }
                ImageUtilities.SaveImageFromBase64String(data,
                    physicalPath,
                    imageFormat);
                return Json(new { result = MediaEnums.EditImageEnums.SaveSuccess });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;
                return Json(new { result = MediaEnums.EditImageEnums.SaveFail, message = ex.Message });
            }
        }

        public ImageResult Thumbnail(string path, int w = 0, int h = 0)
        {
            string filePath;
            if(string.IsNullOrEmpty(path))
            {
                filePath = Server.MapPath(Configurations.NoImage);
            }
            else
            {
                filePath = Path.Combine("~", path);
                filePath = Server.MapPath(filePath);
                if (!System.IO.File.Exists(filePath))
                {
                    filePath = Server.MapPath(Configurations.NoImage);
                }
            }
            return new ImageResult(filePath, w, h);
        }
    }
}
