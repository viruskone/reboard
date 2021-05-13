using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Reboard.Infrastructure.MongoDB.Users
{
    public class UserDto
    {
        public Guid CompanyId { get; set; }
        public string EncryptedPassword { get; set; }

        [BsonId]
        public Guid Id { get; set; }

        public string Login { get; set; }
    }
}