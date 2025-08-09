using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Admin.UserManagement;

namespace TennisAcademyApp.Services.Core
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> userManager;
        public UserService(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IEnumerable<UserIndexViewModel>> GetUserManagementDataAsync(string userId)
        {
            var usersList = await userManager.Users
            .Where(u => u.Id != userId)
            .ToListAsync();

            var result = new List<UserIndexViewModel>();

            foreach (var u in usersList)
            {
                var roles = await userManager.GetRolesAsync(u);

                result.Add(new UserIndexViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    Roles = roles
                });
            }

            return result;
        }
    }
}
