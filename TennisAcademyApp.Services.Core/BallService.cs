using GTranslate.Translators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisAcademyApp.Data;
using TennisAcademyApp.Data.Models;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Ball;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.Ball;

namespace TennisAcademyApp.Services.Core
{
    public class BallService : IBallService
    {
        private readonly TennisAcademyDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;

        public BallService(TennisAcademyDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<BallIndexViewModel>> GetAllBallsAsync()
        {
            var currentCulture = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            bool isBg = currentCulture == "bg";

            var balls = await dbContext.Balls
                .AsNoTracking()
                .Include(b => b.RacketCarts)
                .Select(b => new BallIndexViewModel
                {
                    Id = b.Id,
                    Brand = isBg ? b.BrandBg : b.Brand,
                    Model = isBg ? b.ModelBg : b.Model,
                    Price = b.Price,
                    Quantity = b.Quantity,
                    ImageUrl = b.ImageUrl,
                })
                .ToListAsync();

            return balls;
        }

        public async Task<Ball> FindBallByIdAsync(int? id)
        {
            if (id.HasValue)
            {
                var ball = await dbContext.Balls
                    .AsNoTracking()
                    .FirstOrDefaultAsync(b => b.Id == id.Value);

                if (ball == null)
                {
                    throw new ArgumentException(BallNotFoundErrorMessage);
                }

                return ball;
            }
            else
            {
                throw new ArgumentException(BallCannotBeNullErrorMessage);
            }
        }

        public async Task<bool> AddBallAsync(string userId, BallCreateInputModel model)
        {
            bool result = false;
            var user = await userManager.FindByIdAsync(userId);
            bool IsAdmin = await userManager.IsInRoleAsync(user, "Admin");

            if (IsAdmin)
            {
                var translator = new GoogleTranslator();

                var brandTranslation = await translator.TranslateAsync(model.Brand, "bg", "en");
                string brandBgResult = brandTranslation.Translation;

                var modelTranslation = await translator.TranslateAsync(model.Model, "bg", "en");
                string modelBgResult = modelTranslation.Translation;

                var ball = new Ball
                {
                    Brand = model.Brand,
                    BrandBg = brandBgResult,
                    Model = model.Model,
                    ModelBg = modelBgResult,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    ImageUrl = model.ImageUrl ?? "~/pictures/DefaultBallImage.webp",
                };

                await dbContext.Balls.AddAsync(ball);
                await dbContext.SaveChangesAsync();

                result = true;
            }

            return result;
        }

        public async Task<BallEditFormModel> GetBallForEditingAsync(string userId, int? id)
        {
            BallEditFormModel? model = null;
            var user = await userManager.FindByIdAsync(userId);
            bool IsAdmin = await userManager.IsInRoleAsync(user, "Admin");
            if (!IsAdmin)
            {
                throw new UnauthorizedAccessException("You do not have permission to edit a ball.");
            }

            var ball = await FindBallByIdAsync(id);

            model = new BallEditFormModel
            {
                Id = ball.Id,
                Brand = ball.Brand,
                Model = ball.Model,
                Price = ball.Price,
                Quantity = ball.Quantity,
                ImageUrl = ball.ImageUrl
            };

            return model;
        }

        public async Task<bool> EditBallAsync(BallEditFormModel model)
        {
            bool result = false;

            var ball = await dbContext.Balls.FirstOrDefaultAsync(b => b.Id == model.Id);

            if (ball == null)
            {
                throw new ArgumentException(BallNotFoundErrorMessage);
            }
            var translator = new GoogleTranslator();

            var brandTranslation = await translator.TranslateAsync(model.Brand, "bg", "en");
            string brandBgResult = brandTranslation.Translation;

            var modelTranslation = await translator.TranslateAsync(model.Model, "bg", "en");
            string modelBgResult = modelTranslation.Translation;

            ball.Brand = model.Brand;
            ball.BrandBg = brandBgResult;
            ball.Model = model.Model;
            ball.ModelBg = modelBgResult;
            ball.Price = model.Price;
            ball.Quantity = model.Quantity;
            ball.ImageUrl = model.ImageUrl;

            dbContext.Entry(ball).State = EntityState.Modified;

            await dbContext.SaveChangesAsync();

            result = true;
            return result;
        }

        public async Task<BallDeleteViewModel> GetBallForDeletingAsync(string userId, int? id)
        {
            var user = await userManager.FindByIdAsync(userId);
            bool IsAdmin = await userManager.IsInRoleAsync(user, "Admin");
            if (user == null || !IsAdmin)
            {
                throw new UnauthorizedAccessException("You do not have permission to delete a ball.");
            }

            var ball = await FindBallByIdAsync(id);

            var currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            bool isBg = currentCulture.Equals("bg", StringComparison.OrdinalIgnoreCase);

            var model = new BallDeleteViewModel
            {
                Id = ball.Id,
                Brand = isBg ? ball.BrandBg : ball.Brand,
                Model = isBg ? ball.ModelBg : ball.Model,
                ImageUrl = ball.ImageUrl
            };

            return model;
        }

        public async Task<bool> DeleteBallAsync(string userId, BallDeleteViewModel model)
        {
            bool result = false;
            var user = await userManager.FindByIdAsync(userId);

            var ball = await dbContext.Balls.FindAsync(model.Id);

            if (ball == null)
            {
                throw new ArgumentException(BallNotFoundErrorMessage);
            }

            dbContext.Balls.Remove(ball);
            await dbContext.SaveChangesAsync();
            result = true;

            return result;
        }
    }
}
