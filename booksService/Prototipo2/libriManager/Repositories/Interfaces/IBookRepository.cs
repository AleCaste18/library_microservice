using libriManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace libriManager.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBooks(string id);
        Task<IEnumerable<Book>> GetBookByName(string name);
        Task<IEnumerable<Book>> GetBookByCategory(string name);
        Task<IEnumerable<Book>> GetBookByAuthor(string name);

        Task CreateBook(Book book);
        Task<bool> UpdateBook(Book book);
        Task<bool> DeleteBook(string id);

    }
}
