using System.ComponentModel.DataAnnotations;
using TournaCore.API.Models.Entities;

namespace TournaCore.API.Models.DTOs {
    public class LoginRequest {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponse {
        public required string AccessToken { get; set; }
        public required UserResponse User { get; set; }
    }

    public class RegisterRequest {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterResponse {
        public required string AccessToken { get; set; }
        public required UserResponse User { get; set; }
    }

    public class UserResponse {
        public required Guid ID { get; set; }
        public required string Email { get; set; }
    }
}
