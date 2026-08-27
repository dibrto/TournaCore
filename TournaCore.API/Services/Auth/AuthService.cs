using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TournaCore.API.Common;
using TournaCore.API.Data;
using TournaCore.API.Exceptions;
using TournaCore.API.Models.DTOs;
using TournaCore.API.Models.Entities;
using TournaCore.API.Services.Token;

namespace TournaCore.API.Services.Auth {
    public class AuthService(TournaCoreDbContext db, ITokenService tokenService) : IAuthService {
        public async Task<LoginResponse> Login(LoginRequest req) {
            var user = await db.v_sys_Users.SingleOrDefaultAsync(u => u.Email == req.Email);

            // check user exists
            if (user is null)
                throw new AppException(ErrorCodes.InvalidCredentials);
            
            // check passsword
            var passwordHasher = new PasswordHasher<v_sys_User>();
            var verify = passwordHasher.VerifyHashedPassword(user, user.PassHash, req.Password);
            if (verify == PasswordVerificationResult.Failed)
               throw new AppException(ErrorCodes.InvalidCredentials);

            return new LoginResponse {
                AccessToken = tokenService.GenerateToken(user.ID, user.Email, user.RoleName),
                User = new UserResponse { 
                    ID = user.ID,
                    Email  = user.Email
                }                
            };
        }

        public async Task<RegisterResponse> Register(RegisterRequest req) {
            var exists = await db.sys_Users
                .AnyAsync(x => x.Email == req.Email);

            // check user exists
            if (exists)
                throw new AppException(ErrorCodes.EmailAlreadyExists);

            // prepare data
            var role = await db.sys_Roles
                .FirstAsync(r => r.Name == "Player");

            var now = DateTime.Now;

            // make user
            var user = new sys_User {
                ID = Guid.NewGuid(),
                Email = req.Email,
                Username = req.Username,
                Role_ID = role.ID,
                
                CD = now,
                CU = "system",
                LD = now,
                LU = "system"
            };
            var passwordHasher = new PasswordHasher<sys_User>();
            user.PassHash = passwordHasher.HashPassword(user, req.Password);

            db.sys_Users.Add(user);
            await db.SaveChangesAsync();

            return new RegisterResponse {
                AccessToken = tokenService.GenerateToken(user.ID, user.Email, role.Name),
                User = new UserResponse {
                    ID = user.ID,
                    Email = user.Email
                }
            };
        }
    }
}
