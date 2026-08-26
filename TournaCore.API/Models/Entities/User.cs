namespace TournaCore.API.Models.Entities {
    public class User {
        public Guid ID { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PassHash { get; set; } = string.Empty;
        public DateTime CD { get; set; }
        public string CU { get; set; } = string.Empty;
        public DateTime LD { get; set; } 
        public string LU { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public Guid Role_ID { get; set; }
    }
}
