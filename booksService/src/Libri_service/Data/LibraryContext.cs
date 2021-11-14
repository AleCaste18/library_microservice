using Libri_service.Entities;
using Libri_service.Data.Interfaces;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace Libri_service.Data
{
    public class LibraryContext : ILibraryContext
    {
        public LibraryContext(IConfiguration configuration) 
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));


            Books = database.GetCollection<Book>(configuration.GetValue<String>("DatabaseSettings:CollectionName"));
            Boo

        }
    }
}
