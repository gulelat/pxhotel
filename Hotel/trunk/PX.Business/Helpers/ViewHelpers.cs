using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace PX.Business.Helpers
{
    public static class ViewHelpers
    {
        public static MvcHtmlString PartialFor<TModel, TProperty>(this HtmlHelper<TModel> helper, System.Linq.Expressions.Expression<Func<TModel, TProperty>> expression, string partialViewName)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var model = ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model;
            var viewData = new ViewDataDictionary(helper.ViewData)
            {
                TemplateInfo = new TemplateInfo
                {
                    HtmlFieldPrefix = name
                }
            };

            return helper.Partial(partialViewName, model, viewData);
        }
    }
}
