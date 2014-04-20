using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PX.Business.Services.Localizes;
using PX.Business.Services.Settings;
using PX.Core.Configurations;
using PX.Core.Framework.Mvc.Environments;
using PX.EntityModel;

namespace PX.Business.Mvc.ViewEngines.Razor
{
    public class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        private ControllerBase _currentController;
        private ILocalizedResourceServices _localizedResourceServices;
        private ISettingServices _settingServices;

        public override void InitHelpers()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _settingServices = HostContainer.GetInstance<ISettingServices>();
            _currentController = (ControllerBase)HttpContext.Current.Items[Configurations.PxHotelCurrentController];
            base.InitHelpers();
        }

        public override void Execute()
        {
        }

        #region Multi Languages Helpers
        /// <summary>
        /// Get translated value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string T(string key)
        {
            return _localizedResourceServices.T(key);
        }

        /// <summary>
        /// Get translated value by key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public string T(string key, string defaultValue)
        {
            return _localizedResourceServices.T(key, defaultValue);
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

        #region Message Helpers

        #region Private Properties

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public String ErrorMessage
        {
            get
            {
                if (_currentController == null) return null;
                return (string)_currentController.TempData[Configurations.ErrorMessage];
            }
            set
            {
                if (_currentController == null) return;
                _currentController.TempData[Configurations.ErrorMessage] = value;
            }
        }

        /// <summary>
        /// Gets or sets the warning message.
        /// </summary>
        public String WarningMessage
        {
            get
            {
                if (_currentController == null) return null;
                return (string)_currentController.TempData[Configurations.WarningMessage];
            }
            set
            {
                if (_currentController == null) return;
                _currentController.TempData[Configurations.WarningMessage] = value;
            }
        }

        /// <summary>
        /// Gets or sets the status message.
        /// </summary>
        public String SuccessMessage
        {
            get
            {
                if (_currentController == null) return null;
                return (string)_currentController.TempData[Configurations.SuccessMessage];
            }
            set
            {
                if (_currentController == null) return;
                _currentController.TempData[Configurations.SuccessMessage] = value;
            }
        }

        #endregion

        /// <summary>
        /// Show status message after page refresh
        /// </summary>
        /// <returns></returns>
        public IHtmlString ShowStatusMessage()
        {
            var template = @"<div class=""alert alert-{0}"">
<button class=""close"" data-dismiss=""alert"" type=""button"">
<i class=""icon-remove""></i>
</button>
{1}
<br>
</div>";
            if (!string.IsNullOrEmpty(SuccessMessage))
            {
                template = string.Format(template, "info", SuccessMessage);
            }
            else if (!string.IsNullOrEmpty(ErrorMessage))
            {
                template = string.Format(template, "danger", ErrorMessage);
            }
            else if (!string.IsNullOrEmpty(WarningMessage))
            {
                template = string.Format(template, "warning", WarningMessage);
            }
            else
            {
                return new HtmlString(string.Empty);
            }
            return new HtmlString(template);
        }

        #endregion

        #region Image Helpers
        public string Thumbnail(string filePath, int width, int height)
        {
            return Url.Action("Thumbnail", "Media", new { area = "Admin", path = filePath, w = width, h = height });
        }
        #endregion

        /// <summary>
        /// Get current logged in user
        /// </summary>
        public User CurrentUser
        {
            get { return WorkContext.WorkContext.CurrentUser; }
            set { WorkContext.WorkContext.CurrentUser = value; }
        }
    }
}
