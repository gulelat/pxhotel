using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class PageTemplateRepository : HierarchyRepository<PageTemplate>
    {
        public PageTemplateRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
