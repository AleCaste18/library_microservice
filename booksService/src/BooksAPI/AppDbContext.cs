using BooksAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI
{
    /*Per usare Entity Framework per eseguire query, inserire, aggiornare ed eliminare dati usando oggetti .NET, 
      è prima di tutto necessario creare un modello che esegue il mapping delle entità e delle relazioni definite nel modello alle tabelle di un database.*/
    public class AppDbContext : DbContext  
    {
        public AppDbContext(DbContextOptions options) 
        : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
    }
}
