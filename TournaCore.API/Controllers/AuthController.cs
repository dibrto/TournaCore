using Microsoft.AspNetCore.Mvc;
using TournaCore.API.Models;
using TournaCore.API.Servides.Auth;

namespace TournaCore.API.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase {
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        [ProducesResponseType(typeof(void), 422, Description = "Validation error")]
        [ProducesResponseType(typeof(void), 401, Description = "Invalid credentials")]       
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest req) {
            var res = await service.Login(req);

            if (res.Error is not null)
                return Unauthorized(res.Error);

            return Ok(res.Data);
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterResponse), 200)]
        [ProducesResponseType(typeof(void), 422, Description = "Validation error")]
        [ProducesResponseType(typeof(void), 409, Description = "Email already exists")]
        public async Task<ActionResult<RegisterResponse>> Register (RegisterRequest req) {
            var res = await service.Register(req);

            if (res.Error is not null)
                return Conflict(res.Error);

            return Ok(res.Data);
        }
    }
}
