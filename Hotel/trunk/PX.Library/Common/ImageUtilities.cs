using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using PX.Library.Configuration;

namespace PX.Library.Common
{
    public class ImageUtilities
    {
        public static void DeleteFile(string folder, string fileName)
        {
            folder = HttpContext.Current.Server.MapPath(folder);
            var path = folder + fileName; 
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static string SaveFileFromTempFolders(string folder, string fileName, string seoName)
        {
            try
            {
                var tmpFolder = HttpContext.Current.Server.MapPath(Configurations.TempFolder);
                folder = HttpContext.Current.Server.MapPath(folder);
                var path = tmpFolder + fileName;
                var extension = Path.GetExtension(fileName);
                if (File.Exists(path))
                {
                    fileName = GetRightNameToSave(folder, seoName + extension);
                    File.Move(path, folder + fileName);
                }
            }
            catch
            {
                return string.Empty;
            }
            return fileName;
        }

        public static void RemoveTempFiles(List<string> tempFiles)
        {
            try
            {
                var tmpFolder = HttpContext.Current.Server.MapPath(Configurations.TempFolder);

                foreach (var fileName in tempFiles)
                {
                    var path = tmpFolder + fileName;
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
            }
            catch { }
        }

        public static string CropImage(string folder, string filename, double x, double y, double width, double height, bool toThumbnail, int thumbnailWidth)
        {
            var tmpFolder = HttpContext.Current.Server.MapPath(Configurations.TempFolder);
            var cropRect = new Rectangle(Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(width), Convert.ToInt32(height));
            Bitmap image;
            var tmpPath = Path.Combine(tmpFolder, filename);
            using (image = Image.FromFile(tmpPath) as Bitmap)
            {
                var img = CropImage(image, cropRect);

                if (toThumbnail)
                {
                    if (width > thumbnailWidth)
                    {
                        height = (height * thumbnailWidth) / width;
                        img = img.GetThumbnailImage(thumbnailWidth, Convert.ToInt32(height), () => false, IntPtr.Zero);
                    }
                }

                filename = GetRightNameToSave(folder, filename);
                filename = GetRightNameToSave(tmpFolder, filename);

                var extension = Path.GetExtension(filename);
                switch (extension)
                {
                    case ".png":
                        img.Save(tmpFolder + filename, ImageFormat.Png);
                        break;
                    case ".gif":
                        img.Save(tmpFolder + filename, ImageFormat.Gif);
                        break;
                    default:
                        img.Save(tmpFolder + filename, ImageFormat.Jpeg);
                        break;
                }
                img.Dispose();
            }
            return filename;
        }

        public static string GetRightNameToSave(string path, string fileName)
        {
            var thumb = Path.GetFileNameWithoutExtension(fileName);
            var extension = Path.GetExtension(fileName);
            var suffix = 1;

            while (File.Exists(Path.Combine(path, thumb + extension)))
            {
                thumb = string.Format("{0}_{1}", thumb, suffix);
                suffix++;
            }

            return string.Format("{0}{1}", thumb, extension);
        }

        private static Image CropImage(Image img, Rectangle cropArea)
        {
            var bmpImage = new Bitmap(img);
            var bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            bmpImage.Dispose();
            return bmpCrop;
        }
    }
}
