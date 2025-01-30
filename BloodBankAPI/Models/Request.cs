using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BloodBankAPI.Models
{
    public class Request
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "RecipientId is required.")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string RecipientId { get; set; }

        [Required(ErrorMessage = "Blood Type is required.")]
        [RegularExpression("^(A|B|AB|O)[+-]$", ErrorMessage = "Invalid blood type format.")]
        public string BloodType { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int QuantityInUnits { get; set; }

        [Required(ErrorMessage = "Donation date is required.")]
        public DateTime RequestDate { get; set; }

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
