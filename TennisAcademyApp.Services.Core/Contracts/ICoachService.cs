using TennisAcademyApp.ViewModels.Coach;

namespace TennisAcademyApp.Services.Core.Contracts
{
    public interface ICoachService
    {
        Task <IEnumerable<AllCoachesViewModel>?> GetAllCoachesAsync(string? userId);
        Task <CoachDetailsViewModel> GetCoachDetailsAsync(string? userId, Guid? id);
        Task<bool> AddCoachAsync(string userId, AddCoachInputModel inputModel);
        Task<CoachEditViewModel?> GetCoachForEdittingAsync(Guid? id, string? userId);
    }
}
