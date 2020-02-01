using MongoDB.Bson;

namespace Reboard.Repository.Mongo
{
    public static class UniqueId
    {
        public static string Generate() => ObjectId.GenerateNewId().ToString();
    }

}