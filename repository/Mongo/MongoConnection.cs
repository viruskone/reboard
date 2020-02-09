namespace Reboard.Repository.Mongo
{
    public class MongoConnection
    {
        public string Connection { get; }
        public string Database { get; }

        public MongoConnection(string connection, string database)
        {
            Connection = connection;
            Database = database;
        }
    }
}