using Reboard.Domain.Users;

namespace Reboard.Repository.Users.Mongo
{
    public static class MongoUserExtensions
    {
        public static UserMongoDto ToDto(this User user)
        => new UserMongoDto
        {
            Email = user.Email,
            Password = user.Password
        };

        public static User FromDto(this UserMongoDto dto)
        => new User
        {
            Email = dto.Email,
            Password = dto.Password
        };

    }
}
