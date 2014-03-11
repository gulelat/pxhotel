namespace PX.Business.Models.UserModels
{
    public class UserSearchModel
    {
        #region Public Properties

        public int? RoleId { get; set; }

        public int? StatusId { get; set; }

        public string Email { get; set; }

        public string IdentityNumber { get; set; }

        public string DateOfBirth { get; set; }

        #endregion
    }
}
