using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MedkonTestProject.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("username")] // MongoDB'deki alan adı
        public string Username { get; set; }

        [BsonElement("password")] // MongoDB'deki alan adı
        public string Password { get; set; }
        [BsonElement("role")] // MongoDB'deki alan adı
        public string Role { get; set; } // Eğer rol alanı varsa
    }
}
