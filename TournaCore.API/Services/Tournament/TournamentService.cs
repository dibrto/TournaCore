using Microsoft.EntityFrameworkCore;
using TournaCore.API.Common;
using TournaCore.API.Data;
using TournaCore.API.Exceptions;
using TournaCore.API.Models.DTOs.Tournament;
using TournaCore.API.Models.DTOs.User;
using TournaCore.API.Models.Entities;
using TournaCore.API.Services.Tournament;

namespace TournaCore.API.Services.Users {
    public class TournamentService(TournaCoreDbContext db) : ITournamentService {
        public async Task<CreateTournamentResponse> Create(CreateTournamentRequest req, string email) {
            var now = DateTime.Now;
            var tournament = new trm_Tournament {
                ID = Guid.NewGuid(),
                Name = req.Name,
                StartDate = req.StartDate,
                EndDate = req.EndDate,

                CD = now,
                CU = email,
                LD = now,
                LU = email
            };
            db.trm_Tournaments.Add(tournament);
            await db.SaveChangesAsync();

            return new CreateTournamentResponse {
                ID = tournament.ID,
                Name = tournament.Name,
                StartDate = tournament.StartDate,
                EndDate = tournament.EndDate,
            };
        }
    }
}
