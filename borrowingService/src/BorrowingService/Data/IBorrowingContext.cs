using BorrowingService.Entities;
using MongoDB.Driver;

namespace BorrowingService.Data
{
    public interface IBorrowingContext
    {
        IMongoCollection<Borrowing> Borrowing { get; }
    }
}
