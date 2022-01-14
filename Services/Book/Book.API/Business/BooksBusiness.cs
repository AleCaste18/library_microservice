using Book.API.Business.Interfaces;
using Book.API.Models;
using Book.API.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Book.API.Business
{
    public class BooksBusiness : BaseBusiness<IBookRepository, Books>, IBooksBusiness
    {
        public BooksBusiness(IBookRepository repository, ILogger<BooksBusiness> logger)
            : base(repository, logger)
        {
        }


    }
}