using Microsoft.AspNetCore.Mvc;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Reservation;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.Reservation;
using static TennisAcademyApp.GCommon.Validations.SuccessfulMessages.Reservation;

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
                    TempData["ErrorMessage"] = InvalidData;
                    return View(model);
                }
                string userId = GetUserId()!;

                await reservationService.CreateReservationAsync(userId, model);
                TempData["SuccessMessage"] = ReservationCreatedSuccessfully;
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
            catch (ArgumentException)
            {
                TempData["ErrorMessage"] = ReservationNotFoundErrorMessage;
                return RedirectToAction(nameof(Index));
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
            catch (ArgumentException)
            {
                TempData["ErrorMessage"] = ReservationNotFoundErrorMessage;
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ReservationDeleteViewModel model)
        {
            try
            {
                string userId = GetUserId()!;

                await reservationService.DeleteReservationAsync(userId, model);
                TempData["SuccessMessage"] = ReservationDeletedSuccessfully;
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException)
            {
                TempData["ErrorMessage"] = ReservationDeleteErrorMessage;
                return RedirectToAction(nameof(Index));
            }
        }
        public async Task<IActionResult> ReservationHistory()
        {
            try
            {
                string userId = GetUserId()!;

                var reservationHistory = await reservationService.GetUserReservationHistoryAsync(userId);
                return View(reservationHistory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}
