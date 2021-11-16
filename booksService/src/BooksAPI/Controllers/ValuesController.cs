using BooksAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController
    {
        private readonly AppDbContext _db;

        public BooksController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await _db.Books.ToListAsync();
            return new JsonResult(books);
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
            return new JsonResult(book);
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<IActionResult> Post(Book book)
        {
            _db.Books.Add(book);
            await _db.SaveChangesAsync();

            return new JsonResult(book.Id);
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Book book)
        {
            var existingBook = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
            existingBook.Title = book.Title;
            existingBook.Category = book.Category;
            existingBook.Author = book.Author;
            var success = (await _db.SaveChangesAsync()) > 0;

            return new JsonResult(success);
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
            _db.Remove(book);
            var success = (await _db.SaveChangesAsync()) > 0;

            return new JsonResult(success);
        }
    }
}
