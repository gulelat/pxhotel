using System;
using System.Collections.Generic;
using System.IO;
using PX.Business.Models.Media;
using PX.Core.Framework.Mvc.Models;

namespace PX.Business.Services.Media
{
    public interface IMediaServices
    {
        #region File Manager
        string MapPath(string virtualPath);
        string MediaDefaultPath { get; }
        DirectoryInfo DataDirectory { get; }

        /// <summary>
        /// Gets the current site media directory
        /// </summary>
        /// <returns></returns>
        Boolean IsImage(string filename);

        string ToRelativePath(string physicalPath);

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
        ResponseModel MoveData(string source, string target, bool isCopy);
        void MoveDirectory(string source, string target, bool isCopy);
        ResponseModel CreateFolder(string relativePath);
        ResponseModel DeletePath(string relativePath);
        ResponseModel Rename(string relativePath, string name, out string path);
        string GetRightFileNameToSave(string targetFolder, string file);

        #endregion
    }
}