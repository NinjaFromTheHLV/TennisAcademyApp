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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRankingService rankingService;

        public BallCartService(
            TennisAcademyDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            IRankingService rankingService)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.rankingService = rankingService;
        }

        public async Task<IEnumerable<BallCartIndexViewModel>> GetAllBallsInCartAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Enumerable.Empty<BallCartIndexViewModel>();
            }

            var currentCulture = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            bool isBg = currentCulture == "bg";

            var leaderboard = await rankingService.GetLeaderboardAsync();
            var userRanking = leaderboard.FirstOrDefault(u => u.FullName == $"{user.FirstName} {user.LastName}");

            decimal discountMultiplier = 1.0m;
            if (userRanking != null)
            {
                discountMultiplier = userRanking.Position switch
                {
                    1 => 0.80m, 
                    2 => 0.85m, 
                    3 => 0.90m, 
                    _ => 1.00m 
                };
            }

            var ballsInCart = await dbContext.BallCart
                .Include(bc => bc.Ball)
                .Where(bc => bc.UserId == userId && !bc.IsOrdered) 
                .Select(bc => new BallCartIndexViewModel
                {
                    Id = bc.BallId,
                    Brand = isBg ? bc.Ball.BrandBg : bc.Ball.Brand,
                    Model = isBg ? bc.Ball.ModelBg : bc.Ball.Model,
                    Price = bc.Ball.Price * discountMultiplier,
                    Quantity = bc.Quantity,
                    TotalPrice = bc.Quantity * (bc.Ball.Price * discountMultiplier),
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
                if (!existingItem.IsOrdered)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    existingItem.Quantity = quantity;
                    existingItem.IsOrdered = false;
                }

                ball.Quantity -= quantity;
                result = true;
            }
            else
            {
                var cartItem = new BallCart
                {
                    UserId = userId,
                    BallId = ballId,
                    Quantity = quantity,
                    IsOrdered = false
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

            var cartItem = await dbContext.BallCart
                .FirstOrDefaultAsync(bc => bc.BallId == ballId && bc.UserId == userId && !bc.IsOrdered);

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

            var cartItems = await dbContext.BallCart
                .Where(bc => bc.UserId == userId && !bc.IsOrdered)
                .ToListAsync();

            if (cartItems.Any())
            {
                foreach (var item in cartItems)
                {
                    item.IsOrdered = true; 
                }

                await dbContext.SaveChangesAsync();
                result = true;
            }
            return result;
        }
    }
}