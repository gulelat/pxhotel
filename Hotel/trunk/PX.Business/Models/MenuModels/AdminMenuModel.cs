using System.Collections.Generic;

namespace PX.Business.Models.MenuModels
{
    public class AdminMenuModel
    {
        public string MenuIcon { get; set; }
        public string Url { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Name { get; set; }

        public List<AdminMenuModel> ChildMenus { get; set; } 
    }
}
