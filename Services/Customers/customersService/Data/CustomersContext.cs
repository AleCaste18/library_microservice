using customersService.Data.Interfaces;
using customersService.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace customersService.Data
{
    public class CustomersContext : ICustomersContext
    {
        public CustomersContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Customers = database.GetCollection<Customer>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            //CustomersContextSeed.SeedData(Customers);
        }
        public IMongoCollection<Customer> Customers { get; }
    }
}
