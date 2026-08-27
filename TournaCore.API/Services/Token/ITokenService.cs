using TournaCore.API.Models.Entities;

namespace TournaCore.API.Services.Token {
    public interface ITokenService {
        string GenerateToken(User user, string roleName);
    }
}
