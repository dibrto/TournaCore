using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TournaCore.API.Common;
using TournaCore.API.Common.Swagger;
using TournaCore.API.Models.DTOs.Auth;
using TournaCore.API.Services.Auth;

namespace TournaCore.API.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase {
        [HttpPost("login")]
        [ProducesAppError(nameof(ErrorCodes.InvalidCredentials))]
        [ProducesAppError(nameof(ErrorCodes.ValidationError))]
        public async Task<Ok<LoginResponse>> Login(LoginRequest req) {
            var res = await service.Login(req);

            return TypedResults.Ok(res);
        }

        [HttpPost("register")]
        [ProducesAppError(nameof(ErrorCodes.EmailAlreadyExists))]
        [ProducesAppError(nameof(ErrorCodes.ValidationError))]
        public async Task<Ok<RegisterResponse>> Register (RegisterRequest req) {
            var res = await service.Register(req);          

            return TypedResults.Ok(res);
        }
    }
}
