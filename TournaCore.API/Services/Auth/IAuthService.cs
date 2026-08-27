using TournaCore.API.Models.DTOs;

namespace TournaCore.API.Services.Auth {
    public interface IAuthService {
        public Task<Response<LoginResponse>> Login(LoginRequest req);
        public Task<Response<RegisterResponse>> Register(RegisterRequest req);
    }
}
