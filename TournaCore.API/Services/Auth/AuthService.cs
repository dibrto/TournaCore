using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TournaCore.API.Common;
using TournaCore.API.Data;
using TournaCore.API.Models;
using TournaCore.API.Servides.Token;

namespace TournaCore.API.Servides.Auth {
    public class AuthService(TournaCoreDbContext db, ITokenService tokenService) : IAuthService {
        public async Task<Response<RegisterResponse>> Register(RegisterRequest req) {
            var exists = await db.sys_Users
                .AnyAsync(x => x.Email == req.Email);

            if (exists) {
                return new Response<RegisterResponse> {
                   Error = new ErrorResponse { 
                        ErrorCode = ErrorCodes.EmailAlreadyExists,
                        ErrorMessage = "Email already exists"
                    }
                };
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

            return new Response<RegisterResponse> {
                Data = new RegisterResponse {
                    Token = tokenService.GenerateToken(user)
                }
            };
        }
    }
}
