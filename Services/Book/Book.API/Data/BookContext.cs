using Book.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Book.API.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions options) :
           base(options)
        {
        }
        public DbSet<Books> Books { get; set; }
    }
}
