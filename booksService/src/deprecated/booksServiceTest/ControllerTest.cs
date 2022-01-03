using BooksService.Configuration;
using BooksService.Controllers;
using BooksService.Data;
using BooksService.Models;
using BooksService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Xunit;

namespace booksServiceTest
{
    public class ControllerTest
    {
        public readonly DbContextOptions<DBContext> dbContextOptions;
        public readonly ILogger _logger;

        public ControllerTest() 
        {
            dbContextOptions = new DbContextOptionsBuilder<DBContext>()
             .UseInMemoryDatabase(databaseName: "BooksDbTest")
             .Options;
        }

        [Fact]
        public async Task WhenBookIsSavedThenItShouldInsertNewEntry()
        {
            var dbContext = new DBContext(dbContextOptions);
            BookRepository repository = new BookRepository(dbContext, _logger);
            var newBook = new Book() { Id = Guid.NewGuid(), Title = "Titolo Test", Author = "Autore Test", Category = "Categoria di Test" };

            //Act
            await repository.Add(newBook);

            //Assert
            Assert.Equal(1, await dbContext.Books.CountAsync());
        }


    }
}
