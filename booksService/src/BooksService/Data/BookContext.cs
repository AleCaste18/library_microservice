using booksService.Models;
using Microsoft.EntityFrameworkCore;

namespace booksService.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions options) :
            base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
    }
}
