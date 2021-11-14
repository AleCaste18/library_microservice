using booksManager.Entity;
using booksManager.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace booksManager.Controllers
{
    [ApiController]
    [Route("Book")]
    public class BookController : ControllerBase
    {
        private readonly IRepository<Book> repository;
        public BookController(IRepository<Book> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAsync()
        {
            var items = (await repository.GetAllAsync()).Select(a => a.AsDto());
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetByIdAsync(Guid id)
        {
            var item = await repository.GetAsync(id);
            if (item == null)
            {
                NotFound();
            }

            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> PostAsync(CreateBookDto createBookDto)
        {
            var book = new Book
            {
                Name = createBookDto.Name,
                Author = createBookDto.Author,
                Category = createBookDto.Category,
                Description = createBookDto.Description,
                CreatedDate = DateTimeOffset.UtcNow,
            };

            await repository.CreateAsync(book);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateBookDto updateItemDto)
        {
            var existingBook = await repository.GetAsync(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            existingBook.Name = updateItemDto.Name;
            existingBook.Author = updateItemDto.Author;
            existingBook.Category = updateItemDto.Category;
            existingBook.Description = updateItemDto.Description;

            await repository.UpdateAsync(existingBook); 
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await repository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }
            await repository.RemoveAsync(item.Id);

            return NoContent();
        }
    }
}
