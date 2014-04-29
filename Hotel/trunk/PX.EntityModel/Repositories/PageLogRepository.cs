using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class PageLogRepository : HierarchyRepository<PageLog>
    {
        public PageLogRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
