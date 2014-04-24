namespace PX.Business.Models.TemplateLogs
{
    public class TemplateLogModel : BaseModel
    {
        public int Id { get; set; }

        public int TemplateId { get; set; }

        public string Name { get; set; }
        
        public int? ParentId { get; set; }
    }
}
