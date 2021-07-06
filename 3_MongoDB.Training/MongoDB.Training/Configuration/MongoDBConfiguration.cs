using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Training.Infrastructure.Models;
using System.Collections.Generic;
using Tag = MongoDB.Training.Infrastructure.Models.Tag;

namespace A3InnuvaDoc.Api.Infrastructure.Configuration.Mongo
{
    public static class MongoDBConfiguration
    {
        public static void Configure()
        {
            var pack = new ConventionPack();
            pack.Add(new CamelCaseElementNameConvention());

            ConventionRegistry.Register(
               "CamelCaseElementNameConvention",
               pack,
               t => true);

            BsonClassMap.RegisterClassMap<Document>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);

                cm.MapIdMember(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));

                cm.GetMemberMap(c => c.Tags).SetSerializer(new EnumerableInterfaceImplementerSerializer<List<string>>(new StringSerializer(BsonType.ObjectId)));
            });

            BsonClassMap.RegisterClassMap<Tag>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);

                cm.MapIdMember(c => c.Id);
                cm.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
        }
    }
}
