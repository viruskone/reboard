using System;
using MongoDB.Driver;

namespace Reboard.Repository
{
    public class MongoConnection
    {
        public string Value { get; private set; }
        public MongoConnection(string value)
        {
            Value = value;
        }
    }
}
