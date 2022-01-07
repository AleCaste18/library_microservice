using booksService.Business;
using booksService.Business.Interfaces;
using booksService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
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

        private void PublishToMessageQueue(string integrationEvent, string eventData)
        {
            // TOOO: Reuse and close connections and channel, etc, 
            // Configurazione RabbitMQ per ambiente di test con docker
            var factory = new ConnectionFactory() { HostName = "host.docker.internal", Port=5672, UserName="prova", Password="password", VirtualHost="/" }; //Rabbit on another docker-compose
           //var factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672, UserName = "prova", Password = "password", VirtualHost = "/" }; //Rabbit on same docker-compose
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "nuovi_libri",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);



                var body = Encoding.UTF8.GetBytes(eventData);

                channel.BasicPublish(exchange: "",
                                             routingKey: "nuovi_libri",
                                             basicProperties: null,
                                             body: body);
            }
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

            var integrationEventData = JsonConvert.SerializeObject(new
            {
                id = book.Id,
                title = book.Title,
                author = book.Author,
                category = book.Category

            });
            PublishToMessageQueue("newbooks", integrationEventData);


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
