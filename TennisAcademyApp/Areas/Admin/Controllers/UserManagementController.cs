using Microsoft.AspNetCore.Mvc;
using TennisAcademyApp.Services.Core.Contracts;

namespace TennisAcademyApp.Areas.Admin.Controllers
{
    public class UserManagementController : AdminBaseController
    {
        private readonly IUserService userService;
        public UserManagementController(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await userService.GetUserManagementDataAsync(GetUserId()!);

                return View(users);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}
