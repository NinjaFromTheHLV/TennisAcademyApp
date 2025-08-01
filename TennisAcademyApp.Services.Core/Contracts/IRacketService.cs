using TennisAcademyApp.ViewModels.Racket;
namespace TennisAcademyApp.Services.Core.Contracts
{
    public interface IRacketService
    {
        Task<IEnumerable<RacketIndexViewModel>> GetAllRacketsAsync();
        Task<bool> AddRacketAsync(string userId, RacketCreateInputModel model);
        Task<RacketEditFormModel> GetRacketForEdittingAsync(string userId, int? id);
        Task<bool> EditRacketAsync(RacketEditFormModel model);
        Task<RacketDeleteViewModel> GetRacketForDeletingAsync(string userId, int? id);
        Task<bool> DeleteRacketAsync(string userId, RacketDeleteViewModel model);
    }
}
