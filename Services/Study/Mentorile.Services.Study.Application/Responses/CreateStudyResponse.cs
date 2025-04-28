namespace Mentorile.Services.Study.Application.Responses;
public class CreateStudyResponse
{
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public string StudyId { get; set;}
}