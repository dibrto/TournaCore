using TournaCore.API.Models;

namespace TournaCore.API.Servides.Auth {
    public interface IAuthService {
        public Task<Response<RegisterResponse>> Register(RegisterRequest req);
    }
}
