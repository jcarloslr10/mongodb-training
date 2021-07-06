// `8.`8888.      ,8'  ,o888888o.     8 8888      88              .8.          8 888888888o.   8 8888888888   
//  `8.`8888.    ,8'. 8888     `88.   8 8888      88             .888.         8 8888    `88.  8 8888         
//   `8.`8888.  ,8',8 8888       `8b  8 8888      88            :88888.        8 8888     `88  8 8888         
//    `8.`8888.,8' 88 8888        `8b 8 8888      88           . `88888.       8 8888     ,88  8 8888         
//     `8.`88888'  88 8888         88 8 8888      88          .8. `88888.      8 8888.   ,88'  8 888888888888 
//      `8. 8888   88 8888         88 8 8888      88         .8`8. `88888.     8 888888888P'   8 8888         
//       `8 8888   88 8888        ,8P 8 8888      88        .8' `8. `88888.    8 8888`8b       8 8888         
//        8 8888   `8 8888       ,8P  ` 8888     ,8P       .8'   `8. `88888.   8 8888 `8b.     8 8888         
//        8 8888    ` 8888     ,88'     8888   ,d8P       .888888888. `88888.  8 8888   `8b.   8 8888         
//        8 8888       `8888888P'        `Y88888P'       .8'       `8. `88888. 8 8888     `88. 8 888888888888 
// 
//          .8.                 ,o888888o.    8 8888        8 8 8888888888            .8.    8888888 8888888888 8 8888888888   8 888888888o.   
//         .888.               8888     `88.  8 8888        8 8 8888                 .888.         8 8888       8 8888         8 8888    `88.  
//        :88888.           ,8 8888       `8. 8 8888        8 8 8888                :88888.        8 8888       8 8888         8 8888     `88  
//       . `88888.          88 8888           8 8888        8 8 8888               . `88888.       8 8888       8 8888         8 8888     ,88  
//      .8. `88888.         88 8888           8 8888        8 8 888888888888      .8. `88888.      8 8888       8 888888888888 8 8888.   ,88'  
//     .8`8. `88888.        88 8888           8 8888        8 8 8888             .8`8. `88888.     8 8888       8 8888         8 888888888P'   
//    .8' `8. `88888.       88 8888           8 8888888888888 8 8888            .8' `8. `88888.    8 8888       8 8888         8 8888`8b       
//   .8'   `8. `88888.      `8 8888       .8' 8 8888        8 8 8888           .8'   `8. `88888.   8 8888       8 8888         8 8888 `8b.     
//  .888888888. `88888.        8888     ,88'  8 8888        8 8 8888          .888888888. `88888.  8 8888       8 8888         8 8888   `8b.   
// .8'       `8. `88888.        `8888888P'    8 8888        8 8 888888888888 .8'       `8. `88888. 8 8888       8 888888888888 8 8888     `88. 

//      _             _                      _ _     _                                _   
//   __| | ___  _ __ | |_ ___  ___ _ __ ___ | | | __| | _____      ___ __  _   _  ___| |_ 
//  / _` |/ _ \| '_ \| __/ __|/ __| '__/ _ \| | |/ _` |/ _ \ \ /\ / / '_ \| | | |/ _ \ __|
// | (_| | (_) | | | | |_\__ \ (__| | | (_) | | | (_| | (_) \ V  V /| | | | |_| |  __/ |_ 
//  \__,_|\___/|_| |_|\__|___/\___|_|  \___/|_|_|\__,_|\___/ \_/\_/ |_| |_|\__, |\___|\__|
//                                                                         |___/   


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
    public class Solutions
    {
        private readonly IMongoDataStore _mongoDataStore;

        public Solutions(DatabaseFixture fixture)
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
            await _mongoDataStore.Documents.InsertOneAsync(new Document
            {
                Name = "Contract 3",
                Priority = 2,
                LastUpdatedDate = new DateTime(2021, 3, 1, 8, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
                Tags = new List<string>()
            });

            var filter = Builders<Document>.Filter.And(
                Builders<Document>.Filter.Eq(d => d.Name, "Contract 3"),
                Builders<Document>.Filter.Eq(d => d.Priority, 2));

            var insertedDocument = (await _mongoDataStore.Documents.FindAsync(filter)).FirstOrDefault();

            Assert.Equal("Contract 3", insertedDocument.Name);
            Assert.Equal(2, insertedDocument.Priority);
            Assert.False(insertedDocument.IsDeleted);
        }

        // Update lastUpdatedDate field with current date and time UTC for those documents where the
        // priority field is greater than 7 and check it by querying.
        [Fact]
        public async Task Exercise_2_Update_Read()
        {
            var filter = Builders<Document>.Filter.Gt(d => d.Priority, 7);

            // Option 1
            var updateDefinition = Builders<Document>.Update.Set(d => d.LastUpdatedDate, DateTime.UtcNow);
            await _mongoDataStore.Documents.UpdateManyAsync(filter, updateDefinition);

            // Option 2
            //var updateDefinition = Builders<Document>.Update.CurrentDate(nameof(Document.LastUpdatedDate), UpdateDefinitionCurrentDateType.Date);
            //await _mongoDataStore.Documents.UpdateManyAsync(filter, updateDefinition);

            var updatedDocuments = await (await _mongoDataStore.Documents.FindAsync(filter)).ToListAsync();

            Assert.Single(updatedDocuments);
            Assert.Equal(DateTime.UtcNow.Date.ToString("s"), updatedDocuments.First().LastUpdatedDate.Date.ToString("s"));
        }

        // Delete documents where priority field is greater than 2 and check it by querying.
        // Then calculate the number of elements where priority field is less than or equal to 2.
        [Fact]
        public async Task Exercise_3_Delete_Read()
        {
            var deleteFilter = Builders<Document>.Filter.Gt(d => d.Priority, 2);
            await _mongoDataStore.Documents.DeleteManyAsync(deleteFilter);
            var deletedDocuments = await (await _mongoDataStore.Documents.FindAsync(deleteFilter)).ToListAsync();

            Assert.Empty(deletedDocuments);

            var countFilter = Builders<Document>.Filter.Lte(d => d.Priority, 2);
            var countDocuments = await _mongoDataStore.Documents.CountDocumentsAsync(countFilter);

            Assert.True(countDocuments > 1);
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
            var documents = await _mongoDataStore.Documents
                .Aggregate()
                .Match(Builders<Document>.Filter.AnyIn(d => d.Tags, new List<string> { "000000000000000000000002" }))
                .SortByDescending(d => d.LastUpdatedDate)
                .Limit(2)
                .Lookup(
                    _mongoDataStore.Tags,
                    d => d.Tags,
                    t => t.Id,
                    (DocumentExtended e) => e.TagsEntities)
                .Project(Builders<DocumentExtended>.Projection.Exclude(e => e.Tags))
                .As<DocumentExtended>()
                .ToListAsync();

            Assert.Equal(2, documents.Count);

            Assert.Equal("Tax", documents[0].Name);
            Assert.Equal(10, documents[0].Priority);
            Assert.True(documents[0].IsDeleted);
            Assert.Single(documents[0].TagsEntities);

            Assert.Equal("License", documents[1].Name);
            Assert.Equal(5, documents[1].Priority);
            Assert.True(documents[1].IsDeleted);
            Assert.Equal(2, documents[1].TagsEntities.Count);
        }
    }
}
