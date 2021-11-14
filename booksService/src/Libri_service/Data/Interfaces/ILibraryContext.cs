using Libri_service.Entities;
using MongoDB.Driver;

namespace Libri_service.Data.Interfaces
{
    public interface ILibraryContext
    {
        IMongoCollection<Book> Books { get; }
    }
}
