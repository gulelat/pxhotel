using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using PX.Business.Models.Medias;
using PX.Business.Mvc.Enums;
using PX.Business.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Logging;

namespace PX.Business.Services.Medias
{
    public class MediaServices : IMediaServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        public MediaServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
        }

        #region File Manager
        public string MapPath(string virtualPath)
        {
            return HostingEnvironment.MapPath(virtualPath);
        }
        public string MediaDefaultPath
        {
            get
            {
                return "/Media";
            }
        }

        /// <summary>
        /// Get the ~/App_Data folder
        /// </summary>
        public DirectoryInfo DataDirectory
        {
            get
            {
                var appDataPath = (string)AppDomain.CurrentDomain.GetData("DataDirectory") ??
                                  Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "App_Data");

                return new DirectoryInfo(appDataPath);
            }
        }

        /// <summary>
        /// Detect if a file is image
        /// </summary>
        /// <param name="filename">File name for testing</param>
        /// <returns>true if file is image, otherwise false</returns>
        public Boolean IsImage(string filename)
        {
            const string imagePattern = @"^.*\.(jpg|JPG|gif|GIF|png|PNG|jpeg|JPEG|tif|TIF)$";
            return Regex.IsMatch(filename, imagePattern);
        }

        public string ToRelativePath(string physicalPath)
        {
            if (Path.IsPathRooted(physicalPath))
            {
                if (HttpContext.Current.Request.PhysicalApplicationPath != null)
                    physicalPath = physicalPath.Replace(HttpContext.Current.Request.PhysicalApplicationPath, String.Empty);
                physicalPath = physicalPath.Replace("\\", "/");
                if (!physicalPath.StartsWith("/"))
                {
                    physicalPath = "/" + physicalPath;
                }
            }

            var mediaPath = physicalPath.Replace(MediaDefaultPath, "");
            physicalPath = string.Format("{0}{1}", MediaDefaultPath, mediaPath);
            return physicalPath;

        }

        public void CopyDirectory(string sourcePath, string destinationPath, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            var dir = new DirectoryInfo(sourcePath);
            var dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourcePath);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            // Get the files in the directory and copy them to the new location.
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                var temppath = Path.Combine(destinationPath, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (!copySubDirs) return;

            foreach (var subdir in dirs)
            {
                var temppath = Path.Combine(destinationPath, subdir.Name);
                CopyDirectory(subdir.FullName, temppath, true);
            }
        }

        /// <summary>
        /// Delete a directory and all of sub-folders/items
        /// </summary>
        /// <param name="path">Path to the directory to delete</param>
        public void DeleteDirectory(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists)
                throw new Exception("Folder not found at path " + path);

            DeleteDirectory(directoryInfo);
        }

        /// <summary>
        /// Delete a directory and all of sub-folders/items
        /// </summary>
        /// <param name="directory">Directory to delete</param>
        public void DeleteDirectory(DirectoryInfo directory)
        {
            foreach (var d in directory.GetDirectories())
            {
                DeleteDirectory(d);
            }
            foreach (var f in directory.GetFiles())
            {
                SetFileAttr(f.FullName);
                f.Delete();
            }
            directory.Delete();
        }

        private void SetFileAttr(string filepath)
        {
            if ((File.GetAttributes(filepath)
                   & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                File.SetAttributes(filepath, FileAttributes.Archive);
            }
        }

        #endregion

        #region Media

        private static readonly ILogger Logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly List<string> _images = new List<string> { ".jpeg", ".png", ".jpg", ".gif", ".ico" };

        public List<string> Images
        {
            get
            {
                return _images;
            }
        }

        /// <summary>
        /// Populate master tree
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="node"></param>
        public void PopulateTree(string relativePath, FileTreeModel node)
        {
            var physicalPath = HttpContext.Current.Server.MapPath(relativePath);
            if (node.children == null)
            {
                node.children = new List<FileTreeModel>();
            }
            var directory = new DirectoryInfo(physicalPath);

            //Loop through each subdirectory
            foreach (var d in directory.GetDirectories())
            {
                var mediaPath = ToRelativePath(string.Format("{0}/{1}", relativePath, d.Name));
                var t = new FileTreeModel
                {
                    attr = new FileTreeAttribute { Id = mediaPath, Rel = "folder" },
                    data = d.Name,
                    state = "closed"
                };

                node.children.Add(t);
            }

            //Loop through each file in master directory
            foreach (var f in directory.GetFiles())
            {
                var mediaPath = ToRelativePath(string.Format("{0}/{1}", relativePath, f.Name));
                var t = new FileTreeModel
                {
                    attr = new FileTreeAttribute { Id = mediaPath, @Class = "jstree-leaf" },
                    data = f.Name,
                    state = "open",
                };
                if (Images.Contains(f.Extension))
                {
                    t.attr.Rel = "image";
                    t.attr.IsImage = true;
                }
                else
                {
                    t.attr.Rel = "file";
                }
                node.children.Add(t);
            }
        }

        public List<FileTreeModel> PopulateChild(string relativePath)
        {
            var physicalPath = HttpContext.Current.Server.MapPath(relativePath);
            var directory = new DirectoryInfo(physicalPath);

            var children = directory.GetDirectories()
                .Select(d => new FileTreeModel
                {
                    attr =
                        new FileTreeAttribute
                        {
                            Id = string.Format("{0}/{1}", ToRelativePath(relativePath), d.Name),
                            Rel = "folder"
                        },
                    data = d.Name,
                    state = "closed"
                }).ToList();

            foreach (var f in directory.GetFiles())
            {
                var t = new FileTreeModel
                {
                    attr = new FileTreeAttribute { Id = string.Format("{0}/{1}", ToRelativePath(relativePath), f.Name), @Class = "jstree-leaf" },
                    data = f.Name,
                    state = "open"
                };
                if (Images.Contains(f.Extension))
                {
                    t.attr.Rel = "image";
                    t.attr.IsImage = true;
                }
                else
                {
                    t.attr.Rel = "file";
                }
                children.Add(t);
            }
            return children;
        }

        public ResponseModel MoveData(string source, string target, bool isCopy)
        {
            var response = new ResponseModel
                {
                    Success = false
                };
            var status = MediaEnums.MoveNodeStatusEnums.Success;
            try
            {
                var sourcePhysicalPath = HttpContext.Current.Server.MapPath(source);
                var targetPhysicalPath = HttpContext.Current.Server.MapPath(target);
                if (Directory.GetParent(sourcePhysicalPath).FullName.Equals(targetPhysicalPath))
                    status = MediaEnums.MoveNodeStatusEnums.MoveSameLocation;
                else
                {
                    // get the file attributes for file or directory
                    var attPath = File.GetAttributes(sourcePhysicalPath);
                    var attDestination = File.GetAttributes(targetPhysicalPath);

                    var fi = new FileInfo(sourcePhysicalPath);

                    if (attDestination != FileAttributes.Directory)
                    {
                        status = MediaEnums.MoveNodeStatusEnums.MoveNodeToFile;
                    }
                    else if (target.Contains(source))
                    {
                        status = MediaEnums.MoveNodeStatusEnums.MoveParentNodeToChild;
                    }
                    else
                    {
                        //detect whether its a directory or file
                        if ((attPath & FileAttributes.Directory) == FileAttributes.Directory)
                        {
                            //Move parent folder to children node
                            var targetFolder = string.Format("{0}/{1}", targetPhysicalPath, fi.Name);
                            MoveDirectory(sourcePhysicalPath, targetFolder, isCopy);
                        }
                        else
                        {
                            var fileName = Path.GetFileName(sourcePhysicalPath) ?? string.Empty;
                            fileName = GetRightFileNameToSave(targetPhysicalPath, fileName);
                            var targetFile = Path.Combine(targetPhysicalPath, fileName);
                            if (isCopy)
                            {
                                File.Copy(sourcePhysicalPath, targetFile);
                            }
                            else File.Move(sourcePhysicalPath, targetFile);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Warn(exception);
                status = MediaEnums.MoveNodeStatusEnums.Failure;
                response.Message = exception.Message;
            }
            switch (status)
            {
                case MediaEnums.MoveNodeStatusEnums.MoveParentNodeToChild:
                    response.Message =
                        _localizedResourceServices.T(
                            "AdminModule:::Media:::MoveData:::Cannot move parent folder to child folder. Please try again.");
                    break;
                case MediaEnums.MoveNodeStatusEnums.MoveNodeToFile:
                    response.Message =
                        _localizedResourceServices.T(
                            "AdminModule:::Media:::MoveData:::Cannot move item to file. Please try again.");
                    break;
                case MediaEnums.MoveNodeStatusEnums.MoveSameLocation:
                    response.Message =
                        _localizedResourceServices.T(
                            "AdminModule:::Media:::MoveData:::Cannot move item in same location. Please try again.");
                    break;
                case MediaEnums.MoveNodeStatusEnums.Failure:
                    response.Message =
                        string.Format(
                            _localizedResourceServices.T(
                                "AdminModule:::Media:::MoveData:::There's an error: while create new folder. Error Message: {0}. Please try again."),
                            response.Message);
                    break;
                case MediaEnums.MoveNodeStatusEnums.Success:
                    response.Message =
                        string.Format(
                            _localizedResourceServices.T(
                                "AdminModule:::Media:::MoveData:::Move data successfully."),
                            response.Message);
                    response.Success = true;
                    break;
            }
            return response;
        }

        public void MoveDirectory(string source, string target, bool isCopy)
        {
            var stack = new Stack<Folders>();
            stack.Push(new Folders(source, target));

            while (stack.Count > 0)
            {
                var folders = stack.Pop();
                Directory.CreateDirectory(folders.Target);
                foreach (var file in Directory.GetFiles(folders.Source, "*.*"))
                {
                    var fileName = Path.GetFileName(file) ?? string.Empty;
                    fileName = GetRightFileNameToSave(folders.Target, fileName);
                    var targetFile = Path.Combine(folders.Target, fileName);
                    if (isCopy)
                        File.Copy(file, targetFile);
                    else
                        File.Move(file, targetFile);
                }

                foreach (var folder in Directory.GetDirectories(folders.Source))
                {
                    var folderName = Path.GetFileName(folder) ?? string.Empty;
                    stack.Push(new Folders(folder, Path.Combine(folders.Target, folderName)));
                }
            }
            if (!isCopy)
                Directory.Delete(source, true);
        }

        public ResponseModel CreateFolder(string relativePath)
        {
            var physicalPath = HttpContext.Current.Server.MapPath(relativePath);
            try
            {
                if (Directory.Exists(physicalPath))
                {
                    return new ResponseModel
                        {
                            Success = false,
                            Data = relativePath,
                            Message = _localizedResourceServices.T("AdminModule:::Media:::CreateFolder:::Folder name has existed in the system. Please you another one.")
                        };
                }
                Directory.CreateDirectory(physicalPath);
            }
            catch (Exception exception)
            {
                Logger.Warn(exception);
                return new ResponseModel
                {
                    Success = false,
                    Data = relativePath,
                    Message = string.Format(_localizedResourceServices.T("AdminModule:::Media:::CreateFolder:::There's an error: while create new folder. Error Message: {0}. Please try again."), exception.Message)
                };
            }
            return new ResponseModel
            {
                Success = true,
                Data = relativePath,
                Message = _localizedResourceServices.T("AdminModule:::Media:::CreateFolder:::Create folder successfully.")
            };
        }

        public ResponseModel DeletePath(string relativePath)
        {
            try
            {
                var fullPath = HttpContext.Current.Server.MapPath(relativePath);
                var attr = File.GetAttributes(fullPath);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    Directory.Delete(fullPath, true);
                }
                else
                {
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }
                }
                return new ResponseModel
                    {
                        Success = true,
                        Message = _localizedResourceServices.T("AdminModule:::Media:::DeleteItem:::Delete file/folder successfully.")
                    };
            }
            catch (Exception exception)
            {
                Logger.Warn(exception);
                return new ResponseModel
                {
                    Success = false,
                    Message = _localizedResourceServices.T("AdminModule:::Media:::DeleteItem:::Delete file/folder failure." + exception.Message)
                };
            }
        }

        public ResponseModel Rename(string relativePath, string name, out string path)
        {
            var response = new ResponseModel
                {
                    Success = false
                };
            path = string.Empty;
            var status = MediaEnums.RenameStatusEnums.Failure;
            try
            {
                var physicalPath = HttpContext.Current.Server.MapPath(relativePath);
                var newPath = Directory.GetParent(physicalPath).FullName;

                // Check if current name and newname is the same
                var attPath = File.GetAttributes(physicalPath);
                if (attPath == FileAttributes.Directory)
                {
                    var diretoryInfo = new DirectoryInfo(physicalPath);
                    if (diretoryInfo.Name.Equals(name))
                    {
                        status = MediaEnums.RenameStatusEnums.Success;
                    }
                    else if (Directory.Exists(Path.Combine(newPath, name)))
                    {
                        status = MediaEnums.RenameStatusEnums.DuplicateName;
                    }
                }
                else
                {
                    var currentFileName = Path.GetFileName(physicalPath);
                    if (currentFileName != null && currentFileName.Equals(name))
                    {
                        status = MediaEnums.RenameStatusEnums.Success;
                    }
                    else if (File.Exists(Path.Combine(newPath, name)))
                    {
                        status = MediaEnums.RenameStatusEnums.DuplicateName;
                    }
                    else
                    {
                        var position = physicalPath.IndexOf(relativePath.Replace("/", "\\"), StringComparison.Ordinal);
                        if (position > 0)
                        {
                            var folder = ToRelativePath(newPath.Substring(position).Replace("\\", "/"));
                            newPath = Path.Combine(newPath, name);
                            Directory.Move(physicalPath, newPath);
                            path = string.Format("{0}/{1}", folder, name);
                            status = MediaEnums.RenameStatusEnums.Success;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Warn(exception);
                response.Message = exception.Message;
            }

            switch (status)
            {
                case MediaEnums.RenameStatusEnums.DuplicateName:
                    response.Message =
                        _localizedResourceServices.T(
                            "AdminModule:::Media:::RenameFolder:::The name of file/folder has already existed. Please rename and try again.");
                    break;
                case MediaEnums.RenameStatusEnums.Failure:
                    response.Message =
                        string.Format(_localizedResourceServices.T(
                            "AdminModule:::Media:::RenameFolder:::Error while rename file/folder. Error Message: {0}. Please try again."), response.Message);
                    break;
                default:
                    response.Message =
                        _localizedResourceServices.T(
                            "AdminModule:::Media:::RenameFolder:::Rename successfully.");
                    response.Success = true;
                    break;
            }
            return response;
        }

        public string GetRightFileNameToSave(string targetFolder, string file)
        {
            var thumb = Path.GetFileNameWithoutExtension(file);
            var name = thumb;
            var extension = Path.GetExtension(file);
            var suffix = 1;

            while (File.Exists(Path.Combine(targetFolder, thumb + extension)))
            {
                thumb = string.Format("{0}({1})", name, suffix);
                suffix++;
            }

            return string.Format("{0}{1}", thumb, extension);
        }
        #endregion
    }
}