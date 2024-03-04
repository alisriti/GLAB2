namespace GLab.Domains.Models.Shared;

public class Result
{
    private bool Successed { get; set; }
    public List<string> Errors { get; set; }

    private Result(bool success, List<string> errors = null)
    {
        Successed = success;
        Errors = errors;
    }

    public static Result Succes => new Result(true);

    public static Result Failure(List<string> errors) =>
        new Result(false, errors);
}