using A3InnuvaDoc.Api.Infrastructure.Configuration.Mongo;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Training.Configuration;
using MongoDB.Training.Helpers;
using MongoDB.Training.Infrastructure;
using System;
using System.Collections.Generic;
using Document = MongoDB.Training.Infrastructure.Models.Document;
using Tag = MongoDB.Training.Infrastructure.Models.Tag;

namespace MongoDB.Training.Fixtures
{
    public class DatabaseFixture : IDisposable
    {
        public IMongoDataStore MongoDataStore { get; private set; }

        public DatabaseFixture()
        {
            MongoDBConfiguration.Configure();

            var mongoDBConfig = new MongoDBSettings();
            ConfigurationHelper.GetConfiguration().GetSection("MongoDB").Bind(mongoDBConfig);

            MongoDataStore = new MongoDataStore(mongoDBConfig.ConnectionString, mongoDBConfig.DatabaseName);

            CleanDatabase();
            InitializeDatabase();
        }

        public void Dispose()
        { }

        private void InitializeDatabase()
        {
            MongoDataStore.Tags.InsertMany(new List<Tag> {
                new Tag { Id = "000000000000000000000001", Name = "Operations", CreationDate = DateTimeHelper.NewDateTimeUTC(2021, 8, 8, 12, 0, 0) },
                new Tag { Id = "000000000000000000000002", Name = "Finance", CreationDate = DateTimeHelper.NewDateTimeUTC(2021, 8, 8, 12, 0, 0) },
                new Tag { Id = "000000000000000000000003", Name = "Marketing", CreationDate = DateTimeHelper.NewDateTimeUTC(2021, 8, 8, 12, 0, 0) },
            });

            MongoDataStore.Documents.InsertMany(new List<Document> {
                new Document {
                    Id = "000000000000000000000001",
                    Name = "Contract 1",
                    Priority = 1,
                    LastUpdatedDate = DateTimeHelper.NewDateTimeUTC(2021, 3, 1, 8, 0, 0),
                    IsDeleted = false,
                    Tags = new List<string> {
                        "000000000000000000000001",
                        "000000000000000000000002"
                    }
                },
                new Document {
                    Id = "000000000000000000000002",
                    Name = "Contract 2",
                    Priority = 1,
                    LastUpdatedDate = DateTimeHelper.NewDateTimeUTC(2021, 5, 1, 8, 0, 0),
                    IsDeleted = false,
                    Tags = new List<string> {
                        "000000000000000000000001",
                        "000000000000000000000002"
                    }
                },
                new Document {
                    Id = "000000000000000000000003",
                    Name = "Agreement 1",
                    Priority = 2,
                    LastUpdatedDate = DateTimeHelper.NewDateTimeUTC(2021, 4, 8, 9, 0, 0),
                    IsDeleted = false,
                    Tags = new List<string> {
                        "000000000000000000000001",
                        "000000000000000000000002",
                        "000000000000000000000003"
                    }
                },
                new Document {
                    Id = "000000000000000000000004",
                    Name = "License",
                    Priority = 5,
                    LastUpdatedDate = DateTimeHelper.NewDateTimeUTC(2021, 5, 16, 9, 0, 0),
                    IsDeleted = true,
                    Tags = new List<string> {
                        "000000000000000000000001",
                        "000000000000000000000002"
                    }
                },
                new Document {
                    Id = "000000000000000000000005",
                    Name = "Tax",
                    Priority = 10,
                    LastUpdatedDate = DateTimeHelper.NewDateTimeUTC(2021, 6, 24, 11, 0, 0),
                    IsDeleted = true,
                    Tags = new List<string> {
                        "000000000000000000000002"
                    }
                },
                new Document {
                    Id = "000000000000000000000006",
                    Name = "Business card",
                    Priority = 2,
                    LastUpdatedDate = DateTimeHelper.NewDateTimeUTC(2021, 7, 30, 21, 0, 0),
                    IsDeleted = false,
                    Tags = new List<string> {
                        "000000000000000000000003"
                    }
                },
            });
        }

        private void CleanDatabase()
        {
            MongoDataStore.Documents.DeleteMany(FilterDefinition<Document>.Empty);
            MongoDataStore.Tags.DeleteMany(FilterDefinition<Tag>.Empty);
        }
    }
}
