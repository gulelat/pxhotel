using System.Web.Mvc;

namespace PX.Core.Framework.Mvc.Helpers
{
    public static class CommonHelpers
    {
        public static string DescriptionCutOff(this HtmlHelper helper, string description)
        {
            var length = Configurations.Configurations.DescriptionLength;
            if (description.Length > length)
            {
                return string.Format("{0}...", description.Substring(0, length));
            }
            return string.Format("{0}...", description);
        }
    }
}
