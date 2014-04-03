using System;
using System.IO;
using System.Web;
using PX.Core.Configurations;
using PX.Core.Ultilities;
using PX.Core.Ultilities.Files;

namespace PX.Business.Models.Medias
{
    public class FileUploadViewModel
    {
        public FileUploadViewModel()
        {
        }

        public FileUploadViewModel(string qqFile)
            : this()
        {
            FileContent = qqFile;
        }

        public void Upload()
        {
            string tempFolder = HttpContext.Current.Server.MapPath(Configurations.TempFolder);

            try
            {
                var stream = HttpContext.Current.Request.InputStream;
                var filePath = string.Empty;
                string fileName;
                if (String.IsNullOrEmpty(HttpContext.Current.Request["qqfile"]))
                {
                    // IE
                    var postedFile = HttpContext.Current.Request.Files[0];
                    stream = postedFile.InputStream;
                    fileName = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);
                }
                else
                {
                    //Webkit, Mozilla
                    fileName = FileContent;
                }
                if (fileName != null)
                {
                    fileName = ImageUtilities.GetRightNameToSave(tempFolder, fileName);

                    filePath = Path.Combine(tempFolder, fileName);
                    FullPath = string.Format("/Images/temp/{0}", fileName);
                }

                FileName = fileName;
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                File.WriteAllBytes(filePath, buffer);
                stream.Close();
                UploadStatus = true;
            }
            catch (Exception ex)
            {
                UploadStatus = false;
            }
        }

        #region Public Properties

        public string FileContent { get; set; }

        public bool UploadStatus { get; set; }

        public string FullPath { get; set; }

        public string FileName { get; set; }

        #endregion
    }
}