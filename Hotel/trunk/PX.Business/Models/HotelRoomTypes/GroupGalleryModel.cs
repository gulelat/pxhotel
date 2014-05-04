using System.Collections.Generic;

namespace PX.Business.Models.HotelRoomTypes
{
    public class RoomTypeGalleryModel
    {
        public RoomTypeGalleryModel()
        {
            GalleryItems = new List<GalleryItemModel>();
        }

        public int Id { get; set; }

        public string RoomTypeName { get; set; }

        public List<GalleryItemModel> GalleryItems { get; set; }
    }

    public class GalleryItemModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public bool IsDefaultImage { get; set; }

        public int RecordOrder { get; set; }
    }
}
