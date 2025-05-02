using MediatR;
using Mentorile.Services.User.Application.Commands;
using Mentorile.Services.User.Domain.Entities;
using Mentorile.Services.User.Domain.Services;
using Mentorile.Shared.Common;

namespace Mentorile.Services.User.Application.CommandHandlers;
public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, Result<bool>>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserProfileCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<bool>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.UpdateAsync(
            new UserProfile(){
            Id = request.UserId,
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        });
        
        if(!userResult.IsSuccess) return Result<bool>.Failure("Failed to update profile.");
        return Result<bool>.Success(userResult.Data, "Profile updated successfully.");
    }
}