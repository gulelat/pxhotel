using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class NewsRepository : Repository<News>
    {
        public NewsRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
