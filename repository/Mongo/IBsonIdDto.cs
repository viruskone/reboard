using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reboard.Repository.Mongo
{
    internal interface IBsonIdDto
    {
        [BsonId]
        ObjectId Id { get; set; }
    }

}