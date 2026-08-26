using TournaCore.API.Models.DTOs;

namespace TournaCore.API.Services.User {
    public interface IUserService {
        Task ChangeUserRole(Guid id, ChangeUserRoleRequest req);
    }
}
