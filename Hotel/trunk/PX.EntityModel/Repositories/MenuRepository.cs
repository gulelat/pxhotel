using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class MenuRepository : HierarchyRepository<Menu>
    {
        public MenuRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
