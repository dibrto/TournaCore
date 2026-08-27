using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TournaCore.API.Common;
using TournaCore.API.Common.Swagger;
using TournaCore.API.Models.DTOs.User;
using TournaCore.API.Services.Users;

namespace TournaCore.API.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController(IUserService service) : ControllerBase {
        [Authorize(Roles = "Admin")]
        [HttpPatch("{id:guid}/role")]
        [ProducesAppError(nameof(ErrorCodes.UserNotFound))]
        [ProducesAppError(nameof(ErrorCodes.UserRoleNotFound))]
        public async Task<NoContent> PatchRole(Guid id, ChangeUserRoleRequest req) {
            await service.ChangeUserRole(id, req);

            return TypedResults.NoContent();
        }
    }
}
