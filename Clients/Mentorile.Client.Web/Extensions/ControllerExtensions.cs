using Mentorile.Shared.Common;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Client.Web.Extensions;
public static class ControllerExtensions
{
    public static async Task<IActionResult> LoadPagedAsync<T>(
        this Controller controller,
        Func<Task<PagedResult<T>>> loader,
        PagingParams pagingParams,
        string viewName = "Index",
        string emptyMessage = "No data available.")
    {
        ArgumentNullException.ThrowIfNull(controller);
        ArgumentNullException.ThrowIfNull(loader);
        ArgumentNullException.ThrowIfNull(pagingParams);

        var result = await loader();

        controller.ViewBag.Paging = pagingParams;

        if (!result.IsSuccess || result.Data is null || !result.Data.Any())
        {
            var emptyResult = PagedResult<T>.Create(
                new List<T>(), 0, pagingParams, 200, emptyMessage);

            return controller.View(viewName, emptyResult);
        }

        var pagedResult = PagedResult<T>.Create(
            result.Data,
            result.TotalCount,
            pagingParams,
            200,
            "Success");

        return controller.View(viewName, pagedResult);
    }
}