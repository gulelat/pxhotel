using System.Collections.Generic;

namespace PX.Business.Models.ClientMenus.CurlyBrackets
{
    public class DynamicMenuCurlyBracket
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int Order { get; set; }

        public int Level { get; set; }

        public string ChildMenusString { get; set; }

        public string ChildMenusMobileString { get; set; }

        public List<DynamicMenuCurlyBracket> ChildMenus { get; set; }
    }
}
