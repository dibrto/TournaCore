using Microsoft.EntityFrameworkCore;
using TournaCore.API.Common;
using TournaCore.API.Data;
using TournaCore.API.Exceptions;
using TournaCore.API.Models.DTOs;

namespace TournaCore.API.Services.User {
    public class UserService(TournaCoreDbContext db) : IUserService {
        public async Task ChangeUserRole(Guid id, ChangeUserRoleRequest req) {
            var user = await db.sys_Users
                .SingleOrDefaultAsync(u => u.ID == id);

            if (user is null) {
                throw new AppException(404, ErrorCodes.UserNotFound, "User doesn't exist.");
            }

            var role = await db.sys_Roles
                .SingleOrDefaultAsync(r => r.ID == req.Role_ID);

            if (role is null) {
                throw new AppException(404, ErrorCodes.UserRoleNotFound, "User role doesn't exist.");
            }

            user.Role_ID = role.ID;
            await db.SaveChangesAsync();
        }
    }
}
