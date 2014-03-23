namespace PX.Business.Models.Pages
{
    public class PageModel : BaseModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int? PageTemplateId { get; set; }

        public string PageTemplateName { get; set; }

        public int Status { get; set; }

        public string FriendlyUrl { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        public string Hierarchy { get; set; }
    }
}
