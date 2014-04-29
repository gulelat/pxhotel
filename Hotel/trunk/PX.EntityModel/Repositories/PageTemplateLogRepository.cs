using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class PageTemplateLogRepository : HierarchyRepository<PageTemplateLog>
    {
        public PageTemplateLogRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
