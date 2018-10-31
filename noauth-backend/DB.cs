using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
namespace Company.Function
{
    public class DB
    {
        IMongoClient client;
        IMongoDatabase database;
        public DB() {
            Connect();
        }
        public void Connect()
        {
            var connectionString = System.Environment.GetEnvironmentVariable("mongoURL", EnvironmentVariableTarget.Process);
            client = new MongoClient(connectionString);
            database = client.GetDatabase("interact");
        }
        public List<BsonDocument> GetData(string collectionName) {
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
            return collection.Find(new BsonDocument()).ToList();
        }
        public List<BsonDocument> GetData(String collectionName, FilterDefinition<BsonDocument> filter)
        {
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
            return collection.Find(filter).ToList();
        }
    }
}