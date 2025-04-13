using Mentorile.Services.Order.Domain.Core;

namespace Mentorile.Services.Order.Domain.OrderAggreagate;
public class Address : ValueObject
{
    public Address(string province, string disctrict, string street, string line, string zipCode)
    {
        Province = province;
        Disctrict = disctrict;
        Street = street;
        Line = line;
        ZipCode = zipCode;
    }

    public string Province { get; private set; }    
    public string Disctrict { get; private set; }
    public string Street { get; private set; }
    public string Line { get; private set; }
    public string ZipCode { get; private set; }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Province;
        yield return Disctrict;
        yield return Street;
        yield return Line;
        yield return ZipCode;
    }
}