using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace BloodBankAPI.Models{

public class BloodDonor
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string ContactInfo { get; set; }
    public string BloodType { get; set; } // Reference to BloodType
}
}