using System.Web;
using PX.Core.Ultilities;
using PX.EntityModel;

namespace PX.Business.Models.NewsCategories.CurlyBrackets
{
    public class CategoryItemModel
    {
        public CategoryItemModel()
        {
            
        }

        public CategoryItemModel(NewsCategory category)
        {
            Id = category.Id;
            Name = category.Name;
            Description = category.Description;
            ParentId = category.ParentId;
            ParentName = category.ParentId.HasValue ? category.NewsCategory1.Name : string.Empty;
        }

        #region Public Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        #endregion
    }
}
