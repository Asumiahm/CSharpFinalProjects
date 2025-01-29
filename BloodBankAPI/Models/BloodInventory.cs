using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace BloodBankAPI.Models{
public class Inventory
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? BloodType { get; set; }  
    public int Quantity { get; set; }     
    public DateTime DonationDate { get; set; }
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