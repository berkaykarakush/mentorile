
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mentorile.Services.Course.Models;
public class Course
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public string Name { get; set; }

    [BsonRepresentation(BsonType.String)]
    public string UserId { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedTime { get; set; }

    // Sadece Topic ID'leri tutar
    public List<string> TopicIds { get; set; } = new List<string>();
}