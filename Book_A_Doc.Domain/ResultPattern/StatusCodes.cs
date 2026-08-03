namespace Book_A_Doc.Domain.ResultPattern;

public static class StatusCodes
{
    // 2xx Success
    public const int OK = 200;
    public const int Created = 201;
    public const int Accepted = 202;
    public const int NoContent = 204;

    // 3xx Redirection
    public const int MovedPermanently = 301;
    public const int Found = 302;
    public const int NotModified = 304;

    // 4xx Client Errors
    public const int BadRequest = 400;
    public const int Unauthorized = 401;
    public const int PaymentRequired = 402;
    public const int Forbidden = 403;
    public const int NotFound = 404;
    public const int MethodNotAllowed = 405;
    public const int Conflict = 409;
    public const int Gone = 410;
    public const int UnsupportedMediaType = 415;
    public const int UnprocessableEntity = 422;
    public const int TooManyRequests = 429;

    // 5xx Server Errors
    public const int InternalServerError = 500;
    public const int NotImplemented = 501;
    public const int BadGateway = 502;
    public const int ServiceUnavailable = 503;
    public const int GatewayTimeout = 504;
}
