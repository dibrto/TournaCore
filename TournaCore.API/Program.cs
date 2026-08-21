
using Microsoft.EntityFrameworkCore;
using TournaCore.API.Data;

namespace TournaCore.API {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // db
            builder.Services.AddDbContext<TournaCoreDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );

            // loweer case routes
            builder.Services.AddRouting(options => {
                options.LowercaseUrls = true;
            });

            // Add services to the container.
            builder.Services.AddControllers();

            // api doc
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // health checks
            builder.Services
                .AddHealthChecks()
                .AddDbContextCheck<TournaCoreDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();
            app.MapHealthChecks("/health");

            app.Run();
        }
    }
}
