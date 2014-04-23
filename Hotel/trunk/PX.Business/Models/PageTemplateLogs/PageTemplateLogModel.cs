namespace PX.Business.Models.PageTemplateLogs
{
    public class PageTemplateLogModel : BaseModel
    {
        public int Id { get; set; }

        public int PageTemplateId { get; set; }

        public string Name { get; set; }
        
        public int? ParentId { get; set; }
    }
}
