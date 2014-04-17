using PX.Business.Models.FileTemplates;

namespace PX.Business.Models.Pages
{
    public class PageRenderModel
    {
        public bool IsFileTemplate { get; set; }

        public TemplateModel FileTemplateModel { get; set; } 

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
