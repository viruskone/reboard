using Reboard.Core.Domain.Users;

namespace Reboard.Infrastructure.MongoDB.Users
{
    public static class Mapper
    {
        public static User FromDto(this UserDto dto)
            => dto != null ?
                User.Make(dto.Login, Password.MakeFromEncrypted(dto.EncryptedPassword)) :
                null;

        public static UserDto ToDto(this User user)
                => new UserDto
                {
                    Login = user.Login,
                    EncryptedPassword = user.Password.EncryptedValue
                };
    }
}