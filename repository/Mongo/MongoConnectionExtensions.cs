using MongoDB.Driver;

namespace Reboard.Repository.Mongo
{
    public static class MongoConnectionExtensions
    {
        public static IMongoCollection<T> GetCollection<T>(this MongoConnection connection, string collectionName)
        {
            var client = new MongoClient(connection.Connection);
            var database = client.GetDatabase(connection.Database);
            return database.GetCollection<T>(collectionName);
        }
    }
}