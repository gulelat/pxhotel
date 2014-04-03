using System.Web;
using PX.Core.Configurations;
using PX.Core.Configurations.Constants;
using PX.Core.Ultilities;
using PX.Core.Ultilities.Files;

namespace PX.Business.Models.Medias
{
    public class ImageCropModel
    {
        public void CropImage()
        {
            var folder = HttpContext.Current.Server.MapPath(Folder);
            FileName = ImageUtilities.CropImage(folder, FileName, X, Y, Width, Height, ToThumbnail,
                                             DefaultConstants.ThumbnailWidth);
            CropStatus = !string.IsNullOrEmpty(FileName);
        }

        #region Public Properties

        public bool ToThumbnail { get; set; }

        public string TempFiles { get; set; }

        public double Ratio { get; set; }

        public string Folder { get; set; }

        public string TempFolder
        {
            get { return Configurations.TempFolder; }
        }

        public string FileName { get; set; }

        public string ImageUrl
        {
            get { return string.Format(Folder + FileName); }
        }

        public bool CropStatus { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        #endregion
    }
}
