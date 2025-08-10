using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisAcademyApp.Data;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Admin.UserManagement;
using TennisAcademyApp.ViewModels.DropDown;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.UserManagement;

namespace TennisAcademyApp.Services.Core
{
    public class UserService : IUserService
    {
        private readonly TennisAcademyDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserService(UserManager<IdentityUser> userManager,
                           RoleManager<IdentityRole> roleManager,
                           TennisAcademyDbContext dbContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<UserIndexViewModel>> GetUserManagementDataAsync(string userId)
        {
            var usersList = await userManager.Users
                 .Where(u => u.Id != userId)
                 .ToListAsync();

            var result = new List<UserIndexViewModel>();

            foreach (var u in usersList)
            {
                var roles = await roleManager.Roles
                    .Select(r => r.Name)
                    .ToListAsync();

                result.Add(new UserIndexViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    Roles = await userManager.GetRolesAsync(u),
                    AllExistingRoles = roles
                });
            }

            return result;
        }

        public async Task<bool> AssignUserToRoleAsync(string userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId);

            bool roleExists = await roleManager.RoleExistsAsync(role);

            if (user == null || roleExists == false)
            {
                return false;
            }

            bool alreadyInRole = await userManager.IsInRoleAsync(user, role);

            if (!alreadyInRole)
            {
                var result = await userManager.AddToRoleAsync(user, role);
            }
            else
            {
                throw new InvalidOperationException(UserAlreadyInRoleErrorMessage);
            }

            return true;
        }
        public async Task<bool> RemoveUserFromRoleAsync(string userId, string role)
        {
            bool isRemoved = false;
            var user = await userManager.FindByIdAsync(userId);

            bool roleExists = await roleManager.RoleExistsAsync(role);

            if (user == null || !roleExists)
            {
                return false;
            }

            bool alreadyInRole = await userManager.IsInRoleAsync(user, role);

            if (alreadyInRole)
            {
                var result = await userManager.RemoveFromRoleAsync(user, role);
            }

            isRemoved = true;
            return isRemoved;
        }

        public async Task<bool> RemoveUserAsync(string userId)
        {
            bool isRemoved = false;
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            var userReservations = dbContext.Reservations
                .Where(r => r.PlayerId == userId);
            dbContext.Reservations.RemoveRange(userReservations);

            var userFavourites = dbContext.UserFavourites
                .Where(uf => uf.UserId == userId);
            dbContext.UserFavourites.RemoveRange(userFavourites);

            var racketCart = dbContext.RacketCart
                .Where(rc => rc.UserId == userId);
            dbContext.RacketCart.RemoveRange(racketCart);

            var ballCart = dbContext.BallCart
                .Where(bc => bc.UserId == userId);
            dbContext.BallCart.RemoveRange(ballCart);

            var bagCart = dbContext.BagCart
                .Where(bc => bc.UserId == userId);
            dbContext.BagCart.RemoveRange(bagCart);

            await dbContext.SaveChangesAsync();

            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                isRemoved = true;
            }

            return isRemoved;
        }
    }
}
