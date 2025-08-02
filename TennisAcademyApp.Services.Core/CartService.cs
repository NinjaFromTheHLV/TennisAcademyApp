using Microsoft.AspNetCore.Identity;
using TennisAcademyApp.Data;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Cart;
using TennisAcademyApp.Data.Models;
using TennisAcademyApp.ViewModels.Racket;
using Microsoft.EntityFrameworkCore;

namespace TennisAcademyApp.Services.Core
{
    public class CartService : ICartService
    {
        private readonly TennisAcademyDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        public CartService(TennisAcademyDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task<IEnumerable<RacketCartViewModel>> GetAllRacketsInCartAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            var racketsInCart = await dbContext.RacketCart
                .Include(r => r.Racket)
                .Where(rc => rc.UserId == userId)
                .Select(rc => new RacketCartViewModel
                {
                    Id = rc.Id,
                    Brand = rc.Racket.Brand,
                    Model = rc.Racket.Model,
                    Price = rc.Racket.Price,
                    Quantity = rc.Quantity,
                    ImageUrl = rc.Racket.ImageUrl
                })
                .ToListAsync();

            return racketsInCart;
        }
        public async Task AddRacketToCartAsync(string userId, int racketId, int quantity)
        {
            var racket = await dbContext.Rackets.FindAsync(racketId);
            if (racket == null || quantity <= 0 || quantity > racket.Quantity)
            {
                throw new InvalidOperationException("Invalid quantity.");
            }

            var existingItem = await dbContext.RacketCart
                .FirstOrDefaultAsync(rc => rc.UserId == userId && rc.RacketId == racketId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                racket.Quantity -= quantity;
            }
            else
            {
                var cartItem = new RacketCart
                {
                    UserId = userId,
                    RacketId = racketId,
                    Quantity = quantity
                };
                racket.Quantity -= quantity;

                await dbContext.RacketCart.AddAsync(cartItem);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> RemoveRacketFromCartAsync(string userId, int id, int racketId)
        {
            bool result = false;
            var racket = await dbContext.Rackets.FindAsync(racketId);
            var cartItem = await dbContext.RacketCart.FirstOrDefaultAsync(rc => rc.Id == id && rc.UserId == userId);

            if (cartItem == null)
            {
                return false;
            }

            dbContext.RacketCart.Remove(cartItem);

            await dbContext.SaveChangesAsync();
            result = true;

            return result;
        }
    }
}
