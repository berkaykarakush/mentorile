using System.Reflection;
using AutoMapper;
using Mentorile.Services.Course.DTOs.Course;
using Mentorile.Services.Course.Models;
using Mentorile.Services.Course.Settings;
using Mentorile.Shared.Common;
using MongoDB.Driver;

namespace Mentorile.Services.Course.Services;
public class CourseService : ICourseService
{
    private readonly IMongoCollection<Models.Course> _courseCollection;   
    private readonly IMapper _mapper;

    public CourseService(IMongoCollection<Models.Course> courseCollection, IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);        
        _courseCollection = database.GetCollection<Models.Course>(databaseSettings.CourseCollectionName);
        _mapper = mapper;
    }

    public async Task<Result<List<CourseDTO>>> GetAllAsync(PagingParams pagingParams)
    {
        var courses = await _courseCollection
                            .Find(courses => true)
                            .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
                            .Limit(pagingParams.PageSize)
                            .ToListAsync();

        return Result<List<CourseDTO>>.Success(_mapper.Map<List<CourseDTO>>(courses));
    }

    public async Task<Result<CourseDTO>> GetByIdAsync(string id)
    {
        var course = await _courseCollection.FindAsync<Models.Course>(c => c.Id == id);
        if (course == null) return Result<CourseDTO>.Failure("Category not found.");

        return Result<CourseDTO>.Success(_mapper.Map<CourseDTO>(course));
    }

    public async Task<Result<CourseDTO>> CreateCourseAsync(CreateCourseDTO createCourseDTO)
    {
        var course = _mapper.Map<Models.Course>(createCourseDTO);
        course.CreatedTime = DateTime.UtcNow;

        await _courseCollection.InsertOneAsync(course);
        return Result<CourseDTO>.Success(_mapper.Map<CourseDTO>(course));
    }

    public async Task<Result<CourseDTO>> UpdateCourseAsync(UpdateCourseDTO updateCourseDTO)
    {
        var updateCourse = _mapper.Map<Models.Course>(updateCourseDTO);
        var result = await _courseCollection.FindOneAndReplaceAsync(c => c.Id == updateCourseDTO.Id, updateCourse);

        if (result == null) return Result<CourseDTO>.Failure("Course not found.");

        return Result<CourseDTO>.Success(null, "Course successfully updated.");
    }

    public async Task<Result<CourseDTO>> DeleteAsync(string id)
    {
        var result = await _courseCollection.DeleteOneAsync(c => c.Id == id);
        
        if (result.DeletedCount > 0) return Result<CourseDTO>.Success(null, "Course deleted successfully."); 

        return Result<CourseDTO>.Failure("Course not found.");
    }

    public async Task<Result<List<CourseDTO>>> GetAllByUserIdAsync(string userId)
    {
        var courses = await _courseCollection.Find<Models.Course>(c => c.UserId == userId).ToListAsync();
        var coursesDTO = _mapper.Map<List<CourseDTO>>(courses);
        return Result<List<CourseDTO>>.Success(coursesDTO);

        
    }
}