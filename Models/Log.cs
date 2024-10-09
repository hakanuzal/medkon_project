using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MedkonTestProject.Models
{
    public class Log
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("UserId")]
        public string UserId { get; set; }

        [BsonElement("LoginTime")]
        public DateTime LoginTime { get; set; }

        [BsonElement("LogoutTime")]
        public DateTime? LogoutTime { get; set; }
    }
}
