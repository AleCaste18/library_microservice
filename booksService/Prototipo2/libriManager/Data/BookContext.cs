using libriManager.Data.Interfaces;
using libriManager.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace libriManager.Data
{
    public class BookContext : IBookContext
    {
        public BookContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Books = database.GetCollection<Book>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            BookContextSeed.SeedData(Books);
        }
        public IMongoCollection<Book> Books { get; }
    }

}

