using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisAcademyApp.Data;
using TennisAcademyApp.Data.Models;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Bag;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.Bag;

namespace TennisAcademyApp.Services.Core
{
    public class BagService : IBagService
    {
        private readonly TennisAcademyDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;

        public BagService(TennisAcademyDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<BagIndexViewModel>> GetAllBagsAsync()
        {
            var bags = await dbContext.Bags
                .AsNoTracking()
                .Include(b => b.BagCarts)
                .Select(b => new BagIndexViewModel
                {
                    Id = b.Id,
                    Brand = b.Brand,
                    Model = b.Model,
                    Price = b.Price,
                    Quantity = b.Quantity,
                    ImageUrl = b.ImageUrl,
                })
                .ToListAsync();

            return bags;
        }

        public async Task<Bag> FindBagByIdAsync(int? id)
        {
            if (id.HasValue)
            {
                var bag = await dbContext.Bags
                    .AsNoTracking()
                    .FirstOrDefaultAsync(b => b.Id == id.Value);

                if (bag == null)
                {
                    throw new ArgumentException(BagNotFoundErrorMessage);
                }

                return bag;
            }
            else
            {
                throw new ArgumentException(BagCannotBeNullErrorMessage);
            }
        }

        public async Task<bool> AddBagAsync(string userId, BagCreateInputModel model)
        {
            bool result = false;
            var user = await userManager.FindByIdAsync(userId);

            var bag = new Bag
            {
                Brand = model.Brand,
                Model = model.Model,
                Price = model.Price,
                Quantity = model.Quantity,
                ImageUrl = model.ImageUrl
            };

            await dbContext.AddAsync(bag);
            await dbContext.SaveChangesAsync();

            result = true;
            return result;
        }

        public async Task<BagEditFormModel> GetBagForEditingAsync(string userId, int? id)
        {
            BagEditFormModel? model = null;
            var user = await userManager.FindByIdAsync(userId);

            var bag = await FindBagByIdAsync(id);

            model = new BagEditFormModel
            {
                Id = bag.Id,
                Brand = bag.Brand,
                Model = bag.Model,
                Price = bag.Price,
                Quantity = bag.Quantity,
                ImageUrl = bag.ImageUrl
            };

            return model;
        }

        public async Task<bool> EditBagAsync(BagEditFormModel model)
        {
            bool result = false;
            var bag = await dbContext.Bags.FindAsync(model.Id);

            if (bag == null)
            {
                throw new ArgumentException(BagNotFoundErrorMessage);
            }

            bag.Brand = model.Brand;
            bag.Model = model.Model;
            bag.Price = model.Price;
            bag.Quantity = model.Quantity;
            bag.ImageUrl = model.ImageUrl;

            await dbContext.SaveChangesAsync();

            result = true;
            return result;
        }

        public async Task<BagDeleteViewModel> GetBagForDeletingAsync(string userId, int? id)
        {
            BagDeleteViewModel? model = null;
            var user = await userManager.FindByIdAsync(userId);

            var bag = await FindBagByIdAsync(id);

            model = new BagDeleteViewModel
            {
                Id = bag.Id,
                Brand = bag.Brand,
                Model = bag.Model,
                ImageUrl = bag.ImageUrl
            };

            return model;
        }

        public async Task<bool> DeleteBagAsync(string userId, BagDeleteViewModel model)
        {
            bool result = false;
            var user = await userManager.FindByIdAsync(userId);

            var bag = await dbContext.Bags.FindAsync(model.Id);

            if (bag == null)
            {
                throw new ArgumentException(BagNotFoundErrorMessage);
            }

            dbContext.Bags.Remove(bag);
            await dbContext.SaveChangesAsync();
            result = true;

            return result;
        }
    }
}