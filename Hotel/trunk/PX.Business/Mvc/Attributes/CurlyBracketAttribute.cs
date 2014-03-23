using System;

namespace PX.Business.Mvc.Attributes
{
    public class CurlyBracketAttribute : Attribute
    {
        public string Name { get; set; }

        public string CurlyBracket { get; set; }
    }
}
