using Reboard.Repository.Mongo;

namespace Reboard.WebServer.Architecture
{
    public class MongoUniqueIdFactory : IUniqueIdFactory
    {
        public string Next() => UniqueId.Generate();
    }
}
