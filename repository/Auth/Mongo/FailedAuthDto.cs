using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Reboard.Repository.Mongo;
using System;

namespace Reboard.Repository.Auth.Mongo
{
    public class FailedAuthDto : IBsonIdDto
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string User { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreateTime { get; set; }
    }
}