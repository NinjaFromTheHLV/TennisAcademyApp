using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TennisAcademyApp.Data;
using TennisAcademyApp.Data.Models;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Coach;
using TennisAcademyApp.ViewModels.DropDown;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.Coach;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.User;
using static TennisAcademyApp.GCommon.Validations.ValidationConstants.Coach;

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

        public async Task<IEnumerable<AllCoachesViewModel>?> GetAllCoachesAsync()
        {
            var allCoaches = await dbContext.Coaches
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

            foreach (var coach in allCoaches)
            {
                if (coach.ImageUrl.IsNullOrEmpty())
                {
                    coach.ImageUrl = NoImageUrl;
                }
            }

            return allCoaches;
        }

        public async Task<IEnumerable<CoachDropDownModel>> GetGoachesForDropDownAsync()
        {
            var coachesDropDown = await dbContext.Coaches
                .AsNoTracking()
                .Select(c => new CoachDropDownModel
                {
                    Id = c.CoachId,
                    Name = c.Name,
                })
                .ToListAsync();

            return coachesDropDown;
        }
        public async Task<CoachDetailsViewModel> GetCoachDetailsAsync(string userId, int id)
        {
            CoachDetailsViewModel? coachDetails = null;

            var user = await userManager.FindByIdAsync(userId);

            var coach = await GetCoachByIdAsync(id);

            if (coach == null)
            {
                throw new ArgumentException(CoachNotFoundErrorMessage);
            }

            coachDetails = new CoachDetailsViewModel
            {
                CoachId = coach.CoachId,
                CoachAge = coach.Age,
                CoachName = coach.Name,
                Description = coach.Description,
                ImageUrl = coach.ImageUrl,
                Nationality = coach.Nationality,
                IsInUserFavorites = userId != null ?
                            await dbContext.UserFavourites.AnyAsync(uc => uc.UserId == userId
                            && uc.CoachId == coach.CoachId) : false
            };

            return coachDetails;
        }
        public async Task<bool> AddCoachAsync(string userId, AddCoachInputModel inputModel)
        {
            bool result = false;
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException(UserCannotBeNull);
            }

            var coach = new Coach
            {
                Name = inputModel.Name,
                ImageUrl = inputModel.ImageUrl,
                Age = inputModel.Age,
                Nationality = inputModel.Nationality,
                Description = inputModel.Description,
            };
            await dbContext.Coaches.AddAsync(coach);
            await dbContext.SaveChangesAsync();

            result = true;

            return result;
        }

        public async Task<CoachEditInputModel> GetCoachForEdittingAsync(string userId, int id)
        {
            CoachEditInputModel? model = null;
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException(UserCannotBeNull);
            }
            var coach = await GetCoachByIdAsync(id);

            model = new CoachEditInputModel
            {
                CoachId = coach.CoachId,
                Name = coach.Name,
                Age = coach.Age,
                Nationality = coach.Nationality,
                Description = coach.Description,
                ImageUrl = coach.ImageUrl,
            };
            return model;
        }

        public async Task<bool> EdittedCoachAsync(string userId, CoachEditInputModel model)
        {
            bool result = false;
            var user = await userManager.FindByIdAsync(userId);

            var coach = await dbContext.Coaches.FindAsync(model.CoachId);

            if (user == null) 
            {
                throw new ArgumentException(UserCannotBeNull);
            }
            if (coach == null)
            {
                throw new ArgumentException(CoachNotFoundErrorMessage);
            }
                coach.Name = model.Name;
                coach.Age = model.Age;
                coach.Nationality = model.Nationality;
                coach.Description = model.Description;
                coach.ImageUrl = model.ImageUrl;
                coach.CoachId = model.CoachId;

                await dbContext.SaveChangesAsync();
                result = true;
            return result;
        }

        public async Task<DeleteCoachViewModel?> GetCoachForDeletingAsync(string userId, int id)
        {
            DeleteCoachViewModel? model = null;
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException(UserCannotBeNull);
            }

            var coach = await GetCoachByIdAsync(id);

            model = new DeleteCoachViewModel
            {
                CoachId = coach.CoachId,
                Name = coach.Name,
                ImageUrl = coach.ImageUrl,
            };
            return model;
        }

        public async Task<bool> DeletedCoachAsync(string userId, DeleteCoachViewModel model)
        {
            // check for admin role
            bool result = false;

            var user = await userManager.FindByIdAsync(userId);

            var coach = await dbContext.Coaches.FindAsync(model.CoachId);

            if (userId == null)
            {
                throw new ArgumentException(UserCannotBeNull);
            }
            if (coach == null)
            {
                throw new ArgumentException(CoachNotFoundErrorMessage);
            }
            dbContext.Coaches.Remove(coach);
            await dbContext.SaveChangesAsync();

            result = true;

            return result;
        }

        public async Task<Coach> GetCoachByIdAsync(int? id)
        {
            if (id.HasValue)
            {
                var coach = await dbContext.Coaches
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CoachId == id.Value);
                if (coach == null)
                {
                    throw new ArgumentException(CoachNotFoundErrorMessage);
                }
                return coach;
            }
            else
            {
                throw new ArgumentException(CoachCannotBeNullErrorMessage);
            }

        }
    }
}
