using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Libri_service.Entities
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation (BsonType.ObjectId) ]
        public string Id { get; set; }

        [BsonElement ("Name")]
        public string Name { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }


    }
}
