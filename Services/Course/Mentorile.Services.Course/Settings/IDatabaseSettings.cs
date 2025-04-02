namespace Mentorile.Services.Course.Settings;
public interface IDatabaseSettings
{
    public string CourseCollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}