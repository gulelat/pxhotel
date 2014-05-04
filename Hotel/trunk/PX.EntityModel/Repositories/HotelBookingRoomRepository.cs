using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class HotelBookingRoomRepository : Repository<HotelBookingRoom>
    {
        public HotelBookingRoomRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
