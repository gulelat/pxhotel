using System;
using System.IO;
using System.Web.Mvc;
using PX.Business.Mvc.Enums;
using PX.Business.Services.Medias;
using PX.Core.Ultilities.Files;

namespace PX.Web.Areas.Admin.Controllers
{
    public class ImageEditorController : Controller
    {
        private readonly IMediaFileManager _mediaFileManager;

        public ImageEditorController(IMediaFileManager mediaFileManager)
        {
            _mediaFileManager = mediaFileManager;
        }

        public ActionResult Index(string virtualPath)
        {
            if (!virtualPath.StartsWith("/"))
            {
                virtualPath = "/" + virtualPath;
            }
            return View((object)virtualPath);
        }

        [HttpPost]
        public ActionResult Index(string virtualPath, string data, string newname, bool overwrite = false)
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

    }
}
