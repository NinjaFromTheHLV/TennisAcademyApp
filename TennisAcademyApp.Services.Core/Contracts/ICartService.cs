using TennisAcademyApp.ViewModels.Cart;

namespace TennisAcademyApp.Services.Core.Contracts
{
    public interface ICartService
    {
        Task<IEnumerable<RacketCartViewModel>> GetAllRacketsInCartAsync(string userId); 
        Task AddRacketToCartAsync(string userId, int racketId, int quantity);
        Task<bool> RemoveRacketFromCartAsync(string userId, int id, int racketId);
    }
}
