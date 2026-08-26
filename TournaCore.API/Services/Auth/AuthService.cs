using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TournaCore.API.Common;
using TournaCore.API.Data;
using TournaCore.API.Models.DTOs;
using TournaCore.API.Models.Entities;
using TournaCore.API.Servides.Token;

namespace TournaCore.API.Servides.Auth {
    public class AuthService(TournaCoreDbContext db, ITokenService tokenService) : IAuthService {
        public async Task<Response<LoginResponse>> Login(LoginRequest req) {
            var user = await db.Database
                .SqlQuery<LoginQuery>($"""
                    SELECT      u.*
                                , r.Name        AS RoleName
                    FROM        sys_Users u
                    JOIN        sys_Roles r
                    ON          r.ID = u.Role_ID
                    WHERE       u.Email = {req.Email}
                """)
                .SingleOrDefaultAsync();

            if (user == null) {
                return new Response<LoginResponse> {
                    Error = new ErrorResponse {
                        ErrorCode = ErrorCodes.InvalidCredentials,
                        ErrorMessage = "Invalid credentials"
                    }
                };
            }

            var passwordHasher = new PasswordHasher<User>();
            var verify = passwordHasher.VerifyHashedPassword(user, user.PassHash, req.Password);
            if (verify == PasswordVerificationResult.Failed) {
                return new Response<LoginResponse> {
                    Error = new ErrorResponse {
                        ErrorCode = ErrorCodes.InvalidCredentials,
                        ErrorMessage = "Invalid credentials"
                    }
                };
            }

            return new Response<LoginResponse> {
                Data = new LoginResponse {
                    AccessToken = tokenService.GenerateToken(user, user.RoleName),
                    User = new UserResponse { 
                        ID = user.ID,
                        Email  = user.Email
                    }
                }
            };
        }

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

            // get role
            Role role = await db.sys_Roles
                .FirstAsync(r => r.Name == "Player");

            var now = DateTime.Now;
            var user = new User {
                ID = Guid.NewGuid(),
                Email = req.Email,
                Username = req.Username,
                Role_ID = role.ID,
                
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
                    AccessToken = tokenService.GenerateToken(user, role.Name),
                    User = new UserResponse {
                        ID = user.ID,
                        Email = user.Email
                    }
                }
            };
        }
    }
}
