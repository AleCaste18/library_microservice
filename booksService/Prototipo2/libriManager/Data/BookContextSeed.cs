using libriManager.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace libriManager.Data
{
    public class BookContextSeed
    {
        public static void SeedData(IMongoCollection<Book> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfiguredBooks());
            }
        }
        private static IEnumerable<Book> GetPreconfiguredBooks()
        {
            return new List<Book>()
            {
                new Book()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "Elon Musk. Tesla, SpaceX e la sfida per un futuro fantastico",
                    Author = "Ashlee Vance",
                    Category = "Bio",
                    Description = "descrizione libro 1"
                },
                new Book()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "Steve Jobs",
                    Author = "Walter Isaacson",
                    Category = "Bio",
                    Description = "descrizione libro 2"
                },
                new Book()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "Pensieri lenti e veloci",
                    Author = "Daniel Kahneman",
                    Category = "Psicologia",
                    Description = "descrizione libro 3"
                },
                new Book()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "Il Cigno nero",
                    Author = "Taleb",
                    Category = "Filosofia",
                    Description = "descrizione libro 4"
                },
                new Book()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "Homo deus. Breve storia del futuro",
                    Author = "Yuval Noah Harari",
                    Category = "Saggio",
                    Description = "descrizione libro 5"
                }
            };
        }
    }
}
