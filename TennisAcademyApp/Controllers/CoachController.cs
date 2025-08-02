using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Coach;

namespace TennisAcademyApp.Controllers
{
    public class CoachController : BaseController
    {
        private readonly ICoachService coachService;
        public CoachController(ICoachService coachService)
        {
            this.coachService = coachService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                var allCoaches = await this.coachService.GetAllCoachesAsync();

                return View(allCoaches);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return this.RedirectToAction(nameof(Index), "Home");
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                string userId = GetUserId()!;

                CoachDetailsViewModel model = await this.coachService
                    .GetCoachDetailsAsync(userId, id);

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return RedirectToAction(nameof(Index), "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                await Task.CompletedTask;
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddCoachInputModel inputModel)
        {
            try
            {
                string userId = GetUserId()!;
                if (ModelState.IsValid == false)
                {
                    return View(inputModel);
                }
                bool result = await this.coachService.AddCoachAsync(userId, inputModel);

                if (result == false)
                {
                    return View(inputModel);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                string user = GetUserId()!;
                var coachEdit = await this.coachService.GetCoachForEdittingAsync(user, id);

                if (coachEdit == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(coachEdit);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CoachEditInputModel model)
        {
            try
            {
                string userId = GetUserId()!;
                if (ModelState.IsValid == false)
                {
                    return RedirectToAction(nameof(Edit), new { id = model.CoachId });
                }
                bool result = await this.coachService.EdittedCoachAsync(userId, model);

                if (result == false)
                {
                    ModelState.AddModelError(string.Empty, "An error occured, please try again");
                    return RedirectToAction(nameof(Edit), new { id = model.CoachId });
                }

                return RedirectToAction(nameof(Details), new {id = model.CoachId});
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                string userId = GetUserId()!;
                var delete = await this.coachService.GetCoachForDeletingAsync(userId, id);

                if (delete == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(delete);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCoachViewModel model)
        {
            try
            {
                string userId = GetUserId()!;

                bool result = await this.coachService.DeletedCoachAsync(userId, model);

                if (result == false)
                {
                    ModelState.AddModelError(string.Empty, "An error occured, please try again");
                    return RedirectToAction(nameof(Delete), new { id = model.CoachId });
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}
