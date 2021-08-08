using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Reboard.Infrastructure.MongoDB.Users
{
    public class CompanyDto
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}