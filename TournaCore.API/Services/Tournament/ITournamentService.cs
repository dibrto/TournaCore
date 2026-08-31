
using TournaCore.API.Models.DTOs.Tournament;

namespace TournaCore.API.Services.Tournament {
    public interface ITournamentService {
        Task<CreateTournamentResponse> Create(CreateTournamentRequest req, string email);
    }
}
