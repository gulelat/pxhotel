using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class SettingTypeRepository : Repository<SettingType>
    {
        public SettingTypeRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
