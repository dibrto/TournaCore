using System.ComponentModel.DataAnnotations;

namespace TournaCore.API.Models.DTOs.Auth {
    public class LoginRequest {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;
    }
}
