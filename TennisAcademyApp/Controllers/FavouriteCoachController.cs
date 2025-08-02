using Microsoft.AspNetCore.Mvc;
using TennisAcademyApp.Services.Core;
using TennisAcademyApp.Services.Core.Contracts;

namespace TennisAcademyApp.Controllers
{
    public class FavouriteCoachController : BaseController
    {
        private readonly IFavouriteCoachService favouriteCoachService;
        public FavouriteCoachController(IFavouriteCoachService coachService)
        {
            this.favouriteCoachService = coachService;
        }
        [HttpGet]
        public async Task<IActionResult> Favourite()
        {
            try
            {
                string userId = GetUserId()!;

                var favourites = await this.favouriteCoachService.GetFavouritesAsync(userId);

                return View(favourites);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddToFavourites(int id)
        {
            try
            {
                string userId = GetUserId()!;

                bool result = await favouriteCoachService.AddFavouriteCoachAsync(userId, id);

                if (result == false)
                {
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Favourite));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromFavourites(int id)
        {
            try
            {
                var userId = GetUserId()!;

                bool result = await favouriteCoachService.RemoveFromFavouritesAsync(userId, id);

                if (result == false)
                {
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Favourite));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
