//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PX.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class HotelRoomType
    {
        public HotelRoomType()
        {
            this.HotelBookingRooms = new HashSet<HotelBookingRoom>();
            this.HotelRoomImages = new HashSet<HotelRoomImage>();
            this.HotelRooms = new HashSet<HotelRoom>();
            this.HotelRoomServices = new HashSet<HotelRoomService>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalRooms { get; set; }
        public string Description { get; set; }
        public string MoreInformation { get; set; }
        public double Price { get; set; }
        public string RoomColor { get; set; }
        public int RecordOrder { get; set; }
        public bool RecordActive { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
    
        public virtual ICollection<HotelBookingRoom> HotelBookingRooms { get; set; }
        public virtual ICollection<HotelRoomImage> HotelRoomImages { get; set; }
        public virtual ICollection<HotelRoom> HotelRooms { get; set; }
        public virtual ICollection<HotelRoomService> HotelRoomServices { get; set; }
    }
}
