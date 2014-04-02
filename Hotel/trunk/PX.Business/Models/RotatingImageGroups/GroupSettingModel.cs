using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.RotatingImageGroups
{
    public class GroupSettingModel
    {
        public GroupSettingModel()
        {
            AutoPlay = 3000;
            StopOnHover = true;
            Navigation = true;
            PaginationSpeed = 1000;
            PaginationNumbers = true;
            LazyLoad = true;
            GoToFirstSpeed = 2000;
            SingleItem = true;
            AutoHeight = true;
            TransitionStyle = "fade";
        }

        #region Public Properties

        [Required]
        public int AutoPlay { get; set; }

        [Required]
        public bool StopOnHover { get; set; }

        [Required]
        public bool Navigation { get; set; }

        [Required]
        public int PaginationSpeed { get; set; }

        [Required]
        public bool PaginationNumbers { get; set; }

        [Required]
        public bool LazyLoad { get; set; }

        [Required]
        public int GoToFirstSpeed { get; set; }

        [Required]
        public bool SingleItem { get; set; }

        [Required]
        public bool AutoHeight { get; set; }

        [Required]
        public string TransitionStyle { get; set; }

        #endregion
    }
}
