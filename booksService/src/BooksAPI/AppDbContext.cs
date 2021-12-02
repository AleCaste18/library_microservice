using BooksAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) 
        : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
    }
}
