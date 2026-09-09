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
        public async Task<List<GetAllTournamentsResponse>> GetAll() {
            return await db.trm_Tournaments
                .Select(tur => new GetAllTournamentsResponse {
                    ID = tur.ID,
                    Name = tur.Name,
                    StartDate = tur.StartDate
                })
                .ToListAsync();
        }

        public async Task<GetTournamentResponse> GetById(Guid id) {
            var tournament = await db.trm_Tournaments
                .Select(tur => new GetTournamentResponse {
                    ID = tur.ID,
                    Name = tur.Name,
                    StartDate = tur.StartDate,
                    EndDate = tur.EndDate,
                    CreatedBy = tur.CU,
                    CreatedAt = tur.CD,
                    UpdatedBy = tur.LU,
                    UpdatedAt = tur.LD,
                })
                .SingleOrDefaultAsync(trm => trm.ID == id);

            if (tournament is null)
                throw new AppException(ErrorCodes.TournamentNotFound);

            return tournament;
        }

        public async Task<CreateTournamentResponse> Create(TournamentRequest req, string email, Guid userId) {
            var now = DateTime.Now;
            var tournament = new trm_Tournament {
                ID = Guid.NewGuid(),
                Name = req.Name,
                StartDate = req.StartDate,
                EndDate = req.EndDate,
                Owner_ID = userId,

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
        public async Task Put(Guid id, TournamentRequest req, string email) {
            var tournament = await db.trm_Tournaments.SingleOrDefaultAsync(tur => tur.ID == id);

            if (tournament is null)
                throw new AppException(ErrorCodes.TournamentNotFound);

            if (email != tournament.CU)
                throw new AppException(ErrorCodes.NotTournamentOwner);

            db.Entry(tournament).CurrentValues.SetValues(req);
            tournament.LU = email;
            tournament.LD = DateTime.Now;
            await db.SaveChangesAsync();
        }    
    }
}