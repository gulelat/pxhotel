using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class TemplateLogRepository : HierarchyRepository<TemplateLog>
    {
        public TemplateLogRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
