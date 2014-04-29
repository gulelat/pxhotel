using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
