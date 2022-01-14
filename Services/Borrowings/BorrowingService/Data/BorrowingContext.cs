using BorrowingService.Entities;
using MongoDB.Driver;

namespace BorrowingService.Data
{
    public class BorrowingContext : IBorrowingContext
    {
        public BorrowingContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Borrowing = database.GetCollection<Borrowing>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        }
        public IMongoCollection<Borrowing> Borrowing { get; }
    }
}

