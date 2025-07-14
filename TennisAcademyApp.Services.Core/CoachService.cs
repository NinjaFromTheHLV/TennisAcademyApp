using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisAcademyApp.Data;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Coach;

namespace TennisAcademyApp.Services.Core
{
    public class CoachService : ICoachService
    {
        private readonly TennisAcademyDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        public CoachService(TennisAcademyDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<AllCoachesViewModel>?> GetAllCoachesAsync(string? userId)
        {
            IEnumerable<AllCoachesViewModel> allCoaches = await this.dbContext
                .Coaches
                .Include(uc => uc.UsersCoaches)
                .AsNoTracking()
                .Select(c => new AllCoachesViewModel
                {
                    CoachId = c.CoachId,
                    CoachName = c.Name,
                    ImageUrl = c.ImageUrl,
                    CoachAge = c.Age,
                    Description = c.Description,
                })
                .ToListAsync();

            return allCoaches;
        }
        public async Task<CoachDetailsViewModel> GetCoachDetailsAsync(string? userId, Guid? id)
        {
            CoachDetailsViewModel coachDetails = null!;

            if (id.HasValue)
            {
                var coaches = await this.dbContext
                    .Coaches
                    .Include(c => c.User)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(c => c.CoachId == id.Value);

                if (coaches != null)
                {
                    coachDetails = new CoachDetailsViewModel
                    {
                        CoachId = coaches.CoachId,
                        CoachAge = coaches.Age,
                        CoachName = coaches.Name,
                        Description = coaches.Description,
                        ImageUrl = coaches.ImageUrl,
                        IsAddedBy = userId != null ?
                            coaches.UserId.ToLower() == userId.ToLower() : false,
                    };
                }
            }
            return coachDetails;
        }
    }
}
