using System.Web.Mvc;
using PX.Library.Configuration;

namespace PX.Business.Helpers
{
    public static class CommonHelpers
    {
        public static string DescriptionCutOff(this HtmlHelper helper, string description)
        {
            var length = Configurations.DescriptionLength;
            if (description.Length > length)
            {
                return string.Format("{0}...", description.Substring(0, length));
            }
            return string.Format("{0}...", description);
        }
    }
}
