using TennisAcademyApp.ViewModels.Reservation;

namespace TennisAcademyApp.Services.Core.Contracts
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationIndexViewModel>?> GetUserReservationsAsync(string userId);
        Task<bool> CreateReservationAsync(string userId, ReservationCreateInputModel model);
        //Task<ReservationDetailsViewModel> GetUserReservationDetailsAsync(string userId);
    }
}
