using MongoDB.Bson.Serialization.Attributes;

namespace Reboard.Repository.Users.Mongo
{
    public class UserMongoDto
    {
        [BsonId]
        public string Login { get; set; }

        public string Password { get; set; }
        public string Company { get; set; }
    }
}