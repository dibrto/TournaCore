using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TournaCore.API.Models;
using TournaCore.API.Servides.Auth;

namespace TournaCore.API.Controllers {
    [Route("api/v1/login")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase {
        [HttpPost]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult<RegisterResponse>> Post(RegisterRequest req) {
            var res = await service.Register(req);

            if (res is null)
                return Conflict();

            return StatusCode(201, res);
        }
    }
}
