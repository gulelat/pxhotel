using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class HotelBookingRepository : Repository<HotelBooking>
    {
        public HotelBookingRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
