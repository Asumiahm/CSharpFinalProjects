using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BloodBankAPI.Models
{
    public class Recipient
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("BloodType")]
        public string BloodType { get; set; } = string.Empty;

        [BsonElement("Age")]
        public int Age { get; set; }

        [BsonElement("ContactNumber")]
        public string ContactNumber { get; set; } = string.Empty;
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
