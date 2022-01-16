using BorrowingService.Entities;
using BorrowingService.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorrowingServiceTest
{
    public class BorrowingRepositoryFake : IBorrowingRepository
    {
        private readonly List<Borrowing> _borrowings;

        public BorrowingRepositoryFake()
        {
            _borrowings = new List<Borrowing>()
            {
                new Borrowing() { Id = "602d2149e773f2a3990b47f6", IdCustomer = "1", IdBook = "1"  },
                new Borrowing() { Id = "602d2149e773f2a3990b47f7", IdCustomer = "2", IdBook = "2" },
                new Borrowing() { Id = "602d2149e773f2a3990b47f8", IdCustomer = "3", IdBook = "3" }
            };
        }

        public async Task<IEnumerable<Borrowing>> GetBorrowing()
        {
            return _borrowings;
        }
        public async Task CreateBorrowing(Borrowing borrowing)
        {
            _borrowings.Add(borrowing);
        }

        public async Task<Borrowing> GetBorrowing(string id)
        {
            return _borrowings
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public Task<bool> UpdateBorrowing(Borrowing customer)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteBorrowing(string id)
        {
            var existing = _borrowings.First(a => a.Id == id);
            _borrowings.Remove(existing);
            return true;
        }
    }
}