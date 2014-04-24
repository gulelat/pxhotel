using PX.EntityModel;

namespace PX.Business.Models.TemplateLogs
{
    public class TemplateLogManageModel : BaseModel
    {
        #region Constructors

        public TemplateLogManageModel()
        {
            
        }

        public TemplateLogManageModel(Template template)
        {
            TemplateId = template.Id;
            Name = template.Name;
            Content = template.Content;
            DataType = template.DataType;
        }
        #endregion

        #region Public Properties
        public int Id { get; set; }

        public int TemplateId { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public string DataType { get; set; }

        #endregion
    }
}
