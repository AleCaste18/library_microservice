using customersService.Entities;
using customersService.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace customersServiceTest
{
    public class CustomerRepositoryFake : ICustomerRepository
    {
        private readonly List<Customer> _customers;

        public CustomerRepositoryFake() 
        {
            _customers = new List<Customer>() 
            {
                new Customer() { Id = "602d2149e773f2a3990b47f6", Name = "Carlo", Surname = "Rossi" , Address = "Via Roma 10, Torino 10100", Card = 19191919 }, 
                new Customer() { Id = "602d2149e773f2a3990b47f7", Name = "Martina", Surname = "Rossi" , Address = "Via Roma 10, Torino 10100", Card = 20202020 },
                new Customer() { Id = "602d2149e773f2a3990b47f8", Name = "Giulia", Surname = "Rossi" , Address = "Via Roma 10, Torino 10100", Card = 21212121 }
            }; 
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return  _customers;
        }
        public async Task CreateCustomer(Customer customer)
        {
            _customers.Add(customer);
        }

        public async Task<Customer> GetCustomer(string id)
        {
            return _customers
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public Task<bool> UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteCustomer(string id)
        {
           var existing = _customers.First(a => a.Id == id);
            _customers.Remove(existing);
            return true;
        }
    }
}
