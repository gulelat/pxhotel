using PX.Core.Framework.Mvc.Models;
using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class PageTagRepository : HierarchyRepository<PageTag>
    {
        public PageTagRepository(PXHotelEntities entities)
            : base(entities)
        {
        }

        public ResponseModel Delete(int pageId, int tagId)
        {
            var entity = FetchFirst(t => t.PageId == pageId && t.TagId == tagId);
            if(entity != null)
            {
                return Delete(entity);
            }
            return new ResponseModel
                {
                    Success = true
                };
        }
    }
}
