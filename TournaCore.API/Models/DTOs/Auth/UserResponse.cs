namespace TournaCore.API.Models.DTOs.Auth {
    public class UserResponse {
        public required Guid ID { get; set; }
        public required string Email { get; set; }
    }
}
