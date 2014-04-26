using PX.Business.Models.FileTemplates;
using PX.EntityModel;

namespace PX.Business.Models.Pages
{
    public class PageRenderModel
    {
        #region Constructor
        public PageRenderModel()
        {
            IsFileTemplate = false;
            Title = string.Empty;
            Keywords = string.Empty;
            Content = string.Empty;
            Caption = string.Empty;
        }

        public PageRenderModel(Page page)
        {
            IsFileTemplate = page.FileTemplateId.HasValue;
            if (page.FileTemplateId.HasValue)
            {
                FileTemplateModel = new TemplateModel
                {
                    Name = page.FileTemplate.Name,
                    Action = page.FileTemplate.Action,
                    Controller = page.FileTemplate.Controller,
                    Parameters = string.Format("activePageId={0}&{1}", page.Id, page.FileTemplate.Parameters),
                };
            }
            Id = page.Id;
            Title = page.Title;
            Content = page.Content;
            Keywords = page.Keywords;
            Caption = page.Caption;
        }

        #endregion

        #region Public Properties

        public int Id { get; set; }

        public bool IsFileTemplate { get; set; }

        public TemplateModel FileTemplateModel { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Keywords { get; set; }

        public string Caption { get; set; }

        #endregion
    }
}
