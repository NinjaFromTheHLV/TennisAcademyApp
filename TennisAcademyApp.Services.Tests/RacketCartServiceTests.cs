using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisAcademyApp.Data;
using TennisAcademyApp.Data.Models;
using TennisAcademyApp.Services.Core;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.RacketCart;

namespace TennisAcademyApp.Tests.Services
{
    [TestFixture]
    public class RacketCartServiceTests
    {
        private TennisAcademyDbContext dbContext;
        private Mock<UserManager<IdentityUser>> userManagerMock;
        private RacketCartService racketCartService;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<TennisAcademyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            dbContext = new TennisAcademyDbContext(options);

            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            userManagerMock = new Mock<UserManager<IdentityUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            racketCartService = new RacketCartService(dbContext, userManagerMock.Object);
        }

        [Test]
        public async Task GetAllRacketsInCartAsync_ShouldReturnCorrectRackets()
        {
            var userId = "user1";
            var user = new IdentityUser { Id = userId };

            var racket = new Racket
            {
                Id = 1,
                Brand = "Wilson",
                Model = "Pro Staff",
                Price = 150m,
                Quantity = 10,
                ImageUrl = "wilson.jpg"
            };

            var cartItem = new RacketCart
            {
                RacketId = racket.Id,
                UserId = userId,
                Quantity = 2,
                Racket = racket
            };

            await dbContext.Rackets.AddAsync(racket);
            await dbContext.RacketCart.AddAsync(cartItem);
            await dbContext.SaveChangesAsync();

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

            var result = (await racketCartService.GetAllRacketsInCartAsync(userId)).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Id, Is.EqualTo(racket.Id));
            Assert.That(result[0].Brand, Is.EqualTo("Wilson"));
            Assert.That(result[0].Model, Is.EqualTo("Pro Staff"));
            Assert.That(result[0].Price, Is.EqualTo(150m));
            Assert.That(result[0].Quantity, Is.EqualTo(2));
            Assert.That(result[0].TotalPrice, Is.EqualTo(300m)); // 2 * 150
            Assert.That(result[0].ImageUrl, Is.EqualTo("wilson.jpg"));
        }

        [Test]
        public async Task AddRacketToCartAsync_ShouldAddNewItem_WhenNotExists()
        {
            var userId = "user2";
            var user = new IdentityUser { Id = userId };

            var racket = new Racket
            {
                Id = 1,
                Brand = "Head",
                Model = "Radical",
                Price = 180m,
                Quantity = 5,
                ImageUrl = "head.jpg"
            };

            await dbContext.Rackets.AddAsync(racket);
            await dbContext.SaveChangesAsync();

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

            var result = await racketCartService.AddRacketToCartAsync(userId, racket.Id, 3);

            Assert.That(result, Is.True);

            var cartItem = await dbContext.RacketCart.FirstOrDefaultAsync(rc => rc.UserId == userId && rc.RacketId == racket.Id);
            Assert.That(cartItem, Is.Not.Null);
            Assert.That(cartItem.Quantity, Is.EqualTo(3));

            var updatedRacket = await dbContext.Rackets.FindAsync(racket.Id);
            Assert.That(updatedRacket.Quantity, Is.EqualTo(2)); // 5 - 3 = 2
        }

        [Test]
        public async Task AddRacketToCartAsync_ShouldUpdateQuantity_WhenAlreadyInCart()
        {
            var userId = "user3";
            var user = new IdentityUser { Id = userId };

            var racket = new Racket
            {
                Id = 1,
                Brand = "Babolat",
                Model = "Pure Aero",
                Price = 170m,
                Quantity = 8,
                ImageUrl = "babolat.jpg"
            };

            var existingCartItem = new RacketCart
            {
                RacketId = racket.Id,
                UserId = userId,
                Quantity = 2
            };

            await dbContext.Rackets.AddAsync(racket);
            await dbContext.RacketCart.AddAsync(existingCartItem);
            await dbContext.SaveChangesAsync();

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

            var result = await racketCartService.AddRacketToCartAsync(userId, racket.Id, 3);

            Assert.That(result, Is.True);

            var cartItem = await dbContext.RacketCart.FirstOrDefaultAsync(rc => rc.UserId == userId && rc.RacketId == racket.Id);
            Assert.That(cartItem.Quantity, Is.EqualTo(5)); // 2 + 3

            var updatedRacket = await dbContext.Rackets.FindAsync(racket.Id);
            Assert.That(updatedRacket.Quantity, Is.EqualTo(5)); // 8 - 3 = 5
        }

        [Test]
        public void AddRacketToCartAsync_ShouldThrow_WhenInvalidQuantity()
        {
            var userId = "user4";
            var user = new IdentityUser { Id = userId };

            var racket = new Racket
            {
                Id = 1,
                Brand = "Prince",
                Model = "Tour",
                Price = 140m,
                Quantity = 4,
                ImageUrl = "prince.jpg"
            };

            dbContext.Rackets.Add(racket);
            dbContext.SaveChanges();

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await racketCartService.AddRacketToCartAsync(userId, racket.Id, 0), InvalidQuantityErrorMessage);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await racketCartService.AddRacketToCartAsync(userId, racket.Id, 10), InvalidQuantityErrorMessage);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await racketCartService.AddRacketToCartAsync(userId, 999, 1), InvalidQuantityErrorMessage);
        }

        [Test]
        public async Task RemoveRacketFromCartAsync_ShouldRemoveItem_WhenExists()
        {
            var userId = "user5";
            var user = new IdentityUser { Id = userId };

            var racket = new Racket
            {
                Id = 1,
                Brand = "Yonex",
                Model = "EZone",
                Price = 160m,
                Quantity = 10,
                ImageUrl = "yonex.jpg"
            };

            var cartItem = new RacketCart
            {
                RacketId = racket.Id,
                UserId = userId,
                Quantity = 2
            };

            await dbContext.Rackets.AddAsync(racket);
            await dbContext.RacketCart.AddAsync(cartItem);
            await dbContext.SaveChangesAsync();

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

            var result = await racketCartService.RemoveRacketFromCartAsync(userId, racket.Id);

            Assert.That(result, Is.True);

            var itemInDb = await dbContext.RacketCart.FirstOrDefaultAsync(rc => rc.UserId == userId && rc.RacketId == racket.Id);
            Assert.That(itemInDb, Is.Null);
        }

        [Test]
        public void RemoveRacketFromCartAsync_ShouldThrow_WhenItemNotFound()
        {
            var userId = "user6";
            var user = new IdentityUser { Id = userId };

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await racketCartService.RemoveRacketFromCartAsync(userId, 999), RacketNotFoundInCartErrorMessage);
        }

        [Test]
        public async Task CheckOutAllRacketsAsync_ShouldRemoveAllItems()
        {
            var userId = "user7";
            var user = new IdentityUser { Id = userId };

            var cartItems = new List<RacketCart>
            {
                new RacketCart { RacketId = 1, UserId = userId, Quantity = 1 },
                new RacketCart { RacketId = 2, UserId = userId, Quantity = 2 }
            };

            await dbContext.RacketCart.AddRangeAsync(cartItems);
            await dbContext.SaveChangesAsync();

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

            var result = await racketCartService.CheckOutAllRacketsAsync(userId);

            Assert.That(result, Is.True);

            var remainingItems = await dbContext.RacketCart.Where(rc => rc.UserId == userId).ToListAsync();
            Assert.That(remainingItems.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task CheckOutAllRacketsAsync_ShouldReturnFalse_WhenNoItems()
        {
            var userId = "user8";
            var user = new IdentityUser { Id = userId };

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

            var result = await racketCartService.CheckOutAllRacketsAsync(userId);

            Assert.That(result, Is.False);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
