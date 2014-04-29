using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class UserInGroupRepository : HierarchyRepository<UserInGroup>
    {
        public UserInGroupRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
