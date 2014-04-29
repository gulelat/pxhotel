using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class RotatingImageRepository : Repository<RotatingImage>
    {
        public RotatingImageRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
