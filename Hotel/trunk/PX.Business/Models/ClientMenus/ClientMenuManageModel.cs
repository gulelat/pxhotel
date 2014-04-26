using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PX.Business.Services.ClientMenus;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Ultilities;
using PX.EntityModel;

namespace PX.Business.Models.ClientMenus
{
    public class ClientMenuManageModel : BaseModel, IValidatableObject
    {
        private readonly IClientMenuServices _clientMenuServices;

        public ClientMenuManageModel()
        {
            _clientMenuServices = HostContainer.GetInstance<IClientMenuServices>();
            IncludeInSiteNavigation = true;
            Parents = _clientMenuServices.GetPossibleParents();

            int position;
            int relativePageId;
            var relativePages = _clientMenuServices.GetRelativeMenus(out position, out relativePageId);
            Positions = EnumUtilities.GetSelectListFromEnum<PageEnums.PositionEnums>();
            Position = position;
            RelativeMenuId = relativePageId;
            RelativeMenus = relativePages;
        }

        public ClientMenuManageModel(ClientMenu menu)
        {
            _clientMenuServices = HostContainer.GetInstance<IClientMenuServices>();
            Id = menu.Id;
            Name = menu.Name;
            Url = menu.Url;
            IncludeInSiteNavigation = menu.IncludeInSiteNavigation;
            StartPublishingDate = menu.StartPublishingDate;
            EndPublishingDate = menu.EndPublishingDate;
            ParentId = menu.ParentId;
            Parents = _clientMenuServices.GetPossibleParents(menu.Id);
            
            int position;
            int relativePageId;
            var relativePages = _clientMenuServices.GetRelativeMenus(out position, out relativePageId, menu.Id, menu.ParentId);
            Position = position;
            Positions = EnumUtilities.GetSelectListFromEnum<PageEnums.PositionEnums>();
            RelativeMenuId = relativePageId;
            RelativeMenus = relativePages;
        }

        #region Public Properties

        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Url { get; set; }

        public bool IncludeInSiteNavigation { get; set; }

        public DateTime? StartPublishingDate { get; set; }

        public DateTime? EndPublishingDate { get; set; }

        public int? ParentId { get; set; }

        public IEnumerable<SelectListItem> Parents { get; set; }

        public int Position { get; set; }

        public IEnumerable<SelectListItem> Positions { get; set; }

        public int? RelativeMenuId { get; set; }

        public IEnumerable<SelectListItem> RelativeMenus { get; set; }

        /// <summary>
        /// Validate the model
        /// </summary>
        /// <param name="context">the validation context</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            var localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            if (_clientMenuServices.IsMenuNameExisted(Id, Name))
            {
                yield return
                    new ValidationResult(
                        localizedResourceServices.T(
                            "AdminModule:::ClientMenus:::ValidationMessages:::ExistingTitle:::Title is existed."),
                        new[] {"Title"});
            }

            #endregion
        }
    }
}
