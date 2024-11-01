using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

internal class BanProblemDetails : ProblemDetails
{
    public BanProblemDetails(string detail)
    {
        Title = "Authorization error";
        Detail = detail;
        Status = StatusCodes.Status403Forbidden;
        Type = "https://example.com/probs/authorization";
    }
}

