using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BloodBankAPI.Models{

public class BloodDonation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string BloodDonor { get; set; } 
    public string BloodType { get; set; } 
    public int QuantityInUnits { get; set; }
    public DateTime DonationDate { get; set; }
}

}