namespace Reboard.Infrastructure.MongoDB
{
    public class MongoConnection
    {
        public MongoConnection(string connection, string database)
        {
            Connection = connection;
            Database = database;
        }

        public string Connection { get; }
        public string Database { get; }
    }

}