using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisAcademyApp.Data;
using TennisAcademyApp.Data.Models;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Cart;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.BallCart;

namespace TennisAcademyApp.Services.Core
{
    public class BallCartService : IBallCartService
    {
        private readonly TennisAcademyDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;

        public BallCartService(TennisAcademyDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<BallCartIndexViewModel>> GetAllBallsInCartAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            var ballsInCart = await dbContext.BallCart
                .Include(bc => bc.Ball)
                .Where(bc => bc.UserId == userId)
                .Select(bc => new BallCartIndexViewModel
                {
                    Id = bc.BallId,
                    Brand = bc.Ball.Brand,
                    Model = bc.Ball.Model,
                    Price = bc.Ball.Price,
                    Quantity = bc.Quantity,
                    TotalPrice = bc.Quantity * bc.Ball.Price,
                    ImageUrl = bc.Ball.ImageUrl
                })
                .ToListAsync();

            return ballsInCart;
        }

        public async Task<bool> AddBallToCartAsync(string userId, int ballId, int quantity)
        {
            bool result = false;
            var ball = await dbContext.Balls.FindAsync(ballId);
            if (ball == null || quantity <= 0 || quantity > ball.Quantity)
            {
                throw new InvalidOperationException(InvalidQuantityErrorMessage);
            }

            var existingItem = await dbContext.BallCart
                .FirstOrDefaultAsync(bc => bc.UserId == userId && bc.BallId == ballId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                ball.Quantity -= quantity;

                result = true;
            }
            else
            {
                var cartItem = new BallCart
                {
                    UserId = userId,
                    BallId = ballId,
                    Quantity = quantity
                };
                ball.Quantity -= quantity;

                await dbContext.BallCart.AddAsync(cartItem);
                result = true;
            }

            await dbContext.SaveChangesAsync();

            return result;
        }

        public async Task<bool> RemoveBallFromCartAsync(string userId, int ballId)
        {
            bool result = false;
            var ball = await dbContext.Balls.FindAsync(ballId);

            var cartItem = await dbContext.BallCart
                .FirstOrDefaultAsync(bc => bc.BallId == ballId && bc.UserId == userId);

            if (cartItem == null)
            {
                throw new InvalidOperationException(BallNotFoundInCartErrorMessage);
            }

            dbContext.BallCart.Remove(cartItem);

            await dbContext.SaveChangesAsync();
            result = true;

            return result;
        }

        public async Task<bool> CheckOutAllBallsAsync(string userId)
        {
            bool result = false;
            var user = await userManager.FindByIdAsync(userId);

            var cartItems = await dbContext.BallCart
                .Where(bc => bc.UserId == userId)
                .ToListAsync();

            if (cartItems.Any())
            {
                dbContext.BallCart.RemoveRange(cartItems);
                await dbContext.SaveChangesAsync();

                result = true;
            }
            return result;
        }
    }
}
