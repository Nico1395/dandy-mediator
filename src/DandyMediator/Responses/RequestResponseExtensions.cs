using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace DandyMediator.Responses;

public static class RequestResponseExtensions
{
    public static bool IsSuccess_2xx(this IRequestResponse response) => response.Status <= RequestResponseStatus.NoContent_204;
    public static bool IsClientSide_4xx(this IRequestResponse response) => response.Status >= RequestResponseStatus.BadRequest_400 && response.Status <= RequestResponseStatus.Conflict_409;
    public static bool IsServerSide_5xx(this IRequestResponse response) => response.Status >= RequestResponseStatus.InternalServerError_500;
    public static bool IsStatus(this IRequestResponse response, RequestResponseStatus status) => response.Status == status;
    public static bool IsOK_200(this IRequestResponse response) => response.IsStatus(RequestResponseStatus.OK_200);
    public static bool IsCreated_201(this IRequestResponse response) => response.IsStatus(RequestResponseStatus.Created_201);
    public static bool IsAccepted_202(this IRequestResponse response) => response.IsStatus(RequestResponseStatus.Accepted_202);
    public static bool IsNoContent_204(this IRequestResponse response) => response.IsStatus(RequestResponseStatus.NoContent_204);
    public static bool IsBadRequest_400(this IRequestResponse response) => response.IsStatus(RequestResponseStatus.BadRequest_400);
    public static bool IsUnauthorized_401(this IRequestResponse response) => response.IsStatus(RequestResponseStatus.Unauthorized_401);
    public static bool IsForbidden_403(this IRequestResponse response) => response.IsStatus(RequestResponseStatus.Forbidden_403);
    public static bool IsNotFound_404(this IRequestResponse response) => response.IsStatus(RequestResponseStatus.NotFound_404);
    public static bool IsNotAcceptable_406(this IRequestResponse response) => response.IsStatus(RequestResponseStatus.NotAcceptable_406);
    public static bool IsConflict_409(this IRequestResponse response) => response.IsStatus(RequestResponseStatus.Conflict_409);
    public static bool IsInternalServerError_500(this IRequestResponse response) => response.IsStatus(RequestResponseStatus.InternalServerError_500);
    public static bool IsNotImplemented_501(this IRequestResponse response) => response.IsStatus(RequestResponseStatus.NotImplemented_501);
    public static bool IsServiceUnavailable_503(this IRequestResponse response) => response.IsStatus(RequestResponseStatus.ServiceUnavailable_503);

    public static bool ResultedIn(this IRequestResponse<bool> response, bool expected)
    {
        return response.IsSuccess_2xx() && response.Data == expected;
    }

    public static bool ResultedInTrue(this IRequestResponse<bool> response)
    {
        return response.ResultedIn(true);
    }

    public static bool ResultedInFalse(this IRequestResponse<bool> response)
    {
        return response.ResultedIn(false);
    }

    public static bool TryGetMetadata(this IRequestResponse response, string key, [MaybeNullWhen(false)] out object? value)
    {
        return response.Metadata.TryGetValue(key, out value);
    }

    public static bool HasMetadataKey(this IRequestResponse response, string key)
    {
        return response.Metadata.ContainsKey(key);
    }

    public static object? GetMetadataValueOrDefault(this IRequestResponse response, string key)
    {
        return response.Metadata.GetValueOrDefault(key);
    }

    public static object? GetMetadataValueOrDefault(this IRequestResponse response, string key, object? defaultValue)
    {
        return response.GetMetadataValueOrDefault(key) ?? defaultValue;
    }

    public static IResult ToResult(this IRequestResponse response)
    {
        var statusCode = (int)(HttpStatusCode)response.Status;
        if (statusCode >= 400)
            return CreateProblemResult(response, statusCode);

        if (response.Status == RequestResponseStatus.NoContent_204)
            return Results.NoContent();

        return Results.StatusCode(statusCode);
    }

    public static IResult ToResult<T>(this IRequestResponse<T> response)
    {
        var statusCode = (int)(HttpStatusCode)response.Status;
        if (statusCode >= 400)
            return CreateProblemResult(response, statusCode);

        return response.Status switch
        {
            RequestResponseStatus.OK_200 => Results.Ok(response.Data),
            RequestResponseStatus.Created_201 => Results.Created(GetLocation(response), response.Data),
            RequestResponseStatus.Accepted_202 => Results.Accepted(GetLocation(response), response.Data),
            RequestResponseStatus.NoContent_204 => Results.NoContent(),
            _ => Results.Json(response.Data, statusCode: statusCode)
        };
    }

    private static IResult CreateProblemResult(IRequestResponse response, int statusCode)
    {
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = response.Metadata.TryGetValue("title", out var title) ? title?.ToString() : ReasonPhrases.GetReasonPhrase(statusCode),
            Detail = response.Metadata.TryGetValue("detail", out var detail) ? detail?.ToString() : null
        };

        foreach (var kvp in response.Metadata)
        {
            if (kvp.Key is "title" or "detail")
                continue;

            problemDetails.Extensions[kvp.Key] = kvp.Value;
        }

        return Results.Problem(problemDetails);
    }

    private static string? GetLocation(IRequestResponse response)
    {
        return response.Metadata.TryGetValue("location", out var location) ? location?.ToString() : null;
    }
}
