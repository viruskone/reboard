using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reboard.Repository.Reports.Mongo
{
    public class ReportMongoDto
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreateTime { get; set; }
        public int Downloads { get; set; }
        public TimeSpan AverageDuration { get; set; }
        public double Rating { get; set; }
    }

}
