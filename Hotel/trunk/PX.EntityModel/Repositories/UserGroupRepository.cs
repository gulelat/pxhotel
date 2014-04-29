using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class UserGroupRepository : Repository<UserGroup>
    {
        public UserGroupRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
