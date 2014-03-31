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

        public string ImageFileName { get; set; }

        public int Status { get; set; }

        public string StatusName
        {
            get
            {
                return EnumUtilities.GetName((NewsEnums.NewsStatusEnums)Status);
            }
            set
            {
                Status = value.ToInt();
            }
        }

        public string Categories { get; set; }
    }
}
