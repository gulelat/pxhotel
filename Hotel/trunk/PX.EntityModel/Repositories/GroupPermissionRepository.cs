using System.Linq;
using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class GroupPermissionRepository : Repository<GroupPermission>
    {
        public static IQueryable<GroupPermission> GetByGroupId(int id)
        {
            return Fetch(p => p.UserGroupId == id);
        }
    }
}
