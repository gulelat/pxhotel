using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PX.Business.Services.Localizes;
using PX.Business.Services.Settings;
using PX.Core.Configurations;
using PX.Core.Framework.Mvc.Environments;
using RazorEngine.Templating;

namespace PX.Business.Mvc.ViewEngines.Razor.RazorEngine
{
    [RequireNamespaces("System.Web.Mvc.Html")]
    public class RazorEngineTemplateBase<TModel> : TemplateBase<TModel>
    {
        #region Html/Url Helpers
        private UrlHelper _urlhelper;

        //private ViewDataDictionary _viewdata;
        //private readonly System.Dynamic.DynamicObject _viewbag = null;
        //private HtmlHelper<TModel> _helper;
        //public HtmlHelper<TModel> Html
        //{
        //    get
        //    {
        //        if (_helper == null)
        //        {
        //            var p = WebPageContext.Current;
        //            var wvp = p.Page as WebViewPage;
        //            var context = wvp != null ? wvp.ViewContext : null;
        //            _helper = new HtmlHelper<TModel>(context, this, RouteTable.Routes);
        //        }
        //        return _helper;
        //    }
        //}
        //public ViewDataDictionary ViewData
        //{
        //    get
        //    {
        //        if (_viewbag == null)
        //        {
        //            var p = WebPageContext.Current;
        //            var viewcontainer = p.Page as IViewDataContainer;
        //            _viewdata = new ViewDataDictionary(viewcontainer.ViewData);

        //            if (Model != null)
        //            {
        //                _viewdata.Model = Model;
        //            }

        //        }

        //        return _viewdata;
        //    }
        //    set
        //    {
        //        _viewdata = value;
        //    }

        //}

        public UrlHelper Url
        {
            get { return _urlhelper ?? (_urlhelper = new UrlHelper(HttpContext.Current.Request.RequestContext)); }
        }

        #endregion

        private readonly ControllerBase _currentController;
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly ISettingServices _settingServices;
        public RazorEngineTemplateBase()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _settingServices = HostContainer.GetInstance<ISettingServices>();
            _currentController = (ControllerBase)HttpContext.Current.Items[Configurations.PxHotelCurrentController];
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

        #region Setting Helpers
        public T SValue<T>(string key)
        {
            return _settingServices.GetSetting<T>(key);
        }
        #endregion

        #region Image Helpers
        public string Thumbnail(string filePath, int width, int height)
        {
            return Url.Action("Thumbnail", "Media", new { area = "Admin", path = filePath, w = width, h = height });
        }
        #endregion

        #region Helpers
        public static MvcHtmlString Nl2Br(string text)
        {
            if (string.IsNullOrEmpty(text))
                return MvcHtmlString.Create(text);
            var builder = new StringBuilder();
            var lines = text.Split('\n');
            for (var i = 0; i < lines.Length; i++)
            {
                if (i > 0)
                    builder.Append("<br/>");
                builder.Append(HttpUtility.HtmlEncode(lines[i]));
            }
            return MvcHtmlString.Create(builder.ToString());
        }

        public static string SafeSubstring(string input, int length)
        {
            if (length > input.Length)
            {
                return input;
            }

            var endPosition = input.IndexOf(" ", length, StringComparison.Ordinal);
            if (endPosition < 0) endPosition = input.Length;

            return length >= input.Length ? input : input.Substring(0, endPosition) + "...";
        }
        #endregion
    }
}