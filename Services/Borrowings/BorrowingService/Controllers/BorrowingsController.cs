using BorrowingService.Entities;
using BorrowingService.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BorrowingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingsController : ControllerBase
    {
        private readonly IBorrowingRepository _repository;
        private readonly ILogger<BorrowingsController> _logger;

        public BorrowingsController(IBorrowingRepository repository, ILogger<BorrowingsController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }



        // GET: api/<Borrowings>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Borrowing>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Borrowing>>> GetBorrowing()
        {
            var borrowing = await _repository.GetBorrowing();
            return Ok(borrowing);
        }

        // GET api/<Borrowings>/5
        [HttpGet("{id:length(24)}", Name = "GetBorrowing")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Borrowing), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Borrowing>> GetBorrowingById(string id)
        {
            var borrowing = await _repository.GetBorrowing(id);

            if (borrowing == null)
            {
                _logger.LogError($"Borrowing with id: {id}, not found.");
                return NotFound();
            }

            return Ok(borrowing);
        }

        // POST api/<Borrowings>
        [HttpPost]
        [ProducesResponseType(typeof(Borrowing), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Borrowing>> CreateBorrowing([FromBody] Borrowing borrowing)
        {
            await _repository.CreateBorrowing(borrowing);

            return CreatedAtRoute("GetBorrowing", new { id = borrowing.Id }, borrowing);
        }

        // PUT api/<Borrowings>/5
        [HttpPut]
        [ProducesResponseType(typeof(Borrowing), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBorrowing([FromBody] Borrowing borrowing)
        {
            return Ok(await _repository.UpdateBorrowing(borrowing));
        }

        // DELETE api/<Borrowings>/5
        [HttpDelete("{id:length(24)}", Name = "DeleteBorrowing")]
        [ProducesResponseType(typeof(Borrowing), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBorrowingById(string id)
        {
            return Ok(await _repository.DeleteBorrowing(id));
        }
    }
}
