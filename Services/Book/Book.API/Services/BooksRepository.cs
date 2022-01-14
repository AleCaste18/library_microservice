using Book.API.Data;
using Book.API.Models;
using Book.API.Services.Interfaces;

namespace Book.API.Services
{
    public class BooksRepository : BaseRepository<Books>, IBookRepository
    {
        public BooksRepository(BookContext bookContext) : base(bookContext)
        {
        }
    }
}
