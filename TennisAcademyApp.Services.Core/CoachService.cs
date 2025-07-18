using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisAcademyApp.Data;
using TennisAcademyApp.Data.Models;
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
        public async Task<bool> AddCoachAsync(string userId, AddCoachInputModel inputModel)
        {
            bool result = false;
            IdentityUser? user = await this.userManager
                .FindByIdAsync(userId);

            if (user != null)
            {
                var coach = new Coach
                {
                    Name = inputModel.Name,
                    ImageUrl = inputModel.ImageUrl,
                    Age = inputModel.Age,
                    UserId = userId,
                    Description = inputModel.Description,
                };
                await dbContext.Coaches.AddAsync(coach);
                await dbContext.SaveChangesAsync();

                result = true;
            }
            return result;
        }

        public async Task<CoachEditViewModel?> GetCoachForEdittingAsync(Guid? id, string? userId)
        {
            CoachEditViewModel? model = null;
            IdentityUser? user = await this.userManager
                .FindByIdAsync(userId!);

            if (id.HasValue)
            {
                var coach = await this.dbContext
                    .Coaches
                    .AsNoTracking()
                    .SingleOrDefaultAsync(c => c.CoachId == id.Value);
                if (coach != null && coach.UserId.ToLower() == userId!.ToLower())
                {
                    model = new CoachEditViewModel
                    {
                        CoachId = coach.CoachId,
                        Name = coach.Name,
                        Age = coach.Age,
                        Description = coach.Description,
                        ImageUrl = coach.ImageUrl,
                    };
                }
            }
            return model;
        }

        public async Task<bool> EdittedCoachAsync(string userId, CoachEditViewModel model)
        {
            bool result = false;
            IdentityUser? user = await this.userManager
                .FindByIdAsync(userId);
            Coach? coach = await this.dbContext
                .Coaches
                .FindAsync(model.CoachId);

            if (userId != null && coach != null && coach.UserId.ToLower() == userId.ToLower())
            {
                coach.Name = model.Name;
                coach.Age = model.Age;
                coach.Description = model.Description;
                coach.ImageUrl = model.ImageUrl;
                coach.CoachId = model.CoachId;

                await this.dbContext.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<DeleteCoachViewModel?> GetCoachForDeletingAsync(string? userId, Guid? id)
        {
            DeleteCoachViewModel? model = null;
            IdentityUser? user = await this.userManager
                .FindByIdAsync(userId!);

            if (id.HasValue)
            {
                var coach = await this.dbContext
                    .Coaches
                    .AsNoTracking()
                    .SingleOrDefaultAsync(c => c.CoachId == id.Value);
                if (coach != null && coach.UserId.ToLower() == userId!.ToLower())
                {
                    model = new DeleteCoachViewModel
                    {
                        CoachId = coach.CoachId,
                        Name = coach.Name,
                        Age = coach.Age,
                        Description = coach.Description,
                        ImageUrl = coach.ImageUrl,
                    };
                }
            }
            return model;
        }
    }
}
