using PX.Business.Services.Templates;
using PX.EntityModel;

namespace PX.Business.Models.Templates
{
    public class TemplateRenderModel
    {
        public TemplateRenderModel(Template template)
        {
            Id = template.Id;
            Name = template.Name;
            CacheName = TemplateServices.GetTemplateCacheName(template.Name, template.Created, template.Updated);
            Content = template.Content;
        }

        #region Public Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string CacheName { get; set; }

        public string Content { get; set; }
        #endregion
    }
}
