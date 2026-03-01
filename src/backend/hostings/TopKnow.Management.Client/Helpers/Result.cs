namespace TopKnow.Management.Client.Helpers;

public record Error(string Code, string Message);

public class Result<T> : Result
{
	public T Value { get; set; }
}

public class Result
{
	public bool IsSuccess { get; }
	public Error Error { get; }
}