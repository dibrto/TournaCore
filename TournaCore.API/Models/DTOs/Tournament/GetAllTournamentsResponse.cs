namespace TournaCore.API.Models.DTOs.Tournament {
    public class GetAllTournamentsResponse {
        public Guid ID { get; set; }
        public string Name { get; set; } = null!;
        public int StateID { get; set; }
        public string StateName { get; set; } = null!;
        public DateTime StartDate { get; set; }
    }
}
