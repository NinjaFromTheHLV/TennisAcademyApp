using System.Runtime.CompilerServices;
using TennisAcademyApp.ViewModels.Reservation;

namespace TennisAcademyApp.Services.Core.Contracts
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationIndexViewModel>?> GetUserReservationsAsync(string userId);
        Task<bool> CreateReservationAsync(string userId, ReservationCreateInputModel model);
        Task<bool> AutoReservationDeleteAsync();
        Task<ReservationDetailsViewModel> GetUserReservationDetailsAsync(string userId, int? id);
        Task<ReservationDeleteViewModel> GetUserReservationForDeletingAsync(string userId, int? id);
        Task<bool> DeleteReservationAsync(string userId, ReservationDeleteViewModel model);
        Task<IEnumerable<ReservationHistoryViewModel>?> GetUserReservationHistoryAsync(string userId);
        Task IsCoachAvailableAtTheTimeAsync(ReservationCreateInputModel model);
        Task IsDateValidAsync(ReservationCreateInputModel model);
    }
}
