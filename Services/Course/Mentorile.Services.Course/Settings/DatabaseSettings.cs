namespace Mentorile.Services.Course.Settings;
public class DatabaseSettings : IDatabaseSettings
{
    public string CourseCollectionName {get;set;}
    public string ConnectionString {get;set;}
    public string DatabaseName {get;set;}
}