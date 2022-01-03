using booksService.Business.Interfaces;
using booksService.Models;
using booksService.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace booksService.Business
{
    public class BooksBusiness : BaseBusiness<IBookRepository, Book>, IBooksBusiness
    {
        public BooksBusiness(IBookRepository repository, ILogger<BooksBusiness> logger) 
            : base(repository, logger)
        {
        }


    }
}
