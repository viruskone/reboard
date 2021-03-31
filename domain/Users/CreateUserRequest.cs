namespace Reboard.Domain.Users
{
    public class CreateUserRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
    }
}