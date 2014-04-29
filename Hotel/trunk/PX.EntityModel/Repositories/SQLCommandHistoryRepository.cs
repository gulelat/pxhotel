using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class SQLCommandHistoryRepository : Repository<SQLCommandHistory>
    {
        public SQLCommandHistoryRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
