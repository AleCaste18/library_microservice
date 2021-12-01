using BorrowingService.Data;
using BorrowingService.Entities;
using BorrowingService.Repository.Interfaces;
using MongoDB.Driver;

namespace BorrowingService.Repository
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly IBorrowingContext _context;

        public BorrowingRepository(IBorrowingContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task CreateBorrowing(Borrowing borrowing)
        {
            await _context.Borrowing.InsertOneAsync(borrowing);
        }

        public async Task<bool> DeleteBorrowing(string id)
        {
            FilterDefinition<Borrowing> filter = Builders<Borrowing>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Borrowing
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Borrowing>> GetBorrowing()
        {
            return await _context
                             .Borrowing
                             .Find(p => true)
                             .ToListAsync();
        }

        public async Task<Borrowing> GetBorrowing(string id)
        {
            return await _context
                           .Borrowing
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateBorrowing(Borrowing borrowing)
        {
            var updateResult = await _context
                                       .Borrowing
                                       .ReplaceOneAsync(filter: g => g.Id == borrowing.Id, replacement: borrowing);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}
