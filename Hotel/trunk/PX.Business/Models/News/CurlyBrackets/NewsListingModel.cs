using System.Collections.Generic;

namespace PX.Business.Models.News.CurlyBrackets
{
    public class NewsListingModel
    {
        public NewsListingModel()
        {
            NewsListing = new List<NewsCurlyBracket>();
        }

        #region Public Properties
        public int PageIndex { get; set; }

        public List<NewsCurlyBracket> NewsListing { get; set; } 

        #endregion
    }
}