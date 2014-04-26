using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class SiteSettingRepository : Repository<SiteSetting>
    {
        public SiteSetting GetByKey(string key)
        {
            return FetchFirst(s => s.Name.Equals(key));
        }
    }
}
