namespace Gateway.Domain.Exceptions;

public sealed class ImTeapotException : AppException
{
    public ImTeapotException()
        : base(
            message: "Im A Teapot occured. This is a test exception",
            statusCode: StatusCodes.Status418ImATeapot,
            title: "Im A Teapot",
            type: "https://httpstatuses.com/418")
    {}
}