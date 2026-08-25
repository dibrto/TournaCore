using System.ComponentModel;

namespace TournaCore.API.Models.DTOs {
    public class Response<T> {
        public T? Data { get; set; }
        public ErrorResponse? Error { get; set; }
    }

    public class ErrorResponse {
        [DefaultValue(0)]
        public required int ErrorCode { get; set; }

        public required string ErrorMessage { get; set; }
    }

    public class ValidationErrorResponse : ErrorResponse {
        public Dictionary<string, string[]> Errors { get; set; } = [];
    }

}
