using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TournaCore.API.Models.DTOs.Tournament {
    public class GetTournamentResponse {
        public Guid ID { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CreatedBy { get; set; } = null!; // CU
        public DateTime CreatedAt { get; set; } // CD
        public string UpdatedBy { get; set; } = null!; // LU
        public DateTime UpdatedAt { get; set; } // LD
    }
}
