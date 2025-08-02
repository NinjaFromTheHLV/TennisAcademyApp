using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Ball;

namespace TennisAcademyApp.Controllers
{
    public class BallController : BaseController
    {
        private readonly IBallService ballService;

        public BallController(IBallService ballService)
        {
            this.ballService = ballService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                var balls = await this.ballService.GetAllBallsAsync();
                return View(balls);
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
        public async Task<IActionResult> Create(BallCreateInputModel inputModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(inputModel);
                }

                string userId = GetUserId()!;

                bool isAdded = await this.ballService.AddBallAsync(userId, inputModel);
                if (isAdded)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Failed to add ball.");
                return View(inputModel);
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
                var ball = await this.ballService.GetBallForEditingAsync(userId, id);

                if (ball == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(ball);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BallEditFormModel editModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Edit), new { id = editModel.Id });
                }

                bool isEdited = await this.ballService.EditBallAsync(editModel);
                if (isEdited)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Failed to edit ball.");
                return RedirectToAction(nameof(Edit), new { id = editModel.Id });
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
                var ball = await this.ballService.GetBallForDeletingAsync(userId, id);

                if (ball == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(ball);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(BallDeleteViewModel deleteModel)
        {
            try
            {
                bool isDeleted = await this.ballService.DeleteBallAsync(GetUserId()!, deleteModel);

                if (isDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Failed to delete ball.");
                return RedirectToAction(nameof(Delete), new { id = deleteModel.Id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}
