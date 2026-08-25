using TournaCore.API.Models;

namespace TournaCore.API.Servides.Token {
    public interface ITokenService {
        string GenerateToken(User user);
    }
}
