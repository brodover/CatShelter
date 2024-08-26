using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace WithAngularApp.Server.Database.Models
{
    public class Cat
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? OwnerId { get; set; }

        public string Name { get; set; } = null!;

        public byte Pattern { get; set; }

        public byte Color { get; set; }

        public byte Stats { get; set; }
    }
}
