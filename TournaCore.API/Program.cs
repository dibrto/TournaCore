
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournaCore.API.Common;
using TournaCore.API.Data;
using TournaCore.API.Models;
using TournaCore.API.Servides.Auth;
using TournaCore.API.Servides.Token;

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


            // Add services to the container
            builder.Services
                .AddControllers(options => options.Filters.Add(new ProducesAttribute("application/json")))
                // validation return type conf
                .ConfigureApiBehaviorOptions(options => {
                     options.InvalidModelStateResponseFactory = context => {
                         var errors = context.ModelState
                             .Where(x => x.Value?.Errors.Count > 0)
                             .ToDictionary(
                                 x => x.Key,
                                 x => x.Value!.Errors
                                     .Select(e => e.ErrorMessage)
                                     .ToArray()
                             );

                         var response = new ValidationErrorResponse {
                             ErrorCode = ErrorCodes.ValidationError,
                             ErrorMessage = "Validation failed",
                             Errors = errors
                         };

                         return new BadRequestObjectResult(response);
                     };
                 });
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();

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
