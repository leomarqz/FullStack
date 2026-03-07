
namespace TechNotes.Domain.Abstractions;

public class Result
{
    public bool IsSucessful { get; }
    public bool HasFailed => !IsSucessful;
    public string? ErrorMessage { get; }

    public Result(bool isSuccessful, string? errorMessage = null)
    {
        IsSucessful = isSuccessful;
        ErrorMessage = errorMessage;
    }

    public static Result Ok() => new(true);
    public static Result Fail(string? errorMessage) => new(false, errorMessage);

    public static Result<T> Ok<T>(T? value) => new(value, true, string.Empty);
    public static Result<T> Fail<T>(string? errorMessage) => new(default, false, errorMessage);

    public static Result<T> FromValue<T>(T? value) => value != null ? Ok<T>(value) : Fail<T>("El valor no puede ser nulo!");

    
    
}

public class Result<T> : Result
{
    public T? Value { get; }

    protected internal Result(T? value, bool isSuccessful, string? errorMessage = null) 
    : base(isSuccessful, errorMessage)
    {
        Value = value;
    }
    

    /*
    * Conversion Implicita
    */

    //Note -> Result<Note>
    public static implicit operator Result<T>(T? value) => FromValue<T>(value); 
    
    //Result<Note> -> Note
    public static implicit operator T?(Result<T> result) => result.Value;

}
