using MongoDB.Bson.Serialization.Attributes;

namespace Reboard.Repository.Users.Mongo
{
    public class UserMongoDto
    {
        [BsonId]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}