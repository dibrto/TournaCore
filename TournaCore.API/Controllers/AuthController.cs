using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TournaCore.API.Models.DTOs;
using TournaCore.API.Servides.Auth;

namespace TournaCore.API.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase {
        [HttpPost("login")]
        [ProducesResponseType(typeof(void), 401, Description = "Error codes: 1101 - Invalid credentials")]
        [ProducesResponseType(typeof(void), 422, Description = "Validation error")]
        public async Task<Ok<LoginResponse>> Login(LoginRequest req) {
            var res = await service.Login(req);

            return TypedResults.Ok(res.Data);
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(void), 409, Description = "Error codes: 1102 - Email alreday exists")]
        [ProducesResponseType(typeof(void), 422, Description = "Validation error")]
        public async Task<Ok<RegisterResponse>> Register (RegisterRequest req) {
            var res = await service.Register(req);          

            return TypedResults.Ok(res.Data);
        }
    }
}
