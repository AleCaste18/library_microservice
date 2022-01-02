using booksService.Data;
using booksService.Models;
using booksService.Services.Interfaces;

namespace booksService.Services
{
    public class BooksRepository : BaseRepository<Book>, IBookRepository
    {
        public BooksRepository(BookContext bookContext) : base(bookContext)
        {
        }
    }
}
