using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BloodBankAPI.Models{

public class Donation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string DonorId { get; set; } 
    public string BloodType { get; set; } 
    public int QuantityInUnits { get; set; }
    public DateTime DonationDate { get; set; }
    public void SetId(string? id)
    {
        if (!string.IsNullOrWhiteSpace(id) && ObjectId.TryParse(id, out var objectId))
        {
            Id = objectId.ToString(); // Store as valid ObjectId
        }
        else
        {
            Id = ObjectId.GenerateNewId().ToString(); // Generate new ObjectId if invalid
        }
 
}    
}
}