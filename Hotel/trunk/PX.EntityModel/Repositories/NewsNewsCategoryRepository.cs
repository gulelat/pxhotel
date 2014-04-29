using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class NewsNewsCategoryRepository : Repository<NewsNewsCategory>
    {
        public NewsNewsCategoryRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
