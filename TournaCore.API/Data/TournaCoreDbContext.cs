using Microsoft.EntityFrameworkCore;
using TournaCore.API.Models;

namespace TournaCore.API.Data {
    public class TournaCoreDbContext : DbContext {
        public TournaCoreDbContext(DbContextOptions<TournaCoreDbContext> options): base(options) {}
        public DbSet<User> sys_Users { get; set; }
    }
}
