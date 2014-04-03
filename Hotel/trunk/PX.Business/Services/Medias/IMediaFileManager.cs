using System;
using System.IO;

namespace PX.Business.Services.Medias
{
    public interface IMediaFileManager
    {
        /// <summary>
        /// Gets the Media folder of the particular tenant
        /// </summary>
        DirectoryInfo MediaFolder { get; }

        /// <summary>
        /// Gets the Images folder of the particular tenant
        /// </summary>
        DirectoryInfo ImagesFolder { get; }

        /// <summary>
        /// Gets the Files folder of the particular tenant
        /// </summary>
        DirectoryInfo FilesFolder { get; }

        /// <summary>
        /// Gets the Themes folder for particular tenant
        /// </summary>
        DirectoryInfo ThemesFolder { get; }

        /// <summary>
        /// Gets the folder where Images are stored for specified module, useful for uploading images
        /// </summary>
        /// <param name="moduleId">The module id</param>
        /// <returns></returns>
        DirectoryInfo GetImageFolderForModule(string moduleId);

        /// <summary>
        /// Gets the folder where Files are stored for specified module, useful for uploading files
        /// </summary>
        /// <param name="moduleId">The module id</param>
        /// <returns></returns>
        DirectoryInfo GetFileFolderForModule(string moduleId);

        /// <summary>
        /// Gets the virtual path from physical path
        /// </summary>
        /// <param name="physicalPath">The physical path</param>
        /// <returns></returns>
        string GetVirtualMediaPathFromPhysicalPath(string physicalPath);

        /// <summary>
        /// Gets physical path from a virtual path which has the tenant name trimmed.
        /// </summary>
        /// <param name="virtualPath">The virtual path with tenant name trimmed</param>
        /// <returns></returns>
        string GetPhysicalPathFromVirtualPath(string virtualPath);

        /// <summary>
        /// Detect if a file is image
        /// </summary>
        /// <param name="filename">File name for testing</param>
        /// <returns>true if file is image, otherwise false</returns>
        Boolean IsImage(string filename);
    }
}
