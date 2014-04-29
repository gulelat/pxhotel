using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class PageRepository : HierarchyRepository<Page>
    {
        public PageRepository(PXHotelEntities entities) : base(entities)
        {
            DataContext = entities;
        }
    }
}
