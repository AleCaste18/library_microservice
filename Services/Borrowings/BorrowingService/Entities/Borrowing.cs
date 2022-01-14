global using MongoDB;
global using MongoDB.Bson;
global using MongoDB.Bson.Serialization.Attributes;

namespace BorrowingService.Entities
{
    public class Borrowing
    {       
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } 
        public string IdCustomer { get; set; }
        public string IdBook { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }



}
}
