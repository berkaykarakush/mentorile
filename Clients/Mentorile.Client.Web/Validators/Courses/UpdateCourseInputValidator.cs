using FluentValidation;
using Mentorile.Client.Web.ViewModels.Courses;

namespace Mentorile.Client.Web.Validators.Courses;
public class UpdateCourseInputValidator : AbstractValidator<UpdateCourseInput>
{
    private static readonly string[] AllowedExtensions = {".jpg", ".jpeg",".png"};
    private const long MaxFileSizeInBytes = 5 * 1024 * 1024; // max 5 mb
    public UpdateCourseInputValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id alanı boş olamaz.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} alanı boş olamaz.")
            .MaximumLength(100)
            .WithMessage("{PropertyName} alanı en fazla 100 karakter olabilir.")
            .WithName("Kurs İsmi");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("{PropertyName} değeri boş olamaz.")
            .WithName("Kullanıcı Id");

        RuleFor(x => x.PhotoFormFile)
            .Must(BeValidFileSize)
            .WithMessage("Dosya boyutu 5 MB'dan büyük olamaz.")
            .When(x => x.PhotoFormFile != null)
            .WithName("Resim");

        RuleFor(x => x.PhotoFormFile)
            .Must(BeValidFileExtension)
            .WithMessage("Yalnızca .jpg, .jpeg veya .png dosyalarınza izin verilir.")
            .When(x => x.PhotoFormFile != null)
            .WithName("Resim");
    }

    private bool BeValidFileSize(IFormFile file) => file.Length <= MaxFileSizeInBytes;

    private bool BeValidFileExtension(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return AllowedExtensions.Contains(extension);
    }
}