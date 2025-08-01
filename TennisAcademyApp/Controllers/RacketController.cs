using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Racket;

namespace TennisAcademyApp.Controllers
{
    public class RacketController : BaseController
    {
        private readonly IRacketService racketService;
        public RacketController(IRacketService racketService)
        {
            this.racketService = racketService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                var rackets = await this.racketService.GetAllRacketsAsync();

                return View(rackets);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                await Task.CompletedTask;
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(RacketCreateInputModel inputModel)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return View(inputModel);
                }
                string userId = GetUserId()!;

                bool isAdded = await this.racketService.AddRacketAsync(userId, inputModel);
                if (isAdded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to add racket.");
                    return View(inputModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                string userId = GetUserId()!;
                var racket = await this.racketService.GetRacketForEdittingAsync(userId, id);
                if (racket == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(racket);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RacketEditFormModel editModel)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return RedirectToAction(nameof(Edit), new { id = editModel.Id });
                }
                bool isEdited = await this.racketService.EditRacketAsync(editModel);
                if (isEdited)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to edit racket.");
                    return RedirectToAction(nameof(Edit), new { id = editModel.Id });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                string userId = GetUserId()!;
                var racket = await this.racketService.GetRacketForDeletingAsync(userId, id);
                if (racket == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(racket);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(RacketDeleteViewModel deleteModel)
        {
            try
            {
                bool isDeleted = await this.racketService.DeleteRacketAsync(GetUserId()!, deleteModel);
                if (isDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to delete racket.");
                    return RedirectToAction(nameof(Delete), new { id = deleteModel.Id });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}
