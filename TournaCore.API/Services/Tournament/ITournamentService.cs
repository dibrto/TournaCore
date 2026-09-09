
using TournaCore.API.Models.DTOs.Tournament;

namespace TournaCore.API.Services.Tournament {
    public interface ITournamentService {
        Task<List<GetAllTournamentsResponse>> GetAll();
        Task<GetTournamentResponse> GetById(Guid id);
        Task<CreateTournamentResponse> Create(TournamentRequest req, string email, Guid userId);
        Task Put(Guid id, TournamentRequest req, string email, Guid userId);
    }
}
