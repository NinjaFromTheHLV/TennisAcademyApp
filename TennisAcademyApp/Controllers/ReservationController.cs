using Microsoft.AspNetCore.Mvc;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Reservation;

namespace TennisAcademyApp.Controllers
{
    public class ReservationController : BaseController
    {
        private readonly IReservationService reservationService;
        private readonly ISurfaceService surfaceService;
        private readonly ITrainingTypeService trainingTypeService;
        private readonly ICoachService coachService;

        public ReservationController(IReservationService reservationService,
                                    ISurfaceService surfaceService,
                                    ITrainingTypeService trainingTypeService,
                                    ICoachService coachService)
        {
            this.reservationService = reservationService;
            this.surfaceService = surfaceService;
            this.trainingTypeService = trainingTypeService;
            this.coachService = coachService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                string? userId = GetUserId();
                if (userId == null)
                {
                    return RedirectToAction(nameof(Index), "Home");
                }

                var reservations = await reservationService.GetUserReservationsAsync(userId);

                return View(reservations);
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
                string userId = GetUserId()!;

                var model = new ReservationCreateInputModel
                {
                    Coaches = await coachService.GetGoachesForDropDownAsync(),
                    Surfaces = await surfaceService.GetSurfacesForDropDownAsync(),
                    TrainingTypes = await trainingTypeService.GetAllTrainingTypesForDropDownAsync()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(ReservationCreateInputModel model)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return View(model);
                }
                string userId = GetUserId()!;

                bool isCreated = await reservationService.CreateReservationAsync(userId, model);
                if (isCreated == false)
                {
                    ModelState.AddModelError(string.Empty, "Failed to create a reservation. Please try again.");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                model.Coaches = await coachService.GetGoachesForDropDownAsync();
                model.Surfaces = await surfaceService.GetSurfacesForDropDownAsync();
                model.TrainingTypes = await trainingTypeService.GetAllTrainingTypesForDropDownAsync();
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                string userId = GetUserId()!;

                var reservationDetails = await reservationService.GetUserReservationDetailsAsync(userId, id);

                return View(reservationDetails);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                string userId = GetUserId()!;

                var reservationToDelete = await reservationService.GetUserReservationForDeletingAsync(userId, id);

                return View(reservationToDelete);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ReservationDeleteViewModel model)
        {
            try
            {
                string userId = GetUserId()!;

                bool isDeleted = await reservationService.DeleteReservationAsync(userId, model);
                if (isDeleted == false)
                {
                    ModelState.AddModelError(string.Empty, "Failed to delete the reservation. Please try again.");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}
