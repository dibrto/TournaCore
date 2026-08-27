namespace TournaCore.API.Models.DTOs.Common {
    public class ServerErrorResponse : ErrorResponse {
        public string? StackTrace { get; set; }
    }
}
