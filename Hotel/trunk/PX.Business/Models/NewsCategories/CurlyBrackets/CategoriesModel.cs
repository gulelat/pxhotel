using System.Collections.Generic;

namespace PX.Business.Models.NewsCategories.CurlyBrackets
{
    public class CategoriesModel
    {
        public CategoriesModel()
        {
            Categories = new List<CategoryItemModel>();
        }

        #region Public Properties

        #endregion
        public List<CategoryItemModel> Categories { get; set; }
    }
}
