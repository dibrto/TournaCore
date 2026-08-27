namespace TournaCore.API.Models.DTOs.Auth {
    public class LoginResponse {
        public required string AccessToken { get; set; }
        public required UserResponse User { get; set; }
    }
}
