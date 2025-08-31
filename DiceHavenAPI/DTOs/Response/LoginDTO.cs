namespace DiceHaven_API.DTOs.Response
{
    public class LoginDTO
    {

        public string Login { get; set; }
        public string Password { get; set; }
    }
    public class GoogleLoginDTO
    {
        public string GoogleToken { get; set; }
    }
}
