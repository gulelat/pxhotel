namespace PX.Business.Models.HotelRoomImages
{
    public class HotelRoomImageManageModel
    {
        public int? Id { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public int HotelRoomTypeId { get; set; }

        public int RecordOrder { get; set; }
    }
}
