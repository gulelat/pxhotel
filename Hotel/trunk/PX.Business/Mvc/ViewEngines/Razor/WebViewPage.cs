using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PX.Business.Services.Localizes;
using PX.Business.Services.Settings;
using PX.Core.Framework.Mvc.Environment;
using PX.EntityModel;

namespace PX.Business.Mvc.ViewEngines.Razor
{
    public class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        private ILocalizedResourceServices _localizedResourceServices;
        private ISettingServices _settingServices;

        public override void InitHelpers()
        {
            _localizedResourceServices = DependencyFactory.GetInstance<ILocalizedResourceServices>();
            _settingServices = DependencyFactory.GetInstance<ISettingServices>();
            base.InitHelpers();
        }

        public override void Execute()
        {
        }

        #region Multi Languages Helpers
        public string T(string key)
        {
            return _localizedResourceServices.T(key);
        }

        /// <summary>
        /// Render a tag with text in multi language
        /// </summary>
        /// <param name="tagName">Tag name to render</param>
        /// <param name="textKey">Key of the text</param>
        /// <param name="defaultValue">Default value of the text</param>
        /// <returns>Rendered Tag</returns>
        public IHtmlString MText(Tags tagName,
                                 string textKey,
                                 string defaultValue)
        {
            return MText(tagName, textKey, defaultValue, null);
        }

        /// <summary>
        /// Render a tag with text in multi language
        /// </summary>
        /// <param name="tagName">Tag name to render</param>
        /// <param name="textKey">Key of the text</param>
        /// <param name="defaultValue">Default value of the text</param>
        /// <param name="htmlAttributes">Html attributes for the tag</param>
        /// <param name="parameters">Parameters for the string</param>
        /// <returns>Rendered Tag</returns>
        public IHtmlString MText(Tags tagName,
            string textKey,
            string defaultValue,
            object htmlAttributes,
            object[] parameters = null)
        {
            var text = _localizedResourceServices.GetLocalizedResource(textKey, defaultValue, parameters);

            var tag = new TagBuilder(tagName.ToString().ToLower());
            tag.Attributes.Add("language-edit", "true");
            tag.Attributes.Add("data-textKey", textKey);
            tag.SetInnerText(text);
            tag.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return new MvcHtmlString(tag.ToString(TagRenderMode.Normal));
        }

        public enum Tags
        {
            P,
            Label,
            Span,
            Div,
            Button,
            A,
            Th,
            Text
        }
        #endregion

        #region Setting Helpers
        public T SValue<T>(string key)
        {
            return _settingServices.GetSetting<T>(key);
        }
        #endregion

        public User CurrentUser
        {
            get { return (User)HttpContext.Current.Session["CurrentUser"]; }
            set { HttpContext.Current.Session["CurrentUser"] = value; }
        }
    }
}
