using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisAcademyApp.Data;
using TennisAcademyApp.Data.Models;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Reservation;
using static TennisAcademyApp.GCommon.Validations.ValidationConstants.Reservation;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.Reservation;

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
            var expiredReservations = await dbContext.Reservations
                .Where(r => r.Date <= DateTime.Now)
                .ToListAsync();

            if (expiredReservations.Any())
            {
                dbContext.Reservations.RemoveRange(expiredReservations);
                await dbContext.SaveChangesAsync();
            }

            var user = await userManager.FindByIdAsync(userId);

            var reservations = await dbContext.Reservations
                .AsNoTracking()
                .Include(r => r.Coach)
                .Include(r => r.Surface)
                .Include(r => r.TrainingType)
                .Where(r => r.PlayerId == userId)
                .Select(r => new ReservationIndexViewModel
                {
                    ReservationId = r.Id,
                    CoachName = r.Coach.Name,
                    SurfaceName = r.Surface.Name,
                    TrainingTypeName = r.TrainingType.Name,
                    Date = r.Date.ToString(DateFormat),
                    Duration = r.Duration,
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
                return false;
            }

            var surface = await dbContext.Surfaces.FindAsync(model.SurfaceId);
            var trainingType = await dbContext.Trainings.FindAsync(model.TrainingTypeId);
            var coach = await dbContext.Coaches.FindAsync(model.CoachId);

            if (surface == null || trainingType == null || coach == null)
            {
                return false;
            }

            bool existingReservation = await dbContext.Reservations
                .AsNoTracking()
                .AnyAsync(r => r.CoachId == model.CoachId && r.Date == model.Date);

            if (existingReservation)
            {
                throw new ArgumentException(CoachNotAvailableErrorMessage);
            }

            if (model.Date < DateTime.Now)
            {
                throw new ArgumentException(PastDateErrorMessage);
            }
            if (model.Date >= DateTime.Now.AddDays(14))
            {
                throw new ArgumentException(FutureDateErrorMessage);
            }
            if (model.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                throw new ArgumentException(SundayErrorMessage);
            }
            if (model.Duration != 60 && model.Duration != 120)
            {
                throw new ArgumentException(DurationErrorMessage);
            }
            if (model.Date.TimeOfDay < TimeSpan.FromHours(8) || model.Date.TimeOfDay > TimeSpan.FromHours(20))
            {
                throw new ArgumentException(SelectedTimeErrorMessage);
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
    }
}
