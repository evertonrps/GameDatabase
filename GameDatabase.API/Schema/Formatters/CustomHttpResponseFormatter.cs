using System.Net;
using HotChocolate.AspNetCore.Serialization;
using HotChocolate.Execution;

namespace GameDatabase.API.Schema.Formatters;

public class CustomHttpResponseFormatter : DefaultHttpResponseFormatter
{
    protected override HttpStatusCode OnDetermineStatusCode(
        IQueryResult result, FormatInfo format,
        HttpStatusCode? proposedStatusCode)
    {
        if (result.Errors?.Count > 0 &&
            result.Errors.Any(error => error.Code == "SOME_AUTH_ISSUE"))
        {
            return HttpStatusCode.Forbidden;
        }

        // In all other cases let Hot Chocolate figure out the
        // appropriate status code.
        return base.OnDetermineStatusCode(result, format, proposedStatusCode);
    }
}
