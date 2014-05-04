using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class HotelRoomTypeRepository : Repository<HotelRoomType>
    {
        public HotelRoomTypeRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
