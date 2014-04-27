using System.Collections.Generic;
using PX.Business.Models.NewsCategories;

namespace PX.Business.Models.News.CurlyBrackets
{
    public class NewsItemModel
    {
        #region Public Properties

        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public List<NewsCategoryModel> Categories { get; set; }

        #endregion
    }
}
