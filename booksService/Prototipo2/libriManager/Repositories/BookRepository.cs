using libriManager.Data.Interfaces;
using libriManager.Entities;
using libriManager.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace libriManager.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IBookContext _context;
        public BookRepository(IBookContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _context
                .Books
                .Find(b => true)
                .ToListAsync();
        }
        public async Task<Book> GetBooks(string id)
        {
            return await _context
               .Books
               .Find(b => b.Id == id)
               .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Book>> GetBookByName(string name)
        {
            FilterDefinition<Book> filter = Builders<Book>.Filter.ElemMatch(b => b.Name, name);

            return await _context
                            .Books
                            .Find(filter)
                            .ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetBookByCategory(string categoryName)
        {
            FilterDefinition<Book> filter = Builders<Book>.Filter.Eq(b => b.Category, categoryName);

            return await _context
                            .Books
                            .Find(filter)
                            .ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetBookByAuthor(string authorName)
        {
            FilterDefinition<Book> filter = Builders<Book>.Filter.Eq(b => b.Author, authorName);

            return await _context
                            .Books
                            .Find(filter)
                            .ToListAsync();
        }
        public async Task CreateBook(Book book)
        {
            await _context.Books.InsertOneAsync(book);
        }
        public async Task<bool> UpdateBook(Book book)
        {
            var updateResult = await _context
                                             .Books
                                             .ReplaceOneAsync(filter: g => g.Id == book.Id, replacement: book);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> DeleteBook(string id)
        {
            FilterDefinition<Book> filter = Builders<Book>.Filter.Eq(b => b.Id, id);

            DeleteResult deleteResult = await _context
                                                .Books
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }




    }
}
