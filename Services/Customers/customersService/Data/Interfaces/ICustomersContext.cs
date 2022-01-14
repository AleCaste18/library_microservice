using customersService.Entities;
using MongoDB.Driver;

namespace customersService.Data.Interfaces
{
    public interface ICustomersContext
    {
        IMongoCollection<Customer> Customers { get; }

    }
}
