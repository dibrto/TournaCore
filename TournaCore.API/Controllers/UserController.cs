using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TournaCore.API.Models.DTOs;
using TournaCore.API.Services.User;

namespace TournaCore.API.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController(IUserService service) : ControllerBase {
        [Authorize(Roles = "Admin")]
        [HttpPatch("{id:guid}/role")]
        [ProducesResponseType(typeof(void), 404, Description = "Error codes: 1103, 1104")]
        public async Task<NoContent> PatchRole(Guid id, ChangeUserRoleRequest req) {
            await service.ChangeUserRole(id, req);

            return TypedResults.NoContent();
        }
    }
}
