using Microsoft.AspNetCore.Identity;
using TennisAcademyApp.Data;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Ball;
using TennisAcademyApp.Data.Models;
using Microsoft.EntityFrameworkCore;

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
            var balls = await dbContext.Balls
                .AsNoTracking()
                .Include(b => b.RacketCarts)
                .Select(b => new BallIndexViewModel
                {
                    Id = b.Id,
                    Brand = b.Brand,
                    Model = b.Model,
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
                    throw new ArgumentException("Ball not found.");
                }

                return ball;
            }
            else
            {
                throw new ArgumentException("Ball ID cannot be null.");
            }
        }

        public async Task<bool> AddBallAsync(string userId, BallCreateInputModel model)
        {
            bool result = false;
            var user = await userManager.FindByIdAsync(userId);

            var ball = new Ball
            {
                Brand = model.Brand,
                Model = model.Model,
                Price = model.Price,
                Quantity = model.Quantity,
                ImageUrl = model.ImageUrl
            };

            await dbContext.AddAsync(ball);
            await dbContext.SaveChangesAsync();

            result = true;
            return result;
        }

        public async Task<BallEditFormModel> GetBallForEditingAsync(string userId, int? id)
        {
            BallEditFormModel? model = null;
            var user = await userManager.FindByIdAsync(userId);

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
            var ball = await dbContext.Balls.FindAsync(model.Id);

            if (ball == null)
            {
                return false;
            }

            ball.Brand = model.Brand;
            ball.Model = model.Model;
            ball.Price = model.Price;
            ball.Quantity = model.Quantity;
            ball.ImageUrl = model.ImageUrl;

            await dbContext.SaveChangesAsync();

            result = true;
            return result;
        }

        public async Task<BallDeleteViewModel> GetBallForDeletingAsync(string userId, int? id)
        {
            BallDeleteViewModel? model = null;
            var user = await userManager.FindByIdAsync(userId);

            var ball = await FindBallByIdAsync(id);

            model = new BallDeleteViewModel
            {
                Id = ball.Id,
                Brand = ball.Brand,
                Model = ball.Model,
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
                throw new ArgumentException("Ball not found.");
            }

            dbContext.Balls.Remove(ball);
            await dbContext.SaveChangesAsync();
            result = true;

            return result;
        }
    }
}
