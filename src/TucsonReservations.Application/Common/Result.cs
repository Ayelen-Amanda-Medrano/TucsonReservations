using System.Net;

namespace TucsonReservations.Application.Common;

public class Result<T>
{
    public bool Success { get; }
    public string Message { get; }
    public T? Data { get; }
    public HttpStatusCode StatusCode { get; }

    private Result(bool success, string message, T? data, HttpStatusCode statusCode)
    {
        Success = success;
        Message = message;
        Data = data;
        StatusCode = statusCode;
    }

    public static Result<T> Ok(T? data, HttpStatusCode statusCode, string? message = null)
        => new(true, message ?? string.Empty, data, statusCode);

    public static Result<T> Fail(string message, HttpStatusCode statusCode)
        => new(false, message, default, statusCode);
}

