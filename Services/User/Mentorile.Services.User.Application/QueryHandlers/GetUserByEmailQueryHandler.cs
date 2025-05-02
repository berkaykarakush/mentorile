using AutoMapper;
using MediatR;
using Mentorile.Services.User.Application.DTOs;
using Mentorile.Services.User.Application.Queries;
using Mentorile.Services.User.Domain.Services;
using Mentorile.Shared.Common;

namespace Mentorile.Services.User.Application.QueryHandlers;
public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, Result<UserDTO>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByEmailQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserDTO>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetByEmailAsync(request.Email);
        if(!userResult.IsSuccess) return Result<UserDTO>.Failure("User not found.");

        var userDTO = _mapper.Map<UserDTO>(userResult.Data);
        return Result<UserDTO>.Success(userDTO);
    }
}