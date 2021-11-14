using libriManager.Entities;
using libriManager.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace libriManager.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _repository;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookRepository repository, ILogger<BookController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _repository.GetBooks();
            return Ok(books);
        }

        [HttpGet("{id:length(24)}", Name = "GetBooks")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Book>> GetBookById(string id)
        {
            var book = await _repository.GetBooks(id);

            if (book == null)
            {
                _logger.LogError($"Book with id: {id}, not found.");
                return NotFound();
            }

            return Ok(book);
        }

        [Route("[action]/{category}", Name = "GetBookByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookByCategory(string category)
        {
            var books = await _repository.GetBookByCategory(category);
            return Ok(books);
        }

        [Route("[action]/{author}", Name = "GetBookByAuthor")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookByAuthor(string author)
        {
            var books = await _repository.GetBookByAuthor(author);
            return Ok(books);
        }

        [Route("[action]/{name}", Name = "GetBookByName")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Book>>> GetProductByName(string name)
        {
            var items = await _repository.GetBookByName(name);
            if (items == null)
            {
                _logger.LogError($"Book with name: {name} not found.");
                return NotFound();
            }
            return Ok(items);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Book>> CreateBook([FromBody] Book book)
        {
            await _repository.CreateBook(book);

            return CreatedAtRoute("GetBook", new { id = book.Id }, book);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBook([FromBody] Book book)
        {
            return Ok(await _repository.UpdateBook(book));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteBook")]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBookById(string id)
        {
            return Ok(await _repository.DeleteBook(id));
        }
    }
}
