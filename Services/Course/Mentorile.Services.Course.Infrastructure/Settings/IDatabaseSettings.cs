namespace Mentorile.Services.Course.Infrastructure.Settings;
public interface IDatabaseSettings
{
    public string CourseCollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}