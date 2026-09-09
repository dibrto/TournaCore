using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TournaCore.API.Common;
using TournaCore.API.Common.Swagger;
using TournaCore.API.Models.DTOs.Tournament;
using TournaCore.API.Services.Tournament;

namespace TournaCore.API.Controllers {
    [Route("api/v1/tournaments")]
    [ApiController]
    public class TournamentController(ITournamentService service) : ControllerBase {

        [Authorize]
        [HttpGet]
        public async Task<Ok<List<GetAllTournamentsResponse>>> GetAll() {
            var res = await service.GetAll();

            return TypedResults.Ok(res);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        [ProducesAppError(nameof(ErrorCodes.TournamentNotFound))]
        public async Task<Ok<GetTournamentResponse>> GetById(Guid id) {
            var res = await service.GetById(id);

            return TypedResults.Ok(res);
        }

        [Authorize(Roles = "Admin,Organizer")]
        [HttpPost]
        [ProducesAppError(nameof(ErrorCodes.ValidationError))]
        public async Task<Created<CreateTournamentResponse>> Post(TournamentRequest req) {
            var res = await service.Create(req, User.GetEmail(), User.GetUserId());

            return TypedResults.Created($"/tournaments/{res.ID}", res);
        }

        [Authorize(Roles = "Admin,Organizer")]
        [HttpPut("{id:guid}")]
        [ProducesAppError(nameof(ErrorCodes.ValidationError))]
        [ProducesAppError(nameof(ErrorCodes.NotTournamentOwner))]
        public async Task<NoContent> Put(Guid id, TournamentRequest req) {
            await service.Put(id, req, User.GetEmail(), User.GetUserId());

            return TypedResults.NoContent();
        }
    }
}
