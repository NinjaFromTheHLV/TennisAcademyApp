using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisAcademyApp.Data;
using TennisAcademyApp.Data.Models;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Racket;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.Racket;

namespace TennisAcademyApp.Services.Core
{
    public class RacketService : IRacketService
    {
        private readonly TennisAcademyDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        public RacketService(TennisAcademyDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task<IEnumerable<RacketIndexViewModel>> GetAllRacketsAsync()
        {
            var rackets = await dbContext.Rackets
                .Include(r => r.RacketCart)
                .Select(r => new RacketIndexViewModel
                {
                    Id = r.Id,
                    Brand = r.Brand,
                    Model = r.Model,
                    Price = r.Price,
                    Quantity = r.Quantity,
                    ImageUrl = r.ImageUrl,
                })
                .ToListAsync();

            return rackets;
        }
        public async Task<Racket> FindRacketByIdAsync(int? id)
        {
            if (id.HasValue)
            {
                var racket = await dbContext.Rackets
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id.Value);

                if (racket == null)
                {
                    throw new ArgumentException(RacketNotFoundErrorMessage);
                }
                return racket;
            }
            else
            {
                throw new ArgumentException(RacketCannotBeNullErrorMessage);
            }
        }
        public async Task<bool> AddRacketAsync(string userId, RacketCreateInputModel model)
        {
            bool result = false;
            var user = await userManager.FindByIdAsync(userId);
            bool isAdmin = await userManager.IsInRoleAsync(user, "Admin");
            if (user == null || !isAdmin)
            {
                throw new ArgumentException("You have to be an Admin to add rackets");
            }

            var racket = new Racket
            {
                Brand = model.Brand,
                Model = model.Model,
                Price = model.Price,
                Quantity = model.Quantity,
                ImageUrl = model.ImageUrl
            };
            await dbContext.AddAsync(racket);
            await dbContext.SaveChangesAsync();

            result = true;
            return result;
        }

        public async Task<RacketEditFormModel> GetRacketForEdittingAsync(string userId, int? id)
        {
            RacketEditFormModel? model = null;
            var user = await userManager.FindByIdAsync(userId);
            bool isAdmin = await userManager.IsInRoleAsync(user, "Admin");
            if (user == null || !isAdmin)
            {
                throw new ArgumentException("You have to be an Admin to edit rackets");
            }
            var racket = await FindRacketByIdAsync(id);

            model = new RacketEditFormModel
            {
                Id = racket.Id,
                Brand = racket.Brand,
                Model = racket.Model,
                Price = racket.Price,
                Quantity = racket.Quantity,
                ImageUrl = racket.ImageUrl
            };

            return model;
        }
        public async Task<bool> EditRacketAsync(RacketEditFormModel model)
        {
            bool result = false;
            var racket = await dbContext.Rackets.FindAsync(model.Id);

            if (racket == null)
            {
                throw new ArgumentException(RacketNotFoundErrorMessage);
            }

            racket.Brand = model.Brand;
            racket.Model = model.Model;
            racket.Price = model.Price;
            racket.Quantity = model.Quantity;
            racket.ImageUrl = model.ImageUrl;

            await dbContext.SaveChangesAsync();

            result = true;

            return result;
        }

        public async Task<RacketDeleteViewModel> GetRacketForDeletingAsync(string userId, int? id)
        {
            RacketDeleteViewModel? model = null;
            var user = await userManager.FindByIdAsync(userId);
            bool isAdmin = await userManager.IsInRoleAsync(user, "Admin");
            if (user == null || !isAdmin)
            {
                throw new ArgumentException("You have to be an Admin to delete rackets");
            }

            var racket = await FindRacketByIdAsync(id);

            model = new RacketDeleteViewModel
            {
                Id = racket.Id,
                Brand = racket.Brand,
                Model = racket.Model,
                ImageUrl = racket.ImageUrl
            };

            return model;
        }
        public async Task<bool> DeleteRacketAsync(string userId, RacketDeleteViewModel model)
        {
            bool result = false;
            var user = await userManager.FindByIdAsync(userId);
            var racket = await dbContext.Rackets.FindAsync(model.Id);

            if (racket == null)
            {
                throw new ArgumentException(RacketNotFoundErrorMessage);
            }

            dbContext.Rackets.Remove(racket);
            await dbContext.SaveChangesAsync();
            result = true;

            return result;
        }
    }
}
