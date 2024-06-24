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

        public string? AdopterId { get; set; }

        public string Name { get; set; } = null!;

        public int Pattern { get; set; }

        public int Color { get; set; }

        public int Stats { get; set; }
    }
}
