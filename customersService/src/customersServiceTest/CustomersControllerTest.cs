using customersService.Controllers;
using customersService.Entities;
using customersService.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace customersServiceTest
{
    public class CustomersControllerTest
    {
        private readonly CustomersController _controller;
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomersController> _logger;

        public CustomersControllerTest()
        {
            _repository = new CustomerRepositoryFake();
            _controller = new CustomersController(_repository, _logger);
        }

        [Fact]
        public async Task GetCustomers()
        {
            //Arrange

            //Act
            var result = await _controller.GetCustomers();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var list = result.Result as OkObjectResult;
            Assert.IsType<List<Customer>>(list.Value);
            var listCustomers = list.Value as List<Customer>;
            Assert.Equal(3, listCustomers.Count);
        }

        [Fact]
        public async Task GetCustomerByID() 
        {
            //Arrange
            var idCustomer = "602d2149e773f2a3990b47f6";

            //Act
            var okResult = await _controller.GetCustomerById(idCustomer);

            //Assert
            Assert.IsType<OkObjectResult>(okResult.Result);

        }

        [Fact]
        public async Task CreateCustomer() 
        {
            //Arrange
            var customer = new Customer();
            customer.Id = "602d2149e773f2a3990b47fh";
            customer.Name = "Nome";
            customer.Surname = "Cognome";
            customer.Address = "Indirizzo";
            customer.Card = 25252525;

            //Act
            var createdResult = await _controller.CreateCustomer(customer);

            //Assert
            Assert.IsAssignableFrom<CreatedAtRouteResult>(createdResult.Result);

        }
    }
}