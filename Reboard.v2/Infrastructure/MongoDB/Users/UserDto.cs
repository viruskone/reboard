using MongoDB.Bson.Serialization.Attributes;

namespace Reboard.Infrastructure.MongoDB.Users
{
    public class UserDto
    {
        public string EncryptedPassword { get; set; }

        [BsonId]
        public string Login { get; set; }
    }
}