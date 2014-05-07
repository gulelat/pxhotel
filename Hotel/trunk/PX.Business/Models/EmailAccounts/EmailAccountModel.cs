namespace PX.Business.Models.EmailAccounts
{
    public class EmailAccountModel : BaseModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }

        public bool IsDefault { get; set; }

        public string IsDefaultString
        {
            get
            {
                return IsDefault ? "Yes" : "No";
            }
        }
    }
}
