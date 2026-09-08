using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TournaCore.API.Models.DTOs.Tournament {
    public class TournamentRequest {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
