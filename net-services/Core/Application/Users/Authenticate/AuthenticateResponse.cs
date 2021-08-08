namespace Reboard.Core.Application.Users.Authenticate
{
    public class AuthenticateResponse
    {
        public AuthenticateStatus Status { get; set; }
        public string Token { get; set; }
    }
}