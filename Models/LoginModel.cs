namespace ASPNETCore5Demo.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }

    public class LoginResul
    {
        public string Token { get; set; }
    }
}