using TennisAcademyApp.ViewModels.Coach;
using TennisAcademyApp.ViewModels.DropDown;

namespace TennisAcademyApp.Services.Core.Contracts
{
    public interface ICoachService
    {
        Task<IEnumerable<AllCoachesViewModel>?> GetAllCoachesAsync();
        Task<IEnumerable<CoachDropDownModel>> GetGoachesForDropDownAsync();
        Task <CoachDetailsViewModel> GetCoachDetailsAsync(string userId, int? id);
        Task<bool> AddCoachAsync(string userId, AddCoachInputModel inputModel);
        Task<CoachEditInputModel?> GetCoachForEdittingAsync(int? id, string? userId);
        Task<bool> EdittedCoachAsync(string userId, CoachEditInputModel model);
        Task<DeleteCoachViewModel?> GetCoachForDeletingAsync(string? userId,int? id);
        Task<bool> DeletedCoachAsync(string? userId, DeleteCoachViewModel model);
        Task<IEnumerable<FavouriteCoachViewModel>> GetFavouritesAsync(string? userId);
        Task<bool> AddFavouriteCoachAsync(string userId, int id);
        Task<bool> RemoveFromFavouritesAsync(string userId, int? id);
    }
}
