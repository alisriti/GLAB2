namespace GLab.Domains.Models.Shared;

public class Result
{
    private bool Successed { get; set; }
    public List<string> Errors { get; set; }


    protected Result(bool success, List<string> errors = null)
    {
        Successed = success;
        Errors = errors;
    }

    public static Result Succes => new Result(true);

    public static Result Failure(List<string> errors) =>
        new Result(false, errors);
}

public class Result<T> : Result
{
    public T Value { get;}

    private Result(T value, bool success, List<string> errors = null) : base(success, errors)
    {
        Value = value;
    }

    public static  Result<T> Succes(T value) => new Result<T>(value, true);
}