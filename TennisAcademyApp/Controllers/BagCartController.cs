using Microsoft.AspNetCore.Mvc;
using TennisAcademyApp.Services.Core.Contracts;

namespace TennisAcademyApp.Controllers
{
    public class BagCartController : BaseController
    {
        private readonly IBagCartService cartService;

        public BagCartController(IBagCartService cartService)
        {
            this.cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                string userId = GetUserId()!;
                var bagsInCart = await this.cartService.GetAllBagsInCartAsync(userId);
                return View(bagsInCart);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving bags in cart: {ex.Message}");
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBagToCart(int bagId, int quantity)
        {
            try
            {
                string userId = GetUserId()!;
                await this.cartService.AddBagToCartAsync(userId, bagId, quantity);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding bag to cart: {ex.Message}");
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveBagFromCart(int id)
        {
            try
            {
                string userId = GetUserId()!;
                bool result = await cartService.RemoveBagFromCartAsync(userId, id);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return BadRequest("Failed to remove bag from cart.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing bag from cart: {ex.Message}");
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        public async Task<IActionResult> BagCheckout()
        {
            try
            {
                string userId = GetUserId()!;
                bool result = await cartService.CheckOutAllBagsAsync(userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return BadRequest("Failed to check out all bags.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking out all bags: {ex.Message}");
                return RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}