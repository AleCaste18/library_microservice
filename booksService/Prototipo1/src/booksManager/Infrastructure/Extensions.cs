using booksManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booksManager.Infrastructure
{
    public static class Extensions
    {
        public static BookDto AsDto(this Book book)
        {
            return new BookDto(book.Id, book.Name, book.Author, book.Category, book.Description, book.CreatedDate);
        }
    }
}
