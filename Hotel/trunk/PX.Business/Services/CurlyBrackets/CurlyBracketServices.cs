using System;
using System.Collections.Generic;
using System.Linq;
using PX.Business.Models.CurlyBrackets;
using PX.Business.Mvc.Attributes;
using PX.Business.Services.CurlyBrackets.CurlyBracketResolver;
using PX.EntityModel.Repositories.RepositoryBase.Extensions;

namespace PX.Business.Services.CurlyBrackets
{
    public class CurlyBracketServices : ICurlyBracketServices
    {
        public List<CurlyBracketModel> GetAll()
        {
            var type = typeof(ICurlyBracketResolver);
            var types = EntityExtensions.GetAllClassImplementInteface(type);

            return types.Select(t => new CurlyBracketModel
                {
                    Name = EntityExtensions.GetAttribute<CurlyBracketAttribute>(t).Name,
                    CurlyBracket = EntityExtensions.GetAttribute<CurlyBracketAttribute>(t).CurlyBracket
                }).ToList();
        }

        public string Render(string content)
        {
            return content;
        }
    }
}
