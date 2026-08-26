using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TournaCore.API.Common.Swagger {
    public class AppErrorOperationFilter : IOperationFilter {
        public void Apply(OpenApiOperation operation, OperationFilterContext context) {
            operation.Responses ??= [];

            var errors = context.MethodInfo
                .GetCustomAttributes(typeof(ProducesAppErrorAttribute), true)
                .Cast<ProducesAppErrorAttribute>();

            foreach (var errorAttribute in errors) {
                var field = typeof(ErrorCodes)
                    .GetField(errorAttribute.ErrorName);

                if (field?.GetValue(null) is not AppError error)
                    continue;

                var statusCode = error.HttpCode.ToString();

                if (!operation.Responses.TryGetValue(statusCode, out var response)) {
                    response = new OpenApiResponse();
                    operation.Responses[statusCode] = response;
                }

                var errorDescription = $"{error.ErrorCode} - {error.ErrorMessage}";

                if (string.IsNullOrEmpty(response.Description))
                    response.Description = $"Error codes: {errorDescription}";
                else 
                    response.Description += $"<br> {errorDescription}";
            }
        }
    }
}
