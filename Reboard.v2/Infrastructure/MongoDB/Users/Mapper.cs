using Reboard.Core.Domain.Users;

namespace Reboard.Infrastructure.MongoDB.Users
{
    public static class Mapper
    {
        public static User FromDto(this UserDto dto, Company company)
            => dto != null ?
                User.Make(dto.Id, (Login)dto.Login, Password.MakeFromEncrypted(dto.EncryptedPassword), company) :
                null;

        public static Company FromDto(this CompanyDto dto)
            => dto != null ?
                Company.Make((CompanyId)dto.Id, (CompanyName)dto.Name) :
                null;

        public static CompanyDto ToDto(this Company company)
            => new CompanyDto
            {
                Id = company.Id,
                Name = company.Name
            };

        public static UserDto ToDto(this User user)
                => new UserDto
                {
                    Id = user.Id,
                    Login = user.Login,
                    EncryptedPassword = user.Password.EncryptedValue,
                    CompanyId = user.Company.Id
                };
    }
}