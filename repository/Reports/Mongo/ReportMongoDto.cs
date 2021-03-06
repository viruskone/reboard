using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Reboard.Repository.Reports.Mongo
{
    [BsonIgnoreExtraElements]
    public class ReportMongoDto
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Shortcut { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreateTime { get; set; }

        public int Downloads { get; set; }
        public TimeSpan AverageDuration { get; set; }
        public ColorMongoDto Color { get; set; }
        public string[] AllowedCompanies { get; set; }
        public string[] AllowedUsers { get; set; }
    }

}