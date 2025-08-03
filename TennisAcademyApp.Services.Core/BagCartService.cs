using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisAcademyApp.Data;
using TennisAcademyApp.Data.Models;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Cart;

namespace TennisAcademyApp.Services.Core
{
    public class BagCartService : IBagCartService
    {
        private readonly TennisAcademyDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;

        public BagCartService(TennisAcademyDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<BagCartIndexViewModel>> GetAllBagsInCartAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            var bagsInCart = await dbContext.BagCart
                .Include(bc => bc.Bag)
                .Where(bc => bc.UserId == userId)
                .Select(bc => new BagCartIndexViewModel
                {
                    Id = bc.BagId,
                    Brand = bc.Bag.Brand,
                    Model = bc.Bag.Model,
                    Price = bc.Bag.Price,
                    Quantity = bc.Quantity,
                    TotalPrice = bc.Quantity * bc.Bag.Price,
                    ImageUrl = bc.Bag.ImageUrl
                })
                .ToListAsync();

            return bagsInCart;
        }

        public async Task<bool> AddBagToCartAsync(string userId, int bagId, int quantity)
        {
            bool result = false;
            var bag = await dbContext.Bags.FindAsync(bagId);
            if (bag == null || quantity <= 0 || quantity > bag.Quantity)
            {
                throw new InvalidOperationException("Invalid quantity or bag not found.");
            }

            var existingItem = await dbContext.BagCart
                .FirstOrDefaultAsync(bc => bc.UserId == userId && bc.BagId == bagId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                bag.Quantity -= quantity;
                result = true;
            }
            else
            {
                var cartItem = new BagCart
                {
                    UserId = userId,
                    BagId = bagId,
                    Quantity = quantity
                };
                bag.Quantity -= quantity;

                await dbContext.BagCart.AddAsync(cartItem);
                result = true;
            }

            await dbContext.SaveChangesAsync();

            return result;
        }

        public async Task<bool> RemoveBagFromCartAsync(string userId, int bagId)
        {
            bool result = false;
            var cartItem = await dbContext.BagCart
                .FirstOrDefaultAsync(bc => bc.BagId == bagId && bc.UserId == userId);

            if (cartItem == null)
            {
                return false;
            }

            dbContext.BagCart.Remove(cartItem);
            await dbContext.SaveChangesAsync();

            result = true;
            return result;
        }
        public async Task<bool> CheckOutAllBagsAsync(string userId)
        {
            bool result = false;

            var cartItems = await dbContext.BagCart
                .Where(bc => bc.UserId == userId)
                .ToListAsync();

            if (cartItems.Any())
            {
                dbContext.BagCart.RemoveRange(cartItems);
                await dbContext.SaveChangesAsync();
                result = true;
            }

            return result;
        }
    }
}
