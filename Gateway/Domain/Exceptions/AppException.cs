namespace Gateway.Domain.Exceptions;

public abstract class AppException : Exception
{
    public int StatusCode { get; }
    public string Title { get; }
    public string Type { get; }

    protected AppException(
        string message,
        int statusCode,
        string title,
        string type)
        : base(message)
    {
        StatusCode = statusCode;
        Title = title;
        Type = type;
    }
}