using System.Collections.Generic;

namespace PX.Business.Models.Menus
{
    public class AdminMenuModel
    {
        public int Id { get; set; }
        public string MenuIcon { get; set; }
        public string Url { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Name { get; set; }
        public string Hierarchy { get; set; }
        public int RecordOrder { get; set; }

        public List<AdminMenuModel> ChildMenus { get; set; } 
    }
}
