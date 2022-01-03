using BooksService.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksService.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) 
        {
        }

        public virtual DbSet<Book> Books { get; set; }

        // OnModelCreating method provides the ability for us to manage the table properties of the tables in the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
