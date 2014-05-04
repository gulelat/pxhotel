using System.Collections.Generic;
using System.Linq;

namespace PX.Core.Framework.Mvc.Models
{
    public class PaginationModel
    {
        public PaginationModel()
        {
            PageIndex = 0;
        }

        public PaginationModel(string search)
        {
            Search = search;
        }

        public IList<T> Paging<T>(IQueryable<T> pagingList)
        {
            PageIndex = PageIndex > 0 ? PageIndex : 1;
            if (string.IsNullOrEmpty(Order)) Order = "asc";
            if (string.IsNullOrEmpty(OrderName)) OrderName = "Id";
            Total = pagingList.Count();
            NumberOfPage = Total % PageSize == 0 ? Total / PageSize : Total / PageSize + 1;
            if (PageIndex > NumberOfPage) PageIndex = NumberOfPage;
            NextPage = PageIndex;
            PreviousPage = PageIndex;
            if (PageIndex > 1)
            {
                PreviousPage = PageIndex - 1;
            }
            if (PageIndex < NumberOfPage)
            {
                NextPage = PageIndex + 1;
            }
            return pagingList.OrderBy(OrderBy).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        #region public Properties

        public string Search { get; set; }

        public int NextPage { get; set; }

        public int PreviousPage { get; set; }

        private int _pageIndex { get; set; }

        public int PageIndex
        {
            get
            {
                return _pageIndex == 0 ? 1 : _pageIndex;
            }
            set { _pageIndex = value; }
        }

        public int PageSize { get; set; }

        public int NumberOfPage { get; set; }

        public int Total { get; set; }

        public string Order { get; set; }

        public string OrderName { get; set; }

        public string OrderBy { get { return string.Format("{0} {1}", OrderName, Order); } }

        #endregion
    }
}
