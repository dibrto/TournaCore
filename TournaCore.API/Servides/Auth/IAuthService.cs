using TournaCore.API.Models;

namespace TournaCore.API.Servides.Auth {
    public interface IAuthService {
        public Task<RegisterResponse?> Register(RegisterRequest req);
    }
}
