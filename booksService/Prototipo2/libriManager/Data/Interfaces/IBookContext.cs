using libriManager.Entities;
using MongoDB.Driver;

namespace libriManager.Data.Interfaces
{
    public interface IBookContext
    {
        IMongoCollection<Book> Books { get; }
    }
}
