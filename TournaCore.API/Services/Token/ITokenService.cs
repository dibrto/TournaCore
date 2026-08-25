using TournaCore.API.Models.Entity;

namespace TournaCore.API.Servides.Token {
    public interface ITokenService {
        string GenerateToken(User user);
    }
}
