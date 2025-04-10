namespace Mentorile.Services.Discount.Settings;
public class DatabaseSettings : IDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string DiscountCollectionName { get; set; } = null!;
}