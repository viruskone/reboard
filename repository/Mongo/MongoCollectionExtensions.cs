using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Reboard.Repository.Mongo
{
    internal static class MongoCollectionExtensions
    {
        internal static async Task<IAsyncCursor<T>> FindAsyncById<T>(this IMongoCollection<T> collection, string id) where T : IBsonIdDto
            => await collection.FindAsync(GetFilterById<T>(id));

        private static FilterDefinition<T> GetFilterById<T>(string id) where T : IBsonIdDto
            => new FilterDefinitionBuilder<T>().Eq(dto => dto.Id, new ObjectId(id));
    }
}