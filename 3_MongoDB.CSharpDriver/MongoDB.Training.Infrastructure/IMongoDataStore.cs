using MongoDB.Driver;
using MongoDB.Training.Infrastructure.Models;
using Tag = MongoDB.Training.Infrastructure.Models.Tag;

namespace MongoDB.Training.Infrastructure
{
    public interface IMongoDataStore
    {
        IMongoCollection<Document> Documents { get; }
        IMongoCollection<Tag> Tags { get; }
    }
}
