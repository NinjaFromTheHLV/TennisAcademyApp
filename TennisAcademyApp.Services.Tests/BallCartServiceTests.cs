using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisAcademyApp.Data;
using TennisAcademyApp.Data.Models;
using TennisAcademyApp.Services.Core;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.BallCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisAcademyApp.Tests.Services
{
    [TestFixture]
    public class BallCartServiceTests
    {
        private TennisAcademyDbContext dbContext;
        private Mock<UserManager<IdentityUser>> userManagerMock;
        private BallCartService ballCartService;

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

            ballCartService = new BallCartService(dbContext, userManagerMock.Object);
        }

        [Test]
        public async Task GetAllBallsInCartAsync_ShouldReturnCorrectBalls()
        {
            var userId = "user1";
            var user = new IdentityUser { Id = userId };

            var ball = new Ball
            {
                Id = 1,
                Brand = "Wilson",
                Model = "Pro Staff",
                Price = 15m,
                Quantity = 10,
                ImageUrl = "wilson.jpg"
            };

            var cartItem = new BallCart
            {
                BallId = ball.Id,
                UserId = userId,
                Quantity = 3,
                Ball = ball
            };

            await dbContext.Balls.AddAsync(ball);
            await dbContext.BallCart.AddAsync(cartItem);
            await dbContext.SaveChangesAsync();

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

            var result = (await ballCartService.GetAllBallsInCartAsync(userId)).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Id, Is.EqualTo(ball.Id));
            Assert.That(result[0].Brand, Is.EqualTo("Wilson"));
            Assert.That(result[0].Model, Is.EqualTo("Pro Staff"));
            Assert.That(result[0].Price, Is.EqualTo(15m));
            Assert.That(result[0].Quantity, Is.EqualTo(3));
            Assert.That(result[0].TotalPrice, Is.EqualTo(45m)); // 3 * 15
            Assert.That(result[0].ImageUrl, Is.EqualTo("wilson.jpg"));
        }

        [Test]
        public async Task AddBallToCartAsync_ShouldAddNewItem_WhenNotExists()
        {
            var userId = "user2";
            var user = new IdentityUser { Id = userId };

            var ball = new Ball
            {
                Id = 1,
                Brand = "Head",
                Model = "Speed",
                Price = 20m,
                Quantity = 8,
                ImageUrl = "head.jpg"
            };
            await dbContext.Balls.AddAsync(ball);
            await dbContext.SaveChangesAsync();

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

            var result = await ballCartService.AddBallToCartAsync(userId, ball.Id, 3);

            Assert.That(result, Is.True);

            var cartItem = await dbContext.BallCart.FirstOrDefaultAsync(bc => bc.UserId == userId && bc.BallId == ball.Id);
            Assert.That(cartItem, Is.Not.Null);
            Assert.That(cartItem.Quantity, Is.EqualTo(3));

            var updatedBall = await dbContext.Balls.FindAsync(ball.Id);
            Assert.That(updatedBall.Quantity, Is.EqualTo(5)); // 8 - 3 = 5
        }

        [Test]
        public async Task AddBallToCartAsync_ShouldUpdateQuantity_WhenAlreadyInCart()
        {
            var userId = "user3";
            var user = new IdentityUser { Id = userId };

            var ball = new Ball
            {
                Id = 1,
                Brand = "Babolat",
                Model = "Pure Drive",
                Price = 18m,
                Quantity = 12,
                ImageUrl = "babolat.jpg"
            };

            var existingCartItem = new BallCart
            {
                BallId = ball.Id,
                UserId = userId,
                Quantity = 4
            };

            await dbContext.Balls.AddAsync(ball);
            await dbContext.BallCart.AddAsync(existingCartItem);
            await dbContext.SaveChangesAsync();

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

            var result = await ballCartService.AddBallToCartAsync(userId, ball.Id, 3);

            Assert.That(result, Is.True);

            var cartItem = await dbContext.BallCart.FirstOrDefaultAsync(bc => bc.UserId == userId && bc.BallId == ball.Id);
            Assert.That(cartItem.Quantity, Is.EqualTo(7)); // 4 + 3

            var updatedBall = await dbContext.Balls.FindAsync(ball.Id);
            Assert.That(updatedBall.Quantity, Is.EqualTo(9)); // 12 - 3 = 9
        }

        [Test]
        public void AddBallToCartAsync_ShouldThrow_WhenInvalidQuantity()
        {
            var userId = "user4";
            var user = new IdentityUser { Id = userId };

            var ball = new Ball
            {
                Id = 1,
                Brand = "Prince",
                Model = "Tour",
                Price = 16m,
                Quantity = 6,
                ImageUrl = "prince.jpg"
            };

            dbContext.Balls.Add(ball);
            dbContext.SaveChanges();

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await ballCartService.AddBallToCartAsync(userId, ball.Id, 0), InvalidQuantityErrorMessage);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await ballCartService.AddBallToCartAsync(userId, ball.Id, 10), InvalidQuantityErrorMessage);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await ballCartService.AddBallToCartAsync(userId, 999, 1), InvalidQuantityErrorMessage);
        }

        [Test]
        public async Task RemoveBallFromCartAsync_ShouldRemoveItem_WhenExists()
        {
            var userId = "user5";
            var user = new IdentityUser { Id = userId };

            var ball = new Ball
            {
                Id = 1,
                Brand = "Dunlop",
                Model = "Fort",
                Price = 14m,
                Quantity = 10,
                ImageUrl = "dunlop.jpg"
            };

            var cartItem = new BallCart
            {
                BallId = ball.Id,
                UserId = userId,
                Quantity = 2
            };

            await dbContext.Balls.AddAsync(ball);
            await dbContext.BallCart.AddAsync(cartItem);
            await dbContext.SaveChangesAsync();

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

            var result = await ballCartService.RemoveBallFromCartAsync(userId, ball.Id);

            Assert.That(result, Is.True);

            var itemInDb = await dbContext.BallCart.FirstOrDefaultAsync(bc => bc.UserId == userId && bc.BallId == ball.Id);
            Assert.That(itemInDb, Is.Null);
        }

        [Test]
        public void RemoveBallFromCartAsync_ShouldThrow_WhenItemNotFound()
        {
            var userId = "user6";
            var user = new IdentityUser { Id = userId };

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await ballCartService.RemoveBallFromCartAsync(userId, 999), BallNotFoundInCartErrorMessage);
        }

        [Test]
        public async Task CheckOutAllBallsAsync_ShouldRemoveAllItems()
        {
            var userId = "user7";
            var user = new IdentityUser { Id = userId };

            var cartItems = new List<BallCart>
            {
                new BallCart { BallId = 1, UserId = userId, Quantity = 1 },
                new BallCart { BallId = 2, UserId = userId, Quantity = 2 }
            };

            await dbContext.BallCart.AddRangeAsync(cartItems);
            await dbContext.SaveChangesAsync();

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

            var result = await ballCartService.CheckOutAllBallsAsync(userId);

            Assert.That(result, Is.True);

            var remainingItems = await dbContext.BallCart.Where(bc => bc.UserId == userId).ToListAsync();
            Assert.That(remainingItems.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task CheckOutAllBallsAsync_ShouldReturnFalse_WhenNoItems()
        {
            var userId = "user8";
            var user = new IdentityUser { Id = userId };

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

            var result = await ballCartService.CheckOutAllBallsAsync(userId);

            Assert.That(result, Is.False);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
