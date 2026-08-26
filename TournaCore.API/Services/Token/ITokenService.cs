using TournaCore.API.Models.Entities;

namespace TournaCore.API.Servides.Token {
    public interface ITokenService {
        string GenerateToken(User user);
    }
}
