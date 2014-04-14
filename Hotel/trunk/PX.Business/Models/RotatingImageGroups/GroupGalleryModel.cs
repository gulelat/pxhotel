using System.Collections.Generic;

namespace PX.Business.Models.RotatingImageGroups
{
    public class GroupGalleryModel
    {
        public int Id { get; set; }

        public string GroupName { get; set; }

        public List<GalleryItemModel> GalleryItems { get; set; }
    }

    public class GalleryItemModel
    {
        public string Url { get; set; }

        public int RecordOrder { get; set; }
    }
}
