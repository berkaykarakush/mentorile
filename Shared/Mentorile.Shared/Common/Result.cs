using System.Text.Json.Serialization;

namespace Mentorile.Shared.Common;
public class Result<T>
{
    
    [JsonIgnore] // API donusunde saklar
    public bool IsSuccess => StatusCode >= 200 && StatusCode < 300;

    // Http kodu
    public int StatusCode { get; }

    // Genel mesaj
    public string Message { get; }

    // Dondurulen veri
    public T Data { get; }

    // Hata detaylari
    public List<string> ErrorDetails { get;}

    public Result(int statusCode, string message, T data, List<string> errorDetails)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
        ErrorDetails = errorDetails ?? new List<string>();
    }

    // Basarili donus
    public static Result<T> Success(T data, string message = "Success", int statusCode = 200) => new Result<T>(statusCode, message, data, null);

    // Hatali donus (Tek mesaj ile)
    public static Result<T> Failure(string message, int statusCode = 400) => new Result<T>(statusCode, message, default, new List<string> {message});

    // Hatali donus (Birden fazla mesaj ile)
    public static Result<T> Failure(List<string> errorDetails, int statusCode = 400) => new Result<T>(statusCode, "Validation error", default, errorDetails);
}