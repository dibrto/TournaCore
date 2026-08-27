using System.ComponentModel;

namespace TournaCore.API.Models.DTOs.Common {
    public class ErrorResponse {
        [DefaultValue(0)]
        public required int ErrorCode { get; set; }
        public required string ErrorMessage { get; set; }
    }
}
