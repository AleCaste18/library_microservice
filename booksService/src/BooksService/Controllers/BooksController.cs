using booksService.Business;
using booksService.Business.Interfaces;
using booksService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace booksService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksBusiness _booksBusiness;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBooksBusiness booksBusiness, ILogger<BooksController> logger)
        {
            _booksBusiness = booksBusiness;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _booksBusiness.GetAllAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var books = await _booksBusiness.GetByIdAsync(id);
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Book book)
        {
            
            if (book == null) 
            {
                return BadRequest();
            }

            await _booksBusiness.CreateAsync(book);
            return Ok();     

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            if (id != book.Id)
                return BadRequest();

            await _booksBusiness.UpdateAsync(book);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _booksBusiness.GetByIdAsync(id);
            if (item == null)
                return BadRequest();

            await _booksBusiness.DeleteAsync(id);
            return Ok(item);
        }
    }

    
}
