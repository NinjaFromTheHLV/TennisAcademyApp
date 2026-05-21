using Microsoft.AspNetCore.Identity;
using TennisAcademyApp.Data;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Cart;
using TennisAcademyApp.Data.Models;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.RacketCart;
using Microsoft.EntityFrameworkCore;

namespace TennisAcademyApp.Services.Core
{
    public class RacketCartService : IRacketCartService
    {
        private readonly TennisAcademyDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRankingService rankingService;

        public RacketCartService(
            TennisAcademyDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            IRankingService rankingService)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.rankingService = rankingService;
        }

        public async Task<IEnumerable<RacketCartIndexViewModel>> GetAllRacketsInCartAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Enumerable.Empty<RacketCartIndexViewModel>();
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

            var racketsInCart = await dbContext.RacketCart
                .Include(r => r.Racket)
                .Where(rc => rc.UserId == userId && !rc.IsOrdered)
                .Select(rc => new RacketCartIndexViewModel
                {
                    Id = rc.RacketId,
                    Brand = isBg ? rc.Racket.BrandBg : rc.Racket.Brand,
                    Model = isBg ? rc.Racket.ModelBg : rc.Racket.Model,
                    Price = rc.Racket.Price * discountMultiplier,
                    Quantity = rc.Quantity,
                    TotalPrice = rc.Quantity * (rc.Racket.Price * discountMultiplier),
                    ImageUrl = rc.Racket.ImageUrl
                })
                .ToListAsync();

            return racketsInCart;
        }

        public async Task<bool> AddRacketToCartAsync(string userId, int racketId, int quantity)
        {
            bool result = false;
            var racket = await dbContext.Rackets.FindAsync(racketId);

            if (racket == null || quantity <= 0 || quantity > racket.Quantity)
            {
                throw new InvalidOperationException(InvalidQuantityErrorMessage);
            }

            var existingItem = await dbContext.RacketCart
                .FirstOrDefaultAsync(rc => rc.UserId == userId && rc.RacketId == racketId);

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

                racket.Quantity -= quantity;
                result = true;
            }
            else
            {
                var cartItem = new RacketCart
                {
                    UserId = userId,
                    RacketId = racketId,
                    Quantity = quantity,
                    IsOrdered = false
                };
                racket.Quantity -= quantity;

                await dbContext.RacketCart.AddAsync(cartItem);
                result = true;
            }

            await dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<bool> RemoveRacketFromCartAsync(string userId, int racketId)
        {
            bool result = false;

            var cartItem = await dbContext.RacketCart
                .FirstOrDefaultAsync(rc => rc.RacketId == racketId && rc.UserId == userId && !rc.IsOrdered);

            if (cartItem == null)
            {
                throw new InvalidOperationException(RacketNotFoundInCartErrorMessage);
            }

            dbContext.RacketCart.Remove(cartItem);
            await dbContext.SaveChangesAsync();
            result = true;

            return result;
        }

        public async Task<bool> CheckOutAllRacketsAsync(string userId)
        {
            bool result = false;

            var cartItems = await dbContext.RacketCart
                .Where(rc => rc.UserId == userId && !rc.IsOrdered)
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