namespace PX.Business.Models.Users.Logins
{
    public class LoginModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string RememberMeString { set { RememberMe = value.Equals("on"); } }

        public bool RememberMe { get; set; }
    }
}
