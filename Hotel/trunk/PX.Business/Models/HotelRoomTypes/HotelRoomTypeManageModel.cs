using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PX.Business.Services.HotelRoomTypes;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;

namespace PX.Business.Models.HotelRoomTypes
{
    public class HotelRoomTypeManageModel : BaseModel, IValidatableObject
    {
        #region Public Properties
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public List<int> HotelRoomTypeServiceIds { get; set; }

        public IEnumerable<SelectListItem> HotelRoomTypeServices { get; set; }

        /// <summary>
        /// Validate the model
        /// </summary>
        /// <param name="context">the validation context</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            var hotelRoomTypeServices = HostContainer.GetInstance<IHotelRoomTypeServices>();
            var localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            if (hotelRoomTypeServices.IsRoomTypeNameExisted(Id, Name))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::HotelRoomTypes:::ValidationMessages:::ExistingTitle:::HotelRoomType title is existed."), new[]{ "Title"});
            }
        }
        #endregion
    }
}
