using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisAcademyApp.Data;
using TennisAcademyApp.Data.Models;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Reservation;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.User;
using static TennisAcademyApp.GCommon.Validations.ValidationConstants.Reservation;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.Reservation;
using System.Runtime.CompilerServices;

namespace TennisAcademyApp.Services.Core
{
    public class ReservationService : IReservationService
    {
        private readonly TennisAcademyDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        public ReservationService(TennisAcademyDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<ReservationIndexViewModel>?> GetUserReservationsAsync(string userId)
        {
            var autoDelete = await AutoReservationDeleteAsync();
            var user = await userManager.FindByIdAsync(userId);

            var reservations = await dbContext.Reservations
                .AsNoTracking()
                .Include(r => r.Coach)
                .Include(r => r.Surface)
                .Include(r => r.TrainingType)
                .Where(r => r.PlayerId == userId && r.IsDeleted == false)
                .Select(r => new ReservationIndexViewModel
                {
                    ReservationId = r.Id,
                    CoachName = r.Coach.Name,
                    TrainingTypeName = r.TrainingType.Name,
                    Date = r.Date.ToString(DateFormat),
                })
                .ToListAsync();

            return reservations;
        }
        public async Task<bool> CreateReservationAsync(string userId, ReservationCreateInputModel model)
        {
            bool result = false;

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException(UserCannotBeNull);
            }

            var surface = await dbContext.Surfaces.FindAsync(model.SurfaceId);
            var trainingType = await dbContext.Trainings.FindAsync(model.TrainingTypeId);
            var coach = await dbContext.Coaches.FindAsync(model.CoachId);

            await IsCoachAvailableAtTheTimeAsync(model);
            await DateValidationAsync(model);

            if (model.Duration != 60 && model.Duration != 120)
            {
                throw new ArgumentException(DurationErrorMessage);
            }

            var newReservation = new Reservation()
            {
                PlayerId = userId,
                SurfaceId = model.SurfaceId,
                TrainingTypeId = model.TrainingTypeId,
                CoachId = model.CoachId,
                Date = model.Date,
                Duration = model.Duration,
                Note = model.Note
            };
            await dbContext.Reservations.AddAsync(newReservation);
            await dbContext.SaveChangesAsync();

            result = true;

            return result;
        }
        public async Task<bool> AutoReservationDeleteAsync()
        {
            bool result = false;
            var expiredReservations = await dbContext.Reservations
                .Where(r => r.Date <= DateTime.Now)
                .ToListAsync();

            if (expiredReservations.Any())
            {
                expiredReservations.ForEach(r => r.IsDeleted = true);
                await dbContext.SaveChangesAsync();

                return true;
            }
            return result;
        }

        public async Task<ReservationDetailsViewModel?> GetUserReservationDetailsAsync(string userId, int? id)
        {
            ReservationDetailsViewModel? details = null;
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException(UserCannotBeNull);
            }

            if (id.HasValue)
            {
                var reservationDetails = await dbContext.Reservations
                    .AsNoTracking()
                    .Include(r => r.Coach)
                    .Include(r => r.Surface)
                    .Include(r => r.TrainingType)
                    .FirstOrDefaultAsync(r => r.Id == id && r.PlayerId == userId);


                if (reservationDetails == null)
                {
                    throw new ArgumentException(ReservationNotFoundErrorMessage);
                }

                details = new ReservationDetailsViewModel
                {
                    Id = reservationDetails.Id,
                    ImageUrl = reservationDetails.Surface.ImageUrl,
                    CoachName = reservationDetails.Coach.Name,
                    SurfaceName = reservationDetails.Surface.Name,
                    TrainingTypeName = reservationDetails.TrainingType.Name,
                    Date = reservationDetails.Date,
                    Duration = reservationDetails.Duration,
                    Note = reservationDetails.Note
                };
            }
            return details;
        }

        public async Task<ReservationDeleteViewModel?> GetUserReservationForDeletingAsync(string userId, int? id)
        {
            ReservationDeleteViewModel? reservationToDelete = null;

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException(UserCannotBeNull);
            }

            if (id.HasValue)
            {
                var reservation = await dbContext.Reservations
                    .AsNoTracking()
                    .Include(r => r.Coach)
                    .Include(r => r.Surface)
                    .FirstOrDefaultAsync(r => r.Id == id && r.PlayerId == userId);
                if (reservation == null)
                {
                    throw new ArgumentException(ReservationNotFoundErrorMessage);
                }

                if (reservation.PlayerId == userId)
                {
                    reservationToDelete = new ReservationDeleteViewModel
                    {
                        Id = reservation.Id,
                        SurfaceName = reservation.Surface.Name,
                        Date = reservation.Date,
                        ImageUrl = reservation.Surface.ImageUrl
                    };
                }
            }
            return reservationToDelete;
        }

        public async Task<bool> DeleteReservationAsync(string userId, ReservationDeleteViewModel model)
        {
            bool result = false;

            var user = await userManager.FindByIdAsync(userId);
            var reservation = await dbContext.Reservations.FindAsync(model.Id);
            if (user == null)
            {
                throw new ArgumentException(UserCannotBeNull);
            }
            if (reservation == null)
            {
                throw new ArgumentException(ReservationNotFoundErrorMessage);
            }
            if (reservation.PlayerId == userId)
            {
                reservation.IsDeleted = true;
                await dbContext.SaveChangesAsync();
            }

            result = true;

            return result;
        }

        public async Task<IEnumerable<ReservationHistoryViewModel>?> GetUserReservationHistoryAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            var pastReservations = await dbContext.Reservations
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Include(r => r.Coach)
                .Include(r => r.Surface)
                .Include(r => r.TrainingType)
                .Where(r => r.PlayerId == userId && r.IsDeleted == true)
                .Select(r => new ReservationHistoryViewModel
                {
                    ReservationId = r.Id,
                    CoachName = r.Coach.Name,
                    TrainingTypeName = r.TrainingType.Name,
                    SurfaceImageUrl = r.Surface.ImageUrl,
                    SurfaceName = r.Surface.Name
                })
                .ToListAsync();

            return pastReservations;
        }
        public async Task DateValidationAsync(ReservationCreateInputModel model)
        {
            if (model.Date < DateTime.Now)
            {
                throw new ArgumentException(PastDateErrorMessage);
            }
            if (model.Date < DateTime.Now.AddHours(2))
            {
                throw new ArgumentException(TwoHoursErrorMessage);
            }
            if (model.Date > DateTime.Now.AddDays(14))
            {
                throw new ArgumentException(FutureDateErrorMessage);
            }
            if (model.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                throw new ArgumentException(SundayErrorMessage);
            }
            if (model.Date.TimeOfDay < TimeSpan.FromHours(8)
               || model.Date.AddMinutes(model.Duration).TimeOfDay > TimeSpan.FromHours(20))
            {
                throw new ArgumentException(SelectedTimeErrorMessage);
            }
            await Task.CompletedTask;
        }
        public async Task IsCoachAvailableAtTheTimeAsync(ReservationCreateInputModel model)
        {
            var endDate = model.Date.AddMinutes(model.Duration);

            bool existingReservation = await dbContext.Reservations
                .AsNoTracking()
                .AnyAsync(r =>
                    r.CoachId == model.CoachId &&
                    r.Date < endDate &&
                    r.Date.AddMinutes(r.Duration) > model.Date);

            if (existingReservation)
            {
                throw new ArgumentException(CoachNotAvailableErrorMessage);
            }
        }
    }
}
