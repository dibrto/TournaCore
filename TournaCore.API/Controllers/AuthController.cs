using Microsoft.AspNetCore.Mvc;
using TournaCore.API.Models;
using TournaCore.API.Servides.Auth;

namespace TournaCore.API.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase {
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest, Description = "Validation error")]
        [ProducesResponseType(
            typeof(ErrorResponse),
            StatusCodes.Status401Unauthorized,
            Description = """
                Unauthorized

                | Error code | Description |
                |------------|-------------|
                | 1101 | Invalid credentials |
                """
        )]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest req) {
            var res = await service.Login(req);

            if (res.Error is not null)
                return Unauthorized(res.Error);

            return Ok(res.Data);
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest, Description = "Validation error")]
        [ProducesResponseType(
            typeof(ErrorResponse),
            StatusCodes.Status409Conflict,
            Description = """
                Conflict

                | Error code | Description |
                |------------|-------------|
                | 1102 | Email already exists |
                """
        )]
        public async Task<ActionResult<RegisterResponse>> Register (RegisterRequest req) {
            var res = await service.Register(req);

            if (res.Error is not null)
                return Conflict(res.Error);

            return StatusCode(201, res.Data);
        }        
    }
}
