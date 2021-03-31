using Reboard.Domain.Users;

namespace Reboard.Repository.Users.Mongo
{
    public static class MongoUserExtensions
    {
        public static UserMongoDto ToDto(this User user)
        => new UserMongoDto
        {
            Login = user.Login,
            Password = user.Password,
            Company = user.Company
        };

        public static User FromDto(this UserMongoDto dto)
            => dto != null ? new User
            {
                Login = dto.Login,
                Password = dto.Password,
                Company = dto.Company
            } : null;
    }
}