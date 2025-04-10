using AutoMapper;
using DnsClient.Protocol;
using Mentorile.Services.Discount.DTOs;
using Mentorile.Services.Discount.Settings;
using Mentorile.Shared.Common;
using MongoDB.Driver;

namespace Mentorile.Services.Discount.Services;
public class DiscountService : IDiscountService
{
    private readonly IMongoCollection<Models.Discount> _discountCollection;
    private readonly IMapper _mapper;

    public DiscountService(IDatabaseSettings settings, IMapper mapper)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _discountCollection = database.GetCollection<Models.Discount>(settings.DiscountCollectionName);
        _mapper = mapper;
    }

    public async Task<Result<List<DiscountDTO>>> GetAllAsync()
    {
        var discounts = await _discountCollection.Find(_ => true).ToListAsync();
        var dtoList = _mapper.Map<List<DiscountDTO>>(discounts);
        return Result<List<DiscountDTO>>.Success(dtoList);
    }

    public async Task<Result<DiscountDTO>> GetByCodeAndUserIdAsync(string code, string userId)
    {
        var discount = await _discountCollection.Find(d => d.Code == code && d.UserId == userId).FirstOrDefaultAsync();
        if (discount is null) return Result<DiscountDTO>.Failure("Discount not found");
        var discountDTO = _mapper.Map<DiscountDTO>(discount); 
        return Result<DiscountDTO>.Success(discountDTO);
    }

    public async Task<Result<DiscountDTO>> CreateAsync(CreateDiscountDTO createDiscountDTO)
    {
        var discount = _mapper.Map<Models.Discount>(createDiscountDTO);
        discount.CreatedDate = DateTime.UtcNow;
        await _discountCollection.InsertOneAsync(discount);
        var discountDTO =_mapper.Map<DiscountDTO>(discount);
        return Result<DiscountDTO>.Success(discountDTO);
    }

    public async Task<Result<bool>> DeleteAsync(string discountId)
    {
        var result = await _discountCollection.DeleteOneAsync(d => d.Id == discountId);
        return result.DeletedCount > 0 ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to deleting");
    }
}