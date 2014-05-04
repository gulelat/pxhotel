using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class HotelRoomServiceRepository : Repository<HotelRoomService>
    {
        public HotelRoomServiceRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
