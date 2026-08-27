using TournaCore.API.Models.DTOs.Auth;

namespace TournaCore.API.Services.Auth {
    public interface IAuthService {
        public Task<LoginResponse> Login(LoginRequest req);
        public Task<RegisterResponse> Register(RegisterRequest req);
    }
}
