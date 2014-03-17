using System.Collections.Generic;

namespace PX.Business.Models.MenuModels
{
    public class BreadCrumbModel
    {
        public BreadCrumbModel()
        {
            BreadCrumbs = new List<BreadCrumbItem>();
            CurrentBreadCrumbItem = new BreadCrumbItem();
        }

        public List<BreadCrumbItem> BreadCrumbs { get; set; }

        public BreadCrumbItem CurrentBreadCrumbItem { get; set; }
    }

    public class BreadCrumbItem
    {
        public string MenuIcon { get; set; }
        public string Url { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Name { get;set; }
    }
}
