using BooksService.Services;
using System.Threading.Tasks;

namespace BooksService.Configuration
{
    public interface IUnitOfWork
    {
        IBookRepository Book { get; }
        Task CompleteAsync();
        void Dispose();
    }
}
