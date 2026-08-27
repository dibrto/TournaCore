using TournaCore.API.Models.DTOs.User;

namespace TournaCore.API.Services.Users {
    public interface IUserService {
        Task ChangeUserRole(Guid id, ChangeUserRoleRequest req);
    }
}
