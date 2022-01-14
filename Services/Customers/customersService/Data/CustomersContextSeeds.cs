using customersService.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace customersService.Data
{
    public class CustomersContextSeed
    {
        public static void SeedData(IMongoCollection<Customer> customerCollection)
        {
            bool existCustomer = customerCollection.Find(p => true).Any();
            if (!existCustomer)
            {
                customerCollection.InsertManyAsync(GetPreconfiguredCustomers());
            }
        }

        private static IEnumerable<Customer> GetPreconfiguredCustomers()
        {
            return new List<Customer>()
            {
                new Customer()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "Mario",
                    Surname = "Rossi",
                    Address = "Via Roma 10, Torino 10100",
                    Card = 18181818               
                }
            };
        }
    }
}