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
        public async Task GetCustomersTest()
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

        [Theory]
        [InlineData("602d2149e773f2a3990b47f0")] //Id doesn't exist
        public async Task GetCustomerByFakeId(string id)
        {
            //Arrange
            var validId = id;


            //Act        
            var okResult = await _controller.GetCustomerById(id);

            //Assert           
            Assert.IsType<OkObjectResult>(okResult.Result);

            //Check value of result for the ok object result
            var item = okResult.Result as OkObjectResult;

            //Expect to return a single customer
            Assert.IsType<Customer>(item.Value);

            //Check the value
            var customerItem = item.Value as Customer;
            Assert.Equal(validId, customerItem.Id);
            Assert.Equal("Carlo", customerItem.Name);
        }

        [Theory]
        [InlineData("602d2149e773f2a3990b47f6")] //Id exists
        public async Task GetCustomerById(string id)
        {
            //Arrange
            var validId = id;

            //Act            
            var okResult = await _controller.GetCustomerById(id);

            //Assert            
            Assert.IsType<OkObjectResult>(okResult.Result);

            //Check value of result for the ok object result
            var item = okResult.Result as OkObjectResult;

            //Expect to return a single customer
            Assert.IsType<Customer>(item.Value);

            //Check the value
            var customerItem = item.Value as Customer;
            Assert.Equal(validId, customerItem.Id);
            Assert.Equal("Carlo", customerItem.Name);
        }
    }
}