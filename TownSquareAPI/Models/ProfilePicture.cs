using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TownSquareAPI.Models
{
    public class ProfilePicture
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Picture { get; set; }
        public string UserId { get; set; }
    }
}
