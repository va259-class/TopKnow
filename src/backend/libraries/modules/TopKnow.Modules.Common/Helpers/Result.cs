namespace TopKnow.Modules.Common.Helpers;

public record Error(string Code, string Message);

public class Result
{
    public bool IsSuccess { get; }
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new Result(true, null);
    public static Result Failure(Error error) => new Result(false, error);
    public static Result Empty() => new Result(false, null);
}

public class Result<T> : Result
{
    public T Value { get; set; }
    protected Result(T value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value, true, null);
    }

	public static new Result<T> Failure(Error error)
	{
		return new Result<T>(default, false, error);
	}

	public static new Result<T> Empty()
	{
		return new Result<T>(default, false, null);
	}
}
