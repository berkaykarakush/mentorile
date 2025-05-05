namespace Mentorile.Services.Course.Domain.Entities;
public class Course
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string UserId { get; private set; }
    public DateTime CreateAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public bool IsDeleted { get; set; } = false;
    public string PhotoUri { get; private set; }
    public List<string> TopicIds { get; private set; } = new();

    private Course() { }

    public Course(string name, string userId, string photoUri)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        UserId = userId;
        PhotoUri = photoUri;
        CreateAt = DateTime.UtcNow;
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName)) throw new ArgumentException("Name cannot be empty");
        Name = newName;
    }

    public void AddTopic(string topicId)
    {
        if (!TopicIds.Contains(topicId))
            TopicIds.Add(topicId);
    }

    public void RemoveTopic(string topicId)
    {
        TopicIds.Remove(topicId);
    }

    public void Delete()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
        DeletedAt = DateTime.UtcNow;
    }
}