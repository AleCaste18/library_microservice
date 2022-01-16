using BorrowingService.Controllers;
using BorrowingService.Entities;
using BorrowingService.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BorrowingServiceTest
{
    public class BorrowingsControllerTest
    {
        private readonly BorrowingsController _controller;
        private readonly IBorrowingRepository _repository;
        private readonly ILogger<BorrowingsController> _logger;

        public BorrowingsControllerTest()
        {
            _repository = new BorrowingRepositoryFake();
            _controller = new BorrowingsController(_repository, _logger);
        }

        [Fact]
        public async Task GetBorrowings()
        {
            //Arrange

            //Act
            var result = await _controller.GetBorrowing();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var list = result.Result as OkObjectResult;
            Assert.IsType<List<Borrowing>>(list.Value);
            var listBorrowing = list.Value as List<Borrowing>;
            Assert.Equal(3, listBorrowing.Count);
        }

        [Fact]
        public async Task GetBorrowingByID()
        {
            //Arrange
            var idBorrowing = "602d2149e773f2a3990b47f6";

            //Act
            var okResult = await _controller.GetBorrowingById(idBorrowing);

            //Assert
            Assert.IsType<OkObjectResult>(okResult.Result);

        }

        [Fact]
        public async Task CreateBorrowing()
        {
            //Arrange
            var borrowing = new Borrowing();
            borrowing.Id = "602d2149e773f2a3990b47fh";
            borrowing.IdBook = "1";
            borrowing.IdCustomer = "1";

            //Act
            var createdResult = await _controller.CreateBorrowing(borrowing);

            //Assert
            Assert.IsAssignableFrom<CreatedAtRouteResult>(createdResult.Result);

        }
    }
}