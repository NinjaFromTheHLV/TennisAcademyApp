using Microsoft.AspNetCore.Mvc;
using TennisAcademyApp.Services.Core.Contracts;

namespace TennisAcademyApp.Controllers
{
    public class BallCartController : BaseController
    {
        private readonly IBallCartService cartService;

        public BallCartController(IBallCartService cartService)
        {
            this.cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                string userId = GetUserId()!;
                var ballsInCart = await this.cartService.GetAllBallsInCartAsync(userId);
                return View(ballsInCart);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving balls in cart: {ex.Message}");
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBallToCart(int ballId, int quantity)
        {
            try
            {
                string userId = GetUserId()!;
                await this.cartService.AddBallToCartAsync(userId, ballId, quantity);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding ball to cart: {ex.Message}");
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveBallFromCart(int id)
        {
            try
            {
                string userId = GetUserId()!;
                bool result = await cartService.RemoveBallFromCartAsync(userId, id);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return BadRequest("Failed to remove ball from cart.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing ball from cart: {ex.Message}");
                return RedirectToAction(nameof(Index), "Home");
            }
        }
        public async Task<IActionResult> BallCheckout()
        {
            try
            {
                string userId = GetUserId()!;
                bool result = await cartService.CheckOutAllBallsAsync(userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return BadRequest("Failed to check out all balls.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking out all balls: {ex.Message}");
                return RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}
