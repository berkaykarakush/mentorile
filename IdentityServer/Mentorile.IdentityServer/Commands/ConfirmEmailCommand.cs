using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.IdentityServer.Commands;
public class ConfirmEmailCommand : IRequest<Result<bool>>
{
    public string UserId { get; set; } = default!;
    public string ConfirmationCode { get; set; } = default!;
}