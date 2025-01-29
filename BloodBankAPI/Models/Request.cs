using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BloodBankAPI.Models
{
    public class Request
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("RecipientId")]
        public string RecipientId { get; set; } = string.Empty;

        [BsonElement("BloodType")]
        public string BloodType { get; set; } = string.Empty;

        [BsonElement("Quantity")]
        public int Quantity { get; set; }

        [BsonElement("RequestDate")]
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;

        public void SetId(string? id)
    {
        if (!string.IsNullOrWhiteSpace(id) && ObjectId.TryParse(id, out var objectId))
        {
            Id = objectId.ToString(); 
        }
        else
        {
            Id = ObjectId.GenerateNewId().ToString(); 
        }
 
} 
    }
}
