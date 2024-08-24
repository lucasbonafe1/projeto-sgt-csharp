using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SGT.Core.Exceptions;
using System.Net;

namespace SGT.Infrastructure.Middlewares
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            if (exception == null)
            {
                return false;
            }

            ProblemDetails problemDetails = new ProblemDetails
            {
                Title = "An error occurred",
                Detail = exception.Message,
                Status = (int) HttpStatusCode.InternalServerError, 
                Type = exception.GetType().Name 
            };

            switch (exception)
            {
                case BadRequestException:
                    logger.LogError("Bad Request: {message}", exception.Message);
                    problemDetails.Status = (int) HttpStatusCode.BadRequest;
                    httpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    break;

                case NotFoundException:
                    logger.LogError("Not Found: {message}", exception.Message);
                    problemDetails.Status = (int) HttpStatusCode.NotFound;
                    httpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
                    break;
            }
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
