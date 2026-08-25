using TournaCore.API.Models.DTOs;

namespace TournaCore.API.Servides.Auth {
    public interface IAuthService {
        public Task<Response<LoginResponse>> Login(LoginRequest req);
        public Task<Response<RegisterResponse>> Register(RegisterRequest req);
    }
}
