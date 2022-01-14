using BorrowingService.Entities;

namespace BorrowingService.Repository.Interfaces
{
    public interface IBorrowingRepository
    {
        Task<IEnumerable<Borrowing>> GetBorrowing();
        Task<Borrowing> GetBorrowing(string id);

        Task CreateBorrowing(Borrowing borrowing);
        Task<bool> UpdateBorrowing(Borrowing borrowing);
        Task<bool> DeleteBorrowing(string id);
    }
}
