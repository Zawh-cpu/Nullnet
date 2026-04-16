namespace Gateway.Domain.Exceptions;

public class IncorrectSearchPolicyException : AppException
{
    public IncorrectSearchPolicyException(string[] fields)
        : base(
            message: $"You must set at leats '{string.Join(", ", fields)}' made search request.",
            statusCode: StatusCodes.Status400BadRequest,
            title: "Incorrect Search Policy",
            type: "https://httpstatuses.com/400")
    {
    }
}