using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Reboard.Infrastructure.MongoDB.Reports
{
    [BsonIgnoreExtraElements]
    public class ReportDto
    {
        public Guid[] AllowedCompanies { get; set; }
        public Guid[] AllowedUsers { get; set; }
        public TimeSpan AverageDuration { get; set; }
        public ColorDto Color { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreateTime { get; set; }

        public string Description { get; set; }
        public int Downloads { get; set; }

        [BsonId]
        public Guid Id { get; set; }

        public string Shortcut { get; set; }
        public string Title { get; set; }
    }
}