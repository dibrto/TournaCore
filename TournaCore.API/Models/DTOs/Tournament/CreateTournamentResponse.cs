namespace TournaCore.API.Models.DTOs.Tournament {
    public class CreateTournamentResponse {
        public Guid ID { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
