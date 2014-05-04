using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class HotelRoomRepository : Repository<HotelRoom>
    {
        public HotelRoomRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
