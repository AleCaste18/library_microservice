using BooksService.Data;
using BooksService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BooksService.Services
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(DBContext context, ILogger logger) : base(context, logger) 
        {
        }

        public override async Task<IEnumerable<Book>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(BookRepository));
                return new List<Book>();
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(BookRepository));
                return false;
            }
        }

        public override async Task<bool> Upsert(Book entity)
        {
            try
            {
                var existingBook = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingBook == null)
                    return await Add(entity);

                existingBook.Title = entity.Title;
                existingBook.Author = entity.Author;
                existingBook.Category = entity.Category;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(BookRepository));
                return false;
            }
        }

    }
}
