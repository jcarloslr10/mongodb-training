using MongoDB.Driver;
using MongoDB.Training.Infrastructure.Models;
using Tag = MongoDB.Training.Infrastructure.Models.Tag;

namespace MongoDB.Training.Infrastructure
{
    public class MongoDataStore : IMongoDataStore
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MongoDataStore(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _mongoDatabase = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Document> Documents => _mongoDatabase.GetCollection<Document>("documents");
        public IMongoCollection<Tag> Tags => _mongoDatabase.GetCollection<Tag>("tags");
    }
}
