using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Bag;

namespace TennisAcademyApp.Controllers
{
    public class BagController : BaseController
    {
        private readonly IBagService bagService;

        public BagController(IBagService bagService)
        {
            this.bagService = bagService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                var bags = await this.bagService.GetAllBagsAsync();
                return View(bags);
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
        public async Task<IActionResult> Create(BagCreateInputModel inputModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(inputModel);
                }

                string userId = GetUserId()!;

                bool isAdded = await this.bagService.AddBagAsync(userId, inputModel);
                if (isAdded)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Failed to add bag.");
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
                var bag = await this.bagService.GetBagForEditingAsync(userId, id);

                if (bag == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(bag);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BagEditFormModel editModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Edit), new { id = editModel.Id });
                }

                bool isEdited = await this.bagService.EditBagAsync(editModel);
                if (isEdited)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Failed to edit bag.");
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
                var bag = await this.bagService.GetBagForDeletingAsync(userId, id);

                if (bag == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(bag);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(BagDeleteViewModel deleteModel)
        {
            try
            {
                bool isDeleted = await this.bagService.DeleteBagAsync(GetUserId()!, deleteModel);

                if (isDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Failed to delete bag.");
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
