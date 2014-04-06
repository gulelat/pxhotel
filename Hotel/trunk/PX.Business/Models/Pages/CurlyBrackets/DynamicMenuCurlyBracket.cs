using System.Collections.Generic;

namespace PX.Business.Models.Pages.CurlyBrackets
{
    public class DynamicMenuCurlyBracket
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public int Order { get; set; }

        public string ChildMenusString { get; set; }

        public List<DynamicMenuCurlyBracket> ChildMenus { get; set; }
    }
}
