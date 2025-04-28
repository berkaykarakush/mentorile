using Mentorile.Shared.Common;

namespace Mentorile.Client.Web.ViewModels.Studies;
public class StudyIndexViewModel
{
    public List<StudyViewModel> Studies { get; set; }
    public PagingParams PagingParams { get; set; }
}