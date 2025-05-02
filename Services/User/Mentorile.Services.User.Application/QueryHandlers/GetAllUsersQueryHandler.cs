using AutoMapper;
using MediatR;
using Mentorile.Services.User.Application.DTOs;
using Mentorile.Services.User.Application.Queries;
using Mentorile.Services.User.Domain.Services;
using Mentorile.Shared.Common;

namespace Mentorile.Services.User.Application.QueryHandlers;
public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<PagedResult<UserDTO>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<UserDTO>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetAllAsync(request.PagingParams);
        if(!userResult.IsSuccess) return Result<PagedResult<UserDTO>>.Failure("Users not found.");

        var userDTOs = _mapper.Map<List<UserDTO>>(userResult.Data.Data);
        var pagedResultDTO = PagedResult<UserDTO>.Create(userDTOs, userResult.Data.TotalCount, request.PagingParams, userResult.StatusCode, userResult.Message);
        return Result<PagedResult<UserDTO>>.Success(pagedResultDTO);
    }
}