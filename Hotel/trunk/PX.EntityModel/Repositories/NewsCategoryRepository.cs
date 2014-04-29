using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class NewsCategoryRepository : HierarchyRepository<NewsCategory>
    {
        public NewsCategoryRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
