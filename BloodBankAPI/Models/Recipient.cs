using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BloodBankAPI.Models
{
    public class Recipient
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(18, 65, ErrorMessage = "Age must be between 18 and 65.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression("^(Male|Female|Other)$", ErrorMessage = "Gender must be 'Male', 'Female', or 'Other'.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Contact information is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string ContactInfo { get; set; }

        [Required(ErrorMessage = "Blood type is required.")]
        [RegularExpression("^(A|B|AB|O)[+-]$", ErrorMessage = "Invalid blood type (A+, A-, B+, B-, AB+, AB-, O+, O-).")]
        public string BloodType { get; set; }

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