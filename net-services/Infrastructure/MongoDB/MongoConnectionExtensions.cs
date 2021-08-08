using MongoDB.Driver;
using System.Text;

namespace Reboard.Infrastructure.MongoDB
{
    public static class MongoConnectionExtensions
    {
        public static IMongoCollection<T> GetCollection<T>(this MongoConnection connection)
        {
            var client = new MongoClient(connection.Connection);
            var database = client.GetDatabase(connection.Database);
            return database.GetCollection<T>(GenerateCollectionName<T>());
        }

        private static string GenerateCollectionName<T>()
        {
            var name = typeof(T).Name;
            name = name.Replace("mongo", "", System.StringComparison.InvariantCultureIgnoreCase);
            name = name.Replace("dto", "", System.StringComparison.InvariantCultureIgnoreCase);
            var result = new StringBuilder();
            foreach (var c in name)
            {
                if (char.IsUpper(c))
                {
                    result.Append("_");
                    result.Append(char.ToLower(c));
                    continue;
                }
                result.Append(c);
            }
            return result.ToString().Trim('_');
        }
    }
}