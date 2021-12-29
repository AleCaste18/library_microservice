using customersService.Entities;
using customersService.Repository.Interfaces;

namespace customersService.GraphQL
{
    public class Query
    {
        public Task<IEnumerable<Customer>> GetCustomers([Service] ICustomerRepository customerRepository) =>
            customerRepository.GetCustomers();

        public Task<Customer> GetCustomer(string customerId, [Service] ICustomerRepository customerRepository) =>
            customerRepository.GetCustomer(customerId);
    }
}
