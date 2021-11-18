using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace customersService.Entities
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public int Card { get; set; }
    }
}
