using Mentorile.Shared.Common;

namespace Mentorile.Shared.Services.Abstracts;
public interface IExecutor
{
    Task<Result<T>> ExecuteAsync<T>(Func<Task<T>> operation, [System.Runtime.CompilerServices.CallerMemberName] string methodName = "");
}