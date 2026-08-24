using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TournaCore.API.Data;
using TournaCore.API.Models;

namespace TournaCore.API.Servides.Auth {
    public class AuthService(TournaCoreDbContext db) : IAuthService {
        public async Task<RegisterResponse?> Register(RegisterRequest req) {
            var exists = await db.sys_Users
                .AnyAsync(x => x.Email == req.Email);

            if (exists) {
                // email already exists
                return null;
            }

            var now = DateTime.Now;
            var user = new User {
                ID = Guid.NewGuid(),
                Email = req.Email,                

                CD = now,
                CU = "system",

                LD = now,
                LU = "system"
            };
            var passwordHasher = new PasswordHasher<User>();
            user.PassHash = passwordHasher.HashPassword(user, req.Password);

            db.sys_Users.Add(user);
            await db.SaveChangesAsync();

            return new RegisterResponse { Token = "111" }; //temp
        }
    }
}
