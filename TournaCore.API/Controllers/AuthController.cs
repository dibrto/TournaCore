using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TournaCore.API.Models;
using TournaCore.API.Servides.Auth;

namespace TournaCore.API.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase {
        [HttpPost("login")]
        [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity, Description = "Validation error")]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized, Description = "Invalid credentials")]       
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest req) {
            var res = await service.Login(req);

            if (res.Error is not null)
                return Unauthorized(res.Error);

            return Ok(res.Data);
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity, Description = "Validation error")]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict, Description = "Email already exists")]
        public async Task<Results<Ok<RegisterResponse>, Conflict<ErrorResponse>>> Register (RegisterRequest req) {
            var res = await service.Register(req);

            if (res.Error is not null)
                return TypedResults.Conflict(res.Error);

            return TypedResults.Ok(res.Data);
        }
    }
}
