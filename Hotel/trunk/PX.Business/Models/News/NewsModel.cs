using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Enums;
using PX.Core.Ultilities;

namespace PX.Business.Models.News
{
    public class NewsModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public bool IsHotNews { get; set; }

        public string IsHotNewsString
        {
            get { return IsHotNews ? "Yes" : "No"; }
            set { IsHotNews = value.Equals("Yes"); }
        }

        public int Status { get; set; }

        public string StatusName
        {
            get
            {
                return EnumUtilities.GetName((NewsEnums.StatusEnums)Status);
            }
            set
            {
                Status = value.ToInt();
            }
        }

        public string Categories { get; set; }
    }
}
