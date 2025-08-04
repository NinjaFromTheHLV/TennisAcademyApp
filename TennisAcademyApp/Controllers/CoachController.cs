using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Coach;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages;
using static TennisAcademyApp.GCommon.Validations.SuccessfulMessages.Coach;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.Coach;

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

                var details = await this.coachService.GetCoachDetailsAsync(userId, id);

                return View(details);
            }
            catch (ArgumentException)
            {
                TempData["ErrorMessage"] = CoachNotFoundErrorMessage;

                return RedirectToAction(nameof(Index));
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
                    TempData["ErrorMessage"] = InvalidData;
                    return View(inputModel);
                }
                await this.coachService.AddCoachAsync(userId, inputModel);
                TempData["SuccessMessage"] = CoachAddedSuccessfully;

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = CoachAddErrorMessage;
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                string user = GetUserId()!;
                var coach = await this.coachService.GetCoachForEdittingAsync(user, id);

                return View(coach);
            }
            catch (ArgumentException)
            {
                TempData["ErrorMessage"] = CoachNotFoundErrorMessage;
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
                    TempData["ErrorMessage"] = InvalidData;
                    return RedirectToAction(nameof(Edit), new { id = model.CoachId });
                }
                await this.coachService.EdittedCoachAsync(userId, model);

                TempData["SuccessMessage"] = CoachUpdatedSuccessfully;

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException)
            {
                TempData["ErrorMessage"] = CoachEditErrorMessage;
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
                    TempData["ErrorMessage"] = CoachNotFoundErrorMessage;
                    return RedirectToAction(nameof(Index));
                }
                return View(delete);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = CoachNotFoundErrorMessage;
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
                    TempData["ErrorMessage"] = CoachDeleteErrorMessage;
                    return RedirectToAction(nameof(Delete), new { id = model.CoachId });
                }
                TempData["SuccessMessage"] = CoachDeletedSuccessfully;

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = CoachDeleteErrorMessage;
                return RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}
