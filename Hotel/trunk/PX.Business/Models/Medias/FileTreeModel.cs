using System.Collections.Generic;

namespace PX.Business.Models.Medias
{
    public class FileTreeModel
    {
        public string data;
        public FileTreeAttribute attr;
        public string state;
        public List<FileTreeModel> children;
    }

    public class FileTreeAttribute
    {
        public string Id { get; set; }
        public string Rel { get; set; }
        public string @Class { get; set; }
        public bool IsImage { get; set; }
    }

    public class Folders
    {
        public string Source { get; private set; }
        public string Target { get; private set; }

        public Folders(string source, string target)
        {
            Source = source;
            Target = target;
        }
    }
}
