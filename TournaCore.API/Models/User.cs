using System.ComponentModel.DataAnnotations;

namespace TournaCore.API.Models {
    public class User {
        public Guid ID { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PassHash { get; set; } = string.Empty;
        public DateTime CD { get; set; }
        public string CU { get; set; } = string.Empty;
        public DateTime LD { get; set; } 
        public string LU { get; set; } = string.Empty;
    }
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
        public string Token { get; set; } = string.Empty;
    }

    public class RegisterRequest {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterResponse {
        public string Token { get; set; } = string.Empty;
    }
}
