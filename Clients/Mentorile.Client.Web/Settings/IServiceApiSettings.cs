namespace Mentorile.Client.Web.Settings;
public interface IServiceApiSettings
{
    public string GatewayBaseUri { get; set; }    
    public string IdentityBaseUri { get; set; }    
    public string PhotoStockUri { get; set; }
    public ServiceApi Course { get; set; }
    public ServiceApi PhotoStock { get; set; }
    public ServiceApi Basket { get; set; }
    public ServiceApi Discount { get; set; }
    public ServiceApi Payment { get; set; }
    public ServiceApi Order { get; set; }
    public ServiceApi Study { get; set; }
    public ServiceApi User { get; set; }
}