using System.Net;

namespace DandyMediator.Responses;

public enum RequestResponseStatus
{
    OK_200 = HttpStatusCode.OK,
    Created_201 = HttpStatusCode.Created,
    Accepted_202 = HttpStatusCode.Accepted,
    NoContent_204 = HttpStatusCode.NoContent,
    BadRequest_400 = HttpStatusCode.BadRequest,
    Unauthorized_401 = HttpStatusCode.Unauthorized,
    Forbidden_403 = HttpStatusCode.Forbidden,
    NotFound_404 = HttpStatusCode.NotFound,
    NotAcceptable_406 = HttpStatusCode.NotAcceptable,
    Conflict_409 = HttpStatusCode.Conflict,
    InternalServerError_500 = HttpStatusCode.InternalServerError,
    NotImplemented_501 = HttpStatusCode.NotImplemented,
    ServiceUnavailable_503 = HttpStatusCode.ServiceUnavailable,
}
