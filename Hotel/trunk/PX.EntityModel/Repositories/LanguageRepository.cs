using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class LanguageRepository : Repository<Language>
    {
        public LanguageRepository(PXHotelEntities entities)
            : base(entities)
        {
        }
    }
}
