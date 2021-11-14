using booksManager.Entity;
using booksManager.MongoDB;
using booksManager.Setting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace CommandCenter.MongoDB
{
    public static class Extensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            services.AddSingleton(a =>
            {
                var configuration = a.GetService<IConfiguration>();
                var serviceSettings = configuration.GetSection(nameof(ServiceSetting)).Get<ServiceSetting>();
                var mongoDbSettings = configuration.GetSection(nameof(MongoDbSetting)).Get<MongoDbSetting>();
                var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
                return mongoClient.GetDatabase(serviceSettings.ServiceName);
            });

            return services;
        }

        public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName) where T : IEntity
        {
            services.AddSingleton<IRepository<T>>(a =>
            {
                var database = a.GetService<IMongoDatabase>();
                return new MongoRepository<T>(database, collectionName);
            });
            return services;
        }
    }
}