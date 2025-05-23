using MediatR;
using Mentorile.Shared.Common;

namespace Mentorile.Services.Email.Application.Commands;
public class SendManualEmailCommand : IRequest<Result<bool>>
{
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}