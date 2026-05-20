using GTranslate.Translators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
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
            var currentCulture = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            bool isBg = currentCulture == "bg";
            var rackets = await dbContext.Rackets
                .Include(r => r.RacketCart)
                .Select(r => new RacketIndexViewModel
                {
                    Id = r.Id,
                    Brand = isBg ? r.BrandBg : r.Brand,
                    Model = isBg ? r.ModelBg : r.Model,
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
            var user = await userManager.FindByIdAsync(userId);
            bool isAdmin = await userManager.IsInRoleAsync(user, "Admin");
            if (user == null || !isAdmin)
            {
                throw new ArgumentException("You have to be an Admin to add rackets");
            }

            var translator = new GoogleTranslator();

            var brandTranslation = await translator.TranslateAsync(model.Brand, "bg", "en");
            string brandBgResult = brandTranslation.Translation;

            var modelTranslation = await translator.TranslateAsync(model.Model, "bg", "en");
            string modelBgResult = modelTranslation.Translation;

            var racket = new Racket
            {
                Brand = model.Brand,
                BrandBg = brandBgResult,
                Model = model.Model,
                ModelBg = modelBgResult,
                Price = model.Price,
                Quantity = model.Quantity,
                ImageUrl = model.ImageUrl ?? "~/pictures/DefaultRacketImage.webp"
            };

            await dbContext.Rackets.AddAsync(racket);
            await dbContext.SaveChangesAsync();

            return true;
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

            var racket = await dbContext.Rackets.FirstOrDefaultAsync(r => r.Id == model.Id);

            if (racket == null)
            {
                throw new ArgumentException(RacketNotFoundErrorMessage);
            }

            var translator = new GoogleTranslator();

            var brandTranslation = await translator.TranslateAsync(model.Brand, "bg", "en");
            string brandBgResult = brandTranslation.Translation;

            var modelTranslation = await translator.TranslateAsync(model.Model, "bg", "en");
            string modelBgResult = modelTranslation.Translation;

            racket.Brand = model.Brand;
            racket.BrandBg = brandBgResult;
            racket.Model = model.Model;
            racket.ModelBg = modelBgResult;
            racket.Price = model.Price;
            racket.Quantity = model.Quantity;
            racket.ImageUrl = model.ImageUrl;

            dbContext.Entry(racket).State = EntityState.Modified;

            await dbContext.SaveChangesAsync();

            result = true;
            return result;
        }

        public async Task<RacketDeleteViewModel> GetRacketForDeletingAsync(string userId, int? id)
        {
            var user = await userManager.FindByIdAsync(userId);
            bool isAdmin = await userManager.IsInRoleAsync(user, "Admin");
            if (user == null || !isAdmin)
            {
                throw new ArgumentException("You have to be an Admin to delete rackets");
            }

            var racket = await FindRacketByIdAsync(id);

            var currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            bool isBg = currentCulture.Equals("bg", StringComparison.OrdinalIgnoreCase);

            var model = new RacketDeleteViewModel
            {
                Id = racket.Id,
                Brand = isBg ? racket.BrandBg : racket.Brand,
                Model = isBg ? racket.ModelBg : racket.Model,
                ImageUrl = racket.ImageUrl
            };

            return model;
        }
        public async Task<bool> DeleteRacketAsync(string userId, int id)
        {
            bool result = false;
            var user = await userManager.FindByIdAsync(userId);
            var racket = await dbContext.Rackets.FindAsync(id);

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
