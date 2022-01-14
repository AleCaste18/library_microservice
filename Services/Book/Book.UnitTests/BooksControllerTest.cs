using Book.API.Business;
using Book.API.Controllers;
using Book.API.Data;
using Book.API.Models;
using Book.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Book.UnitTests
{
    public class BooksServiceTests
    {
        readonly BookContext objDataContext;
        readonly BooksRepository objBookRepository;
        readonly BooksBusiness objBookBusiness;
        readonly BooksController objBookController;

        readonly ILogger<BooksBusiness> loggerBusiness;
        readonly ILogger<BooksController> loggerController;

        public BooksServiceTests()
        {
            DbContextOptions<BookContext> options;

            var builder = new DbContextOptionsBuilder<BookContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")                                    //FakeDB
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            options = builder.Options;
            objDataContext = new BookContext(options);
            objBookRepository = new BooksRepository(objDataContext);
            objBookBusiness = new BooksBusiness(objBookRepository, loggerBusiness);

            objBookController = new BooksController(objBookBusiness, loggerController);
        }

        private async Task FeedDbInMemory()
        {

            Books objBook = null;
            for (int i = 1; i < 18; i++)
            {
                objBook = new Books(); ;
                objBook.Title = $"Title_{i}";
                objBook.Author = $"Author_{i}";
                objBook.Category = $"Category_{i}";
                await objBookBusiness.CreateAsync(objBook);
            }
        }


        [Fact]
        public async Task GetBooks_WithBooksInRepo_ReturnsOk()
        {
            //arrange
            Task.Run(() => FeedDbInMemory()).Wait();
            var controller = objBookController;

            //act
            var result = await controller.GetAll();
            var okResult = result as OkObjectResult;

            //assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }


        [Fact]
        public async Task GetBookById_WithBookIdValid_ShouldReturnOk()
        {
            // Arrange
            Task.Run(() => FeedDbInMemory()).Wait();
            var controller = objBookController;
            var idBook = 1;

            // Act
            var controllerResult = await controller.GetById(idBook);
            var objBook = ((OkObjectResult)controllerResult).Value as Books;

            // Assert
            Assert.IsType<OkObjectResult>(controllerResult);
            Assert.Equal($"Title_{idBook}", objBook.Title);

        }

        [Fact]
        public async Task GetUserById_WithUserIdNotValid_ShouldReturnNotFound()
        //System.Exception : Couldn't retrieve entity with id=100: Couldn't find entity with id=100
        //Exception lanciata per via della gestione di getbyid nella classe repository
        {
            // Arrange
            Task.Run(() => FeedDbInMemory()).Wait();
            var controller = objBookController;

            // Act           
            var controllerResult = await controller.GetById(100);

            // Assert
            Assert.IsAssignableFrom<NotFoundObjectResult>(controllerResult);
        }

        [Fact]
        public async Task DeleteBookById_WithBookIdValid_ShouldReturnOk()
        {
            // Arrange
            Task.Run(() => FeedDbInMemory()).Wait();
            var controller = objBookController;
            var idBook = 2;

            // Act
            var controllerResult = await controller.Delete(idBook);

            // Assert
            Assert.IsAssignableFrom<OkObjectResult>(controllerResult);
        }

        [Fact]
        public async Task AddBook_ShouldReturnOk()
        {
            // Arrange
            Task.Run(() => FeedDbInMemory()).Wait();
            var controller = objBookController;
            Books objBook = new Books();
            objBook.Title = "Titolo_Test";
            objBook.Author = "Titolo_Test";

            // Act
            var controllerResult = await controller.Create(objBook);

            // Assert
            Assert.IsAssignableFrom<OkResult>(controllerResult);
        }

        [Fact]
        public async Task EditBook_WithBookValid_ShouldReturnOk()
        {
            // Arrange
            Task.Run(() => FeedDbInMemory()).Wait();
            var controller = objBookController;
            Books objBook = new Books();
            objBook.Title = "Titolo_Modificato";
            objBook.Author = "Titolo_Modificato";
            // Act
            var controllerResult = await controller.UpdateBook(objBook.Id, objBook);

            // Assert
            Assert.IsAssignableFrom<OkResult>(controllerResult);
        }

    }
}