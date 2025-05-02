using AutoMapper;
using MediatR;
using Mentorile.Services.User.Application.DTOs;
using Mentorile.Services.User.Application.Queries;
using Mentorile.Services.User.Domain.Services;
using Mentorile.Shared.Common;

namespace Mentorile.Services.User.Application.QueryHandlers;
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDTO>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetByIdAsync(request.UserId);
        if(!userResult.IsSuccess) return Result<UserDTO>.Failure("User not found.");

        var userDTO = _mapper.Map<UserDTO>(userResult.Data);
        if(userDTO == null) return Result<UserDTO>.Failure("Mapping failed.");

        return Result<UserDTO>.Success(userDTO);
    }
}