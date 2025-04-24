using AutoMapper;
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

    public async Task<Result<List<DiscountDTO>>> GetAllDiscountAsync()
    {
        var discounts = await _discountCollection.Find(_ => true).ToListAsync();
        var dtoList = _mapper.Map<List<DiscountDTO>>(discounts);
        return Result<List<DiscountDTO>>.Success(dtoList);
    }

    public async Task<Result<DiscountDTO>> GetByCodeAndUserIdAsync(string code, string userId)
    {
        var discount = await _discountCollection.Find(d =>
            d.Code == code &&
            d.UserIds.Contains(userId) &&
            d.IsActive &&
            d.ExpirationDate >= DateTime.UtcNow    
        ).FirstOrDefaultAsync();
        if (discount is null) return Result<DiscountDTO>.Failure("Discount not found or not valid for this user.");
        var discountDTO = _mapper.Map<DiscountDTO>(discount); 
        return Result<DiscountDTO>.Success(discountDTO);
    }

    public async Task<Result<DiscountDTO>> CreateAsync(CreateDiscountDTO createDiscountDTO)
    {
        var discount = _mapper.Map<Models.Discount>(createDiscountDTO);
        discount.CreatedDate = DateTime.UtcNow;
        discount.ExpirationDate = DateTime.UtcNow.AddDays(10);
        await _discountCollection.InsertOneAsync(discount);
        var discountDTO =_mapper.Map<DiscountDTO>(discount);
        return Result<DiscountDTO>.Success(discountDTO);
    }

    public async Task<Result<bool>> DeleteAsync(string discountId)
    {
        var result = await _discountCollection.DeleteOneAsync(d => d.Id == discountId);
        return result.DeletedCount > 0 ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to deleting");
    }

    public async Task<Result<decimal>> ApplyDiscountAsync(string code, decimal totalPrice)
    {
        var discount = await _discountCollection
            .Find(d => d.Code == code && d.IsActive && d.ExpirationDate >= DateTime.UtcNow)
            .FirstOrDefaultAsync();
        
        if(discount == null) return Result<decimal>.Failure("Invalid or expired discount code.");

        // indirim oranini al
        var discountAmount = discount.Rate / 100 * totalPrice;
        var discountedPrice = totalPrice - discountAmount;

        return Result<decimal>.Success(discountedPrice);

    }

    public async Task<Result<bool>> CancelDiscountAsync(string code)
    {
        var discount = await _discountCollection
            .Find(d => d.Code == code && d.IsActive)
            .FirstOrDefaultAsync();
        if(discount == null) return Result<bool>.Failure("Discount not found or already cancelled.");

        // indirim kodunu gecersiz hale getirme
        discount.IsActive = false;
        var result = await _discountCollection.ReplaceOneAsync(d => d.Id == discount.Id, discount);
        return result.ModifiedCount > 0 ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to cancel the discount.");
    }

    public async Task<Result<DiscountDTO>> UpdateAsync(UpdateDiscountDTO updateDiscountDTO)
    {
        var existingDiscount = await _discountCollection.Find(x => x.Id == updateDiscountDTO.Id).FirstOrDefaultAsync();

        if(existingDiscount == null) return Result<DiscountDTO>.Failure("Discount not found.");

        existingDiscount.Code = updateDiscountDTO.Code;
        existingDiscount.Rate = updateDiscountDTO.Rate;
        existingDiscount.ExpirationDate = updateDiscountDTO.ExpirationDate;
        existingDiscount.IsActive = updateDiscountDTO.IsActive;

        var result = await _discountCollection.ReplaceOneAsync(x => x.Id == updateDiscountDTO.Id, existingDiscount);
        if(!result.IsAcknowledged || result.ModifiedCount == 0) return Result<DiscountDTO>.Failure("Failed to update discount.");
        
        var discountDTO = _mapper.Map<DiscountDTO>(existingDiscount);
        return Result<DiscountDTO>.Success(discountDTO);
    }

    public async Task<Result<List<DiscountDTO>>> GetAllDiscountByUserIdAsync(string userId)
    {
        var discountCodes = await _discountCollection.Find(x => x.UserIds.Contains(userId)).ToListAsync();
        var dtoList = _mapper.Map<List<DiscountDTO>>(discountCodes);
        return Result<List<DiscountDTO>>.Success(dtoList);
    }

    public async Task<Result<DiscountDTO>> GetDiscountByIdAsync(string discountId)
    {
        var discount = await _discountCollection.Find(x => x.Id == discountId).FirstOrDefaultAsync();
        if(discount == null) return Result<DiscountDTO>.Failure("Discount not found.");
        var discountDTO = _mapper.Map<DiscountDTO>(discount);
        return Result<DiscountDTO>.Success(discountDTO);
    }
}