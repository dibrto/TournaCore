using Microsoft.EntityFrameworkCore;

namespace TournaCore.API.Data {
    public class TournaCoreDbContext : DbContext {
        public TournaCoreDbContext(
            DbContextOptions<TournaCoreDbContext> options)
            : base(options) {
        }
    }
}
