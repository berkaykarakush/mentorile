using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mentorile.Services.Discount.Models;
public class Discount
{
    [BsonId] 
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } 

    [BsonElement("userId")]
    public string UserId { get; set; }

    [BsonElement("rate")]
    public int Rate { get; set; }

    [BsonElement("code")]
    public string Code { get; set; }

    [BsonElement("isActive")]
    public bool IsActive { get; set; } = true;

    [BsonElement("createdDate")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedDate { get; set; }
    
    [BsonElement("expirationDate")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime ExpirationDate { get; set; }
}