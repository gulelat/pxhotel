using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PX.Business.Models.NewsCategories.CurlyBrackets;
using PX.Core.Ultilities;

namespace PX.Business.Models.News.CurlyBrackets
{
    public class NewsCurlyBracket : BaseModel
    {
        public NewsCurlyBracket()
        {

        }

        public NewsCurlyBracket(EntityModel.News news)
        {
            Id = news.Id;
            Title = news.Title;
            Description = news.Description;
            Content = news.Content;
            ImageUrl = news.ImageUrl;
            DetailsUrl = UrlUtilities.GenerateUrl(HttpContext.Current.Request.RequestContext, "News", "Details",
                                   new
                                   {
                                       id = news.Id,
                                       title = news.Title.ToUrlString()
                                   });
            LastUpdate = news.Updated ?? news.Created;
            LastUpdatedBy = news.Updated.HasValue ? news.UpdatedBy : news.CreatedBy;
            RecordOrder = news.RecordOrder;
            Created = news.Created;
            CreatedBy = news.CreatedBy;
            Updated = news.Updated;
            UpdatedBy = news.UpdatedBy;
            var newsCategories = news.NewsNewsCategories.Select(nc => nc.NewsCategory).ToList();
            Categories = newsCategories.Any()
                             ? newsCategories.Select(c => new CategoryItemModel(c)).ToList()
                             : new List<CategoryItemModel>();
        }

        #region Public Properties
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string DetailsUrl { get; set; }

        public string ImageUrl { get; set; }

        public DateTime LastUpdate { get; set; }

        public string LastUpdatedBy { get; set; }

        public List<CategoryItemModel> Categories { get; set; }

        #endregion
    }
}
