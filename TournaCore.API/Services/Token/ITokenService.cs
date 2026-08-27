using TournaCore.API.Models.Entities;

namespace TournaCore.API.Services.Token {
    public interface ITokenService {
        string GenerateToken(Guid id, string email, string roleName);
    }
}
