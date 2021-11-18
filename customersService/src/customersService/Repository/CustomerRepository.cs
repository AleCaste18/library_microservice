using customersService.Data.Interfaces;
using customersService.Entities;
using customersService.Repository.Interfaces;
using MongoDB.Driver;

namespace customersService.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ICustomersContext _context;

        public CustomerRepository(ICustomersContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateCustomer(Customer customer)
        {
            await _context.Customers.InsertOneAsync(customer);
        }

        public async Task<bool> DeleteCustomer(string id)
        {
            FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Customers
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<Customer> GetCustomer(string id)
        {
            return await _context
                           .Customers
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _context
                            .Customers
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            var updateResult = await _context
                                        .Customers
                                        .ReplaceOneAsync(filter: g => g.Id == customer.Id, replacement: customer);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}
