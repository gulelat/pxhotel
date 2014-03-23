using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.Menus
{
    public class MenuModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Url { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        public string Hierarchy { get; set; }

        [Required]
        public string MenuIcon { get; set; }

        [Required]
        public bool Visible { get; set; }

        public string VisibleString { get { return Visible ? "Yes" : "No"; } set { Visible = value.Equals("Yes"); } }
    }
}
