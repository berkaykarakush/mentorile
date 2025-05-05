using MassTransit;
using Mentorile.Services.Course.Domain.Exceptions;
using Mentorile.Services.Course.Domain.Interfaces;
using Mentorile.Services.Course.Infrastructure.Persistence;
using Mentorile.Shared.Common;
using Mentorile.Shared.Messages.Events;
using Mentorile.Shared.Services.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Mentorile.Services.Course.Infrastructure.Repositories;
public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IExecutor _executor;

    public CourseRepository(AppDbContext appDbContext, IPublishEndpoint publishEndpoint, IExecutor executor)
    {
        _appDbContext = appDbContext;
        _publishEndpoint = publishEndpoint;
        _executor = executor;
    }

    public async Task<Result<Domain.Entities.Course>> CreateCourseAsync(Domain.Entities.Course course)
        => await _executor.ExecuteAsync(async () =>{
            await _appDbContext.Courses.AddAsync(course);
            await _appDbContext.SaveChangesAsync();
            return course;
        });

    public async Task<Result<bool>> DeleteAsync(string id)
        => await _executor.ExecuteAsync(async () => {
            var course = await _appDbContext.Courses.FindAsync(id);
            if (course == null) throw new CourseNotFoundException();
            course.Delete();
            _appDbContext.Courses.Update(course);
            var result = await _appDbContext.SaveChangesAsync() > 0;
            return result;
        });

    public async Task<Result<PagedResult<Domain.Entities.Course>>> GetAllAsync(PagingParams pagingParams)
        => await _executor.ExecuteAsync(async () => {
            var query = _appDbContext.Courses;
            var totalCount = await query.CountAsync();
            var courses = await query
                .Skip((pagingParams.PageNumber -1 ) * pagingParams.PageSize)
                .Take(pagingParams.PageSize)
                .ToListAsync();
            var paged = PagedResult<Domain.Entities.Course>.Create(courses, totalCount, pagingParams);
            return paged;
        });

    public async Task<Result<PagedResult<Domain.Entities.Course>>> GetAllByUserIdAsync(string userId, PagingParams pagingParams)
        => await _executor.ExecuteAsync(async () => {
            var query = _appDbContext.Courses.Where(q => q.UserId == userId);
            var totalCount = await query.CountAsync();
            var courses = await query
                .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
                .Take(pagingParams.PageSize)
                .ToListAsync();
            var paged = PagedResult<Domain.Entities.Course>.Create(courses, totalCount, pagingParams);
            return paged;
        });

    public async Task<Result<Domain.Entities.Course>> GetByIdAsync(string id)
        => await _executor.ExecuteAsync(async () => {
            var course = await _appDbContext.Courses.FindAsync(id);
            if(course == null) throw new CourseNotFoundException();
            return course;
        });

    public async Task<Result<bool>> UpdateCourseAsync(Domain.Entities.Course course)
        => await _executor.ExecuteAsync(async () => {
            _appDbContext.Courses.Update(course);
            var result = await _appDbContext.SaveChangesAsync() > 0;
            if(!result) throw new Exception("Course could not be updated.");

            await _publishEndpoint.Publish<CourseNameChangedEvent>(new CourseNameChangedEvent(){
                CourseId = course.Id,
                UpdatedName = course.Name
            });
            return result;
        });
}