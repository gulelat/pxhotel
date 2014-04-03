using System;
using System.Collections.Generic;
using System.IO;
using PX.Business.Models.Medias;
using PX.Business.Mvc.Enums;

namespace PX.Business.Services.Medias
{
    public interface IMediaServices
    {
        #region File Manager
        string MapPath(string virtualPath);
        string MediaDefaultPath { get; }
        DirectoryInfo DataDirectory { get; }
        DirectoryInfo SitesDirectory { get; }
        DirectoryInfo CurrentSiteSettingDirectory { get; }

        DirectoryInfo MediaDirectory();
        /// <summary>
        /// Gets the current site media directory
        /// </summary>
        /// <returns></returns>
        DirectoryInfo CurrentSiteMediaDirectory();
        DirectoryInfo CurrentSiteImagesDirectory();
        DirectoryInfo GetDirectoryForImagesUpload(string moduleName);
        DirectoryInfo CurrentSiteFilesDirectory();
        DirectoryInfo GetDirectoryForFilesUpload(string moduleName);
        Boolean IsImage(string filename);
        string ToRelativePath(string mediaPath);
        string ToMediaPath(string physicalPath);

        /// <summary>
        /// Copy a directory
        /// </summary>
        /// <param name="sourcePath">Path to the source directory</param>
        /// <param name="destinationPath">Path to the destination</param>
        /// <param name="copySubDirs">Enable copy sub directories or not</param>
        void CopyDirectory(string sourcePath, string destinationPath, bool copySubDirs);

        /// <summary>
        /// Delete a directory and all of sub-folders/items
        /// </summary>
        /// <param name="path">Path to the directory to delete</param>
        void DeleteDirectory(string path);

        /// <summary>
        /// Delete a directory and all of sub-folders/items
        /// </summary>
        /// <param name="directory">Directory to delete</param>
        void DeleteDirectory(DirectoryInfo directory);

        #endregion

        #region Media
        List<string> Images { get; }

        /// <summary>
        /// Populate master tree
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="node"></param>
        void PopulateTree(string relativePath, FileTreeModel node);

        List<FileTreeModel> PopulateChild(string relativePath);
        MediaEnums.MoveNodeStatusEnums MoveData(string source, string target, bool isCopy);
        void MoveDirectory(string source, string target, bool isCopy);
        bool CreateFolder(string relativePath);
        bool DeletePath(string relativePath);
        MediaEnums.RenameStatusEnums Rename(string relativePath, string name, out string path);
        string GetRightFileNameToSave(string targetFolder, string file);

        #endregion
    }
}