using System.Collections.Generic;

namespace PX.Business.Models.RotatingImageGroups
{
    public class GroupGalleryModel
    {
        public GroupGalleryModel()
        {
            GalleryItems = new List<GalleryItemModel>();
        }

        public int Id { get; set; }

        public string GroupName { get; set; }

        public List<GalleryItemModel> GalleryItems { get; set; }
    }

    public class GalleryItemModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public int RecordOrder { get; set; }
    }
}
