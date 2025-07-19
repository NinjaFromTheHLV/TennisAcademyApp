using TennisAcademyApp.ViewModels.Coach;

namespace TennisAcademyApp.Services.Core.Contracts
{
    public interface ICoachService
    {
        Task <IEnumerable<AllCoachesViewModel>?> GetAllCoachesAsync();
        Task <CoachDetailsViewModel> GetCoachDetailsAsync(string? userId, int? id);
        Task<bool> AddCoachAsync(string userId, AddCoachInputModel inputModel);
        Task<CoachEditViewModel?> GetCoachForEdittingAsync(int? id, string? userId);
        Task<bool> EdittedCoachAsync(string userId, CoachEditViewModel model);
        Task<DeleteCoachViewModel?> GetCoachForDeletingAsync(int id);
        Task DeletedCoachAsync(int id, string userId);
        //Task<IEnumerable<FavouriteCoachViewModel>> GetFavouritesAsync(string? userId);
    }
}
