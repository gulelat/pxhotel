using System.Collections.Generic;
using System.Web;
using PX.Business.Models.News.CurlyBrackets;
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
            Total = category.NewsNewsCategories.Count;

            DetailsUrl = UrlUtilities.GenerateUrl(HttpContext.Current.Request.RequestContext, "NewsCategory", "Details",
                                                  new
                                                      {
                                                          id = category.Id,
                                                          title = category.Name.ToUrlString()
                                                      });
        }

        #region Public Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public string DetailsUrl { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        public int Total { get; set; }

        public List<NewsCurlyBracket> NewsListing { get; set; }

        #endregion
    }
}