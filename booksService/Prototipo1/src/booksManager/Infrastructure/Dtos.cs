using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace booksManager.Infrastructure
{
        public record BookDto(Guid Id, string Name, string Author, string Category, string Description, DateTimeOffset CreatedDate);
        public record CreateBookDto([Required] string Name, [Required] string Author, string Category, string Description, DateTimeOffset CreatedDate);
        public record UpdateBookDto([Required] string Name, [Required] string Author, string Category, string Description, DateTimeOffset CreatedDate);
    
}
