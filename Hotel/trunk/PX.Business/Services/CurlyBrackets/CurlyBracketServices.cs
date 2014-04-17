using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PX.Business.Models.CurlyBrackets;
using PX.Business.Mvc.Attributes;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Mvc.WorkContext;
using PX.Business.Services.CurlyBrackets.CurlyBracketResolver;
using PX.Business.Services.Settings;
using PX.Core.Configurations.Constants;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;

namespace PX.Business.Services.CurlyBrackets
{
    public class CurlyBracketServices : ICurlyBracketServices
    {
        private readonly ISettingServices _settingServices;
        public CurlyBracketServices()
        {
            _settingServices = HostContainer.GetInstance<ISettingServices>();
        }

        /// <summary>
        /// Get all curly bracket in the system
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CurlyBracketModel> GetAllCurlyBracketsOfApplication()
        {
            if(WorkContext.CurlyBrackets == null)
            {
                var type = typeof(ICurlyBracketResolver);
                var types = ReflectionUtilities.GetAllImplementTypesOfInterface(type);

                WorkContext.CurlyBrackets = types.Select(t => new CurlyBracketModel
                {
                    Name = ReflectionUtilities.GetAttribute<CurlyBracketAttribute>(t).Name,
                    CurlyBracket = ReflectionUtilities.GetAttribute<CurlyBracketAttribute>(t).CurlyBracket,
                    Description = ReflectionUtilities.GetAttribute<CurlyBracketAttribute>(t).Descrition,
                    Type = ReflectionUtilities.GetAttribute<CurlyBracketAttribute>(t).Type.ToString(),
                }).ToList();
            }
            return WorkContext.CurlyBrackets;
        }

        /// <summary>
        /// Search curly brackets
        /// </summary>
        /// <param name="si"></param>
        /// <returns></returns>
        public JqGridSearchOut SearchCurlyBrackets(JqSearchIn si)
        {
            var curlyBrackets = GetAllCurlyBracketsOfApplication().AsQueryable();
            return si.Search(curlyBrackets);
        }

        /// <summary>
        /// Replace all curly bracket in the content
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Render(string content)
        {
            var render = new CurlyBracketRenderer();
            var maxLoop = _settingServices.GetSetting<int>("CurlyBracket.MaxLoop");
            return render.ResolverContent(content, maxLoop);
        }

        /// <summary>
        /// Check if page template content is valid or not
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool IsPageTemplateValid(string content)
        {
            //Not contains render body curly bracket
            if (content.Contains(DefaultConstants.RenderBody))
            {
                return false;
            }

            //Contains more than 1 render body curly bracket
            if (content.LastIndexOf(DefaultConstants.RenderBody, StringComparison.Ordinal) != content.IndexOf(DefaultConstants.RenderBody, StringComparison.Ordinal))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get all fields from object
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetCurlyBracketSelectListFromObject(Type type)
        {
            var properties = type.GetProperties();
            return properties.Select(p => new SelectListItem
                {
                    Text = string.Format("{{{0}}}", p.Name),
                    Value = string.Format("{{{0}}}", p.Name)
                });
        }

        /// <summary>
        /// Get all fields from string
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetCurlyBracketSelectListFromObject(string type)
        {
            var t = Type.GetType(type);
            if(t != null)
            {
                return GetCurlyBracketSelectListFromObject(t);
            }
            return new List<SelectListItem>();
        }
    }
}
