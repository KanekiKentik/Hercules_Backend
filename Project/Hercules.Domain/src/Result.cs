public class Result
{
    public bool IsSuccess { get; init; }
    public bool IsFailure { get => !IsSuccess; }
    public ErrorType ErrorType {get; init; }
    public string? Message {get; init; }

    protected Result(bool isSuccess, ErrorType errorType = ErrorType.None, string? message = null)
        => (IsSuccess, ErrorType, Message) = (isSuccess, errorType, message);

    public static Result Success() => new(true);
    public static Result Failure(ErrorType error, string? message = null) => new (false, error, message);
}

public sealed class Result<T> : Result
{
    public T Value { get; init; }
    private Result(bool isSuccess, T value, ErrorType errorType = ErrorType.None, string? message = null) : base(isSuccess, errorType, message)
        => Value = value;

    public static Result<T> Success(T value) => new(true, value);
    public new static Result<T> Failure(ErrorType error, string? message = null) => new (false, default!, error, message);
}