using Microsoft.AspNetCore.Mvc;
using TennisAcademyApp.Services.Core.Contracts;

namespace TennisAcademyApp.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService cartService;
        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }
        public async Task<IActionResult> RacketsIndex()
        {
            try
            {
                string userId = GetUserId()!;
                var racketsInCart = await this.cartService.GetAllRacketsInCartAsync(userId);
                return View(racketsInCart);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving rackets in cart: {ex.Message}");
                return RedirectToAction(nameof(Index), "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddRacketToCart(int racketid, int quantity)
        {
            try
            {
                string userId = GetUserId()!;
                await this.cartService.AddRacketToCartAsync(userId, racketid, quantity);
                return RedirectToAction(nameof(RacketsIndex));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding racket to cart: {ex.Message}");
                return RedirectToAction(nameof(Index), "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> RemoveRacketFromCart(int id, int racketId)
        {
            try
            {
                string userId = GetUserId()!;
                bool result = await cartService.RemoveRacketFromCartAsync(userId, id, racketId);
                if (result)
                {
                    return RedirectToAction(nameof(RacketsIndex));
                }
                else
                {
                    return BadRequest("Failed to remove racket from cart.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing racket from cart: {ex.Message}");
                return RedirectToAction(nameof(Index), "Home");
            }
        }
        public async Task<IActionResult> RacketCheckout()
        {
            try
            {
                string userId = GetUserId()!;
                bool result = await cartService.CheckOutAllRacketsAsync(userId);
                if (result)
                {
                    return RedirectToAction(nameof(RacketsIndex));
                }
                else
                {
                    return BadRequest("Failed to check out all rackets.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking out all rackets: {ex.Message}");
                return RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}
