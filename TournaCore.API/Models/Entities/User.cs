using System.ComponentModel.DataAnnotations;

namespace TournaCore.API.Models.Entity {
    public class User {
        public Guid ID { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PassHash { get; set; } = string.Empty;
        public DateTime CD { get; set; }
        public string CU { get; set; } = string.Empty;
        public DateTime LD { get; set; } 
        public string LU { get; set; } = string.Empty;
    }    
}
