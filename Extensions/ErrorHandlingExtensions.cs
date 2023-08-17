using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace Valkyrie.Extensions;

public static class ErrorHandlingExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            var exceptionDetails = context.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionDetails?.Error;

            await Results.Problem(
                title: $"There is an internal error: {exception?.Message}",
                statusCode: StatusCodes.Status500InternalServerError,
                extensions: new Dictionary<string, object?>
                {
                    { "traceID", Activity.Current?.Id }
                }
                ).ExecuteAsync(context);
        });
    }
}
