using BooksService.Configuration;
using BooksService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BooksService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _unitOfWork.Book.All();
            return Ok(users);
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var item = await _unitOfWork.Book.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<IActionResult> CreateBook(Book book)
        {
            if (ModelState.IsValid)
            {
                book.Id = Guid.NewGuid();

                await _unitOfWork.Book.Add(book);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetBook", new { book.Id }, book);
            }

            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var item = await _unitOfWork.Book.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Book.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}
