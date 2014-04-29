using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class RotatingImageGroupRepository : Repository<RotatingImageGroup>
    {
        public RotatingImageGroupRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
