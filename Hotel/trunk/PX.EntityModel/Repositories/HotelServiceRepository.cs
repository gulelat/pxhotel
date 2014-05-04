using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class HotelServiceRepository : Repository<HotelService>
    {
        public HotelServiceRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
