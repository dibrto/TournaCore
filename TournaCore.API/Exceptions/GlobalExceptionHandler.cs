using Microsoft.AspNetCore.Diagnostics;
using TournaCore.API.Common;
using TournaCore.API.Models.DTOs;

namespace TournaCore.API.Exceptions {
    public class GlobalExceptionHandler : IExceptionHandler {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {
            if (exception is AppException appException) {

                httpContext.Response.StatusCode = appException.Error.HttpCode;

                await httpContext.Response.WriteAsJsonAsync(
                    new ErrorResponse {
                        ErrorCode = appException.Error.ErrorCode,
                        ErrorMessage = appException.Error.ErrorMessage
                    },
                    cancellationToken
                );
                return true;
            }

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(
                new ServerErrorResponse {
                    ErrorCode = ErrorCodes.InternalServerError,
                    ErrorMessage = exception.Message,
                    StackTrace = exception.StackTrace
                },
                cancellationToken);

            return true;
        }
    }
}
