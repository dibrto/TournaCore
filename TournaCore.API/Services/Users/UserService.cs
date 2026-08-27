using Microsoft.EntityFrameworkCore;
using TournaCore.API.Common;
using TournaCore.API.Data;
using TournaCore.API.Exceptions;
using TournaCore.API.Models.DTOs.User;

namespace TournaCore.API.Services.Users {
    public class UserService(TournaCoreDbContext db) : IUserService {
        public async Task ChangeUserRole(Guid id, ChangeUserRoleRequest req) {
            var user = await db.sys_Users
                .Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.ID == id);

            if (user is null) 
                throw new AppException(ErrorCodes.UserNotFound);

            if (user.Role.Name == "Admin")
                throw new AppException(ErrorCodes.CannotModifyAdmin);

            var role = await db.sys_Roles
                .SingleOrDefaultAsync(r => r.ID == req.Role_ID);

            if (role is null)
                throw new AppException(ErrorCodes.UserRoleNotFound);

            if (role.Name == "Admin")
                throw new AppException(ErrorCodes.CannotAssignAdminRole);

            user.Role_ID = role.ID;
            await db.SaveChangesAsync();
        }
    }
}
