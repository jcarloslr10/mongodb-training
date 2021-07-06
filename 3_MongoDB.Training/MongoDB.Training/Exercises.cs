using MongoDB.Driver;
using MongoDB.Training.Fixtures;
using MongoDB.Training.Infrastructure;
using MongoDB.Training.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Document = MongoDB.Training.Infrastructure.Models.Document;
using Tag = MongoDB.Training.Infrastructure.Models.Tag;

namespace MongoDB.Training
{
    [Collection(FixturesConstants.DatabaseCollectionFixtureName)]
    public class Exercises
    {
        private readonly IMongoDataStore _mongoDataStore;

        public Exercises(DatabaseFixture fixture)
        {
            _mongoDataStore = fixture.MongoDataStore;
        }

        // Get the tag with _id equal to "000000000000000000000001".
        [Fact]
        public async Task Example_1()
        {
            var filter = Builders<Tag>.Filter.Eq(t => t.Id, "000000000000000000000001");
            var tag = (await _mongoDataStore.Tags.FindAsync(filter)).FirstOrDefault();

            Assert.Equal("Operations", tag.Name);
            Assert.Equal(new DateTime(2021, 8, 8, 12, 0, 0, DateTimeKind.Utc), tag.CreationDate);
        }

        // Get the documents where priority field is equal to 1.
        [Fact]
        public async Task Example_2()
        {
            var filter = Builders<Document>.Filter.Eq(d => d.Priority, 1);
            var documents = await (await _mongoDataStore.Documents.FindAsync(filter)).ToListAsync();

            Assert.Equal(2, documents.Count);
            Assert.Equal("Contract 1", documents[0].Name);
            Assert.Equal(1, documents[0].Priority);
            Assert.Equal(new DateTime(2021, 3, 1, 8, 0, 0, DateTimeKind.Utc), documents[0].LastUpdatedDate);
            Assert.False(documents[0].IsDeleted);
            Assert.Equal(2, documents[0].Tags.Count);
            Assert.Equal("Contract 2", documents[1].Name);
            Assert.Equal(1, documents[1].Priority);
            Assert.Equal(new DateTime(2021, 5, 1, 8, 0, 0, DateTimeKind.Utc), documents[1].LastUpdatedDate);
            Assert.False(documents[1].IsDeleted);
            Assert.Equal(2, documents[1].Tags.Count);
        }

        // Insert a document without specifying _id and query it by name and priority fields
        [Fact]
        public async Task Exercise_1_Create_Read()
        {
            // TYPE THE INSERT COMMAND...

            // TYPE THE QUERY TO GET DOCUMENT...
            //var insertedDocument = ...

            // UNCOMMENT ASSERTS WHEN DONE
            //Assert.Equal("Contract 3", insertedDocument.Name);
            //Assert.Equal(2, insertedDocument.Priority);
            //Assert.False(insertedDocument.IsDeleted);
        }

        // Update lastUpdatedDate field with current date and time UTC for those documents where the
        // priority field is greater than 7 and check it by querying.
        [Fact]
        public async Task Exercise_2_Update_Read()
        {
            // TYPE THE UPDATE COMMAND...

            // TYPE THE QUERY TO GET UPDATED DOCUMENTS...
            //var updatedDocuments = ...

            // UNCOMMENT ASSERTS WHEN DONE
            //Assert.Single(updatedDocuments);
            //Assert.Equal(DateTime.UtcNow.Date.ToString("s"), updatedDocuments.First().LastUpdatedDate.Date.ToString("s"));
        }

        // Delete documents where priority field is greater than 2 and check it by querying.
        // Then calculate the number of elements where priority field is less than or equal to 2.
        [Fact]
        public async Task Exercise_3_Delete_Read()
        {
            // TYPE THE DELETE COMMAND...

            // TYPE THE QUERY TO GET DELETED DOCUMENTS...
            //var deletedDocuments = ...

            // TYPE THE QUERY TO GET THE NUMBER OF DOCUMENTS...
            //var countDocuments = ...

            // UNCOMMENT ASSERTS WHEN DONE
            //Assert.Empty(deletedDocuments);
            //Assert.True(countDocuments > 1);
        }

        // Get the top 2 documents where tags field contains at least the tag identifier "000000000000000000000002"
        // sorted by lastUpdatedDate field in descending order.
        // Join the result with the tag collection to have access to the name field.
        // Overwrite the tags field with the names of the tags obtained after joining.
        // Exclude the tags field from the final result.
        // Finally, answer the next question: 
        // $sort & $limit after $match or after $project? Is it matter?
        [Fact]
        public async Task Exercise_4_Aggregation()
        {
            // TYPE THE QUERY TO GET DOCUMENTS...
            //var documents = ...

            // UNCOMMENT ASSERTS WHEN DONE
            //Assert.Equal(2, documents.Count);
            //Assert.Equal("Tax", documents[0].Name);
            //Assert.Equal(10, documents[0].Priority);
            //Assert.True(documents[0].IsDeleted);
            //Assert.Single(documents[0].TagsEntities);
            //Assert.Equal("License", documents[1].Name);
            //Assert.Equal(5, documents[1].Priority);
            //Assert.True(documents[1].IsDeleted);
            //Assert.Equal(2, documents[1].TagsEntities.Count);
        }
    }
}
