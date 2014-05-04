using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class HotelRoomImageRepository : Repository<HotelRoomImage>
    {
        public HotelRoomImageRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
