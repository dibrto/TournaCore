namespace TournaCore.API.Models.DTOs.Common {
    public class ValidationErrorResponse : ErrorResponse {
        public Dictionary<string, string[]> Errors { get; set; } = [];
    }
}
