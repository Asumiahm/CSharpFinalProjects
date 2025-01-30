using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BloodBankAPI.Models
{
    public class Donor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string ContactInfo { get; set; }
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
