using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TownSquareAPI.Models
{
    public class ProfilePicture
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public byte[] Picture { get; set; }
        public int userId { get; set; }
        public bool IsDeafult { get; set; }
    }
}
