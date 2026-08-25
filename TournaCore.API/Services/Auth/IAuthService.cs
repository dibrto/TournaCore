using TournaCore.API.Models;

namespace TournaCore.API.Servides.Auth {
    public interface IAuthService {
        public Task<Response<LoginResponse>> Login(LoginRequest req);
        public Task<Response<RegisterResponse>> Register(RegisterRequest req);
    }
}
