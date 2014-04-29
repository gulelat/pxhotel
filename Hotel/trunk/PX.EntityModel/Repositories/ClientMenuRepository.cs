using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class ClientMenuRepository : HierarchyRepository<ClientMenu>
    {
        public ClientMenuRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
