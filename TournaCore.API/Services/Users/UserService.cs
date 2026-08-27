using Microsoft.EntityFrameworkCore;
using TournaCore.API.Common;
using TournaCore.API.Data;
using TournaCore.API.Exceptions;
using TournaCore.API.Models.DTOs.User;

namespace TournaCore.API.Services.Users {
    public class UserService(TournaCoreDbContext db) : IUserService {
        public async Task ChangeUserRole(Guid id, ChangeUserRoleRequest req) {
            var user = await db.sys_Users
                .SingleOrDefaultAsync(u => u.ID == id);

            if (user is null) 
                throw new AppException(ErrorCodes.UserNotFound);

            var role = await db.sys_Roles
                .SingleOrDefaultAsync(r => r.ID == req.Role_ID);

            if (role is null)
                throw new AppException(ErrorCodes.UserRoleNotFound);

            user.Role_ID = role.ID;
            await db.SaveChangesAsync();
        }
    }
}
