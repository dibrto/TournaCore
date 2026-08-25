using Microsoft.AspNetCore.Mvc;
using TournaCore.API.Models;
using TournaCore.API.Servides.Auth;

namespace TournaCore.API.Controllers {
    [Route("api/v1/login")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase {
        [HttpPost]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest, Description = "Validation error")]
        [ProducesResponseType(
            typeof(ErrorResponse),
            StatusCodes.Status409Conflict,
            Description = """
                Conflict

                | Error code | Description |
                |------------|-------------|
                | 1101 | Email already exists |
                """
        )]
        public async Task<ActionResult<RegisterResponse>> Post(RegisterRequest req) {
            var res = await service.Register(req);

            if (res.Error is not null)
                return Conflict(res.Error);

            return StatusCode(201, res.Data);
        }
    }
}
