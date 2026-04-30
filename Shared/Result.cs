namespace InvoiceSystem.Shared;

public class Result<T>
{
    public T Data { get; set; }
    public bool Success { get; set; }
    public string Error { get; set; }
    public List<string> Details { get; set; } = new();

    public static Result<T> SuccessResult(T data) => new() { Data = data, Success = true };
    public static Result<T> FailureResult(string error, List<string> details = null) => 
        new() { Error = error, Success = false, Details = details ?? new() };
}

public class Result : Result<object>
{
    public static Result SuccessResult() => new() { Success = true };
}
