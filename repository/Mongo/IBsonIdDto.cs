using MongoDB.Bson;

namespace Reboard.Repository.Mongo
{
    internal interface IBsonIdDto
    {
        ObjectId Id { get; set; }
    }
}