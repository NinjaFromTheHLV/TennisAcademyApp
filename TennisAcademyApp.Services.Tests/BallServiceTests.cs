using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisAcademyApp.Data;
using TennisAcademyApp.Data.Models;
using TennisAcademyApp.Services.Core;
using TennisAcademyApp.ViewModels.Ball;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.Ball;

namespace TennisAcademyApp.Tests.Services
{
    [TestFixture]
    public class BallServiceTests
    {
        private TennisAcademyDbContext dbContext;
        private Mock<UserManager<ApplicationUser>> userManagerMock;
        private BallService ballService;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<TennisAcademyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new TennisAcademyDbContext(options);

            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null
            );

            ballService = new BallService(dbContext, userManagerMock.Object);
        }

        [Test]
        public async Task GetAllBallsAsync_ShouldReturnAllBallsWithCorrectProperties()
        {
            dbContext.Balls.Add(new Ball
            {
                Id = 1,
                Brand = "Wilson",
                Model = "US Open",
                Price = 15.5m,
                Quantity = 100,
                ImageUrl = "ball1.jpg",
            });
            dbContext.Balls.Add(new Ball
            {
                Id = 2,
                Brand = "Head",
                Model = "ATP",
                Price = 18m,
                Quantity = 80,
                ImageUrl = "ball2.jpg",
            });
            await dbContext.SaveChangesAsync();

            var result = (await ballService.GetAllBallsAsync()).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Brand, Is.EqualTo("Wilson"));
            Assert.That(result[0].ImageUrl, Is.EqualTo("ball1.jpg"));
            Assert.That(result[1].Brand, Is.EqualTo("Head"));
            Assert.That(result[1].ImageUrl, Is.EqualTo("ball2.jpg"));
        }

        [Test]
        public void FindBallByIdAsync_WithNullId_ShouldThrowArgumentException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await ballService.FindBallByIdAsync(null));
            Assert.That(ex.Message, Is.EqualTo(BallCannotBeNullErrorMessage));
        }

        [Test]
        public void FindBallByIdAsync_WithNonExistingId_ShouldThrowArgumentException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await ballService.FindBallByIdAsync(999));
            Assert.That(ex.Message, Is.EqualTo(BallNotFoundErrorMessage));
        }

        [Test]
        public async Task FindBallByIdAsync_WithValidId_ShouldReturnBall()
        {
            var ball = new Ball
            {
                Id = 1,
                Brand = "Babolat",
                Model = "Team",
                Price = 14m,
                Quantity = 50,
                ImageUrl = "babolat.jpg"
            };
            dbContext.Balls.Add(ball);
            await dbContext.SaveChangesAsync();

            var result = await ballService.FindBallByIdAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Brand, Is.EqualTo("Babolat"));
            Assert.That(result.ImageUrl, Is.EqualTo("babolat.jpg"));
        }

        [Test]
        public async Task AddBallAsync_UserIsAdmin_ShouldAddBallAndReturnTrue()
        {
            var user = new ApplicationUser { Id = "admin-id" };
            var model = new BallCreateInputModel
            {
                Brand = "Yonex",
                Model = "Tour",
                Price = 16m,
                Quantity = 60,
                ImageUrl = "yonex.jpg"
            };

            userManagerMock.Setup(x => x.FindByIdAsync("admin-id")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

            var result = await ballService.AddBallAsync("admin-id", model);

            Assert.That(result, Is.True);
            var ballInDb = await dbContext.Balls.FirstOrDefaultAsync(b => b.Brand == "Yonex");
            Assert.That(ballInDb, Is.Not.Null);
            Assert.That(ballInDb.ImageUrl, Is.EqualTo("yonex.jpg"));
        }

        [Test]
        public async Task AddBallAsync_UserNotAdmin_ShouldNotAddBallAndReturnFalse()
        {
            var user = new ApplicationUser { Id = "user-id" };
            var model = new BallCreateInputModel
            {
                Brand = "Yonex",
                Model = "Tour",
                Price = 16m,
                Quantity = 60,
                ImageUrl = "yonex.jpg"
            };

            userManagerMock.Setup(x => x.FindByIdAsync("user-id")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

            var result = await ballService.AddBallAsync("user-id", model);

            Assert.That(result, Is.False);
            var ballInDb = await dbContext.Balls.FirstOrDefaultAsync(b => b.Brand == "Yonex");
            Assert.That(ballInDb, Is.Null);
        }

        [Test]
        public async Task GetBallForEditingAsync_AdminUserWithValidBall_ShouldReturnModel()
        {
            // Arrange
            var user = new ApplicationUser { Id = "admin-id" };
            var ball = new Ball
            {
                Id = 1,
                Brand = "Wilson",
                Model = "US Open",
                Price = 15.5m,
                Quantity = 100,
                ImageUrl = "wilson.jpg"
            };

            dbContext.Balls.Add(ball);
            await dbContext.SaveChangesAsync();

            userManagerMock.Setup(x => x.FindByIdAsync("admin-id")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

            // Act
            var result = await ballService.GetBallForEditingAsync("admin-id", 1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Brand, Is.EqualTo("Wilson"));
            Assert.That(result.ImageUrl, Is.EqualTo("wilson.jpg"));
        }

        [Test]
        public void GetBallForEditingAsync_NonAdminUser_ShouldThrowUnauthorizedAccessException()
        {
            // Arrange
            var user = new ApplicationUser { Id = "user-id" }; 

            userManagerMock.Setup(x => x.FindByIdAsync("user-id")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

            // Act & Assert
            var ex = Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await ballService.GetBallForEditingAsync("user-id", 1));
            Assert.That(ex.Message, Is.EqualTo("You do not have permission to edit a ball."));
        }

        [Test]
        public void GetBallForEditingAsync_AdminUserButBallNotFound_ShouldThrowArgumentException()
        {
            // Arrange
            var user = new ApplicationUser { Id = "admin-id" }; 

            userManagerMock.Setup(x => x.FindByIdAsync("admin-id")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await ballService.GetBallForEditingAsync("admin-id", 999));
            Assert.That(ex.Message, Is.EqualTo("Ball not found."));
        }

        [Test]
        public void EditBallAsync_NonExistingBall_ShouldThrowException()
        {
            var model = new BallEditFormModel
            {
                Id = 999,
                Brand = "Fake",
                Model = "None",
                Price = 0m,
                Quantity = 0,
                ImageUrl = "none.jpg"
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await ballService.EditBallAsync(model));
            Assert.That(ex.Message, Is.EqualTo(BallNotFoundErrorMessage));
        }

        [Test]
        public async Task EditBallAsync_ExistingBall_ShouldUpdateAndReturnTrue()
        {
            var ball = new Ball
            {
                Id = 1,
                Brand = "OldBrand",
                Model = "OldModel",
                Price = 10m,
                Quantity = 30,
                ImageUrl = "old.jpg"
            };
            dbContext.Balls.Add(ball);
            await dbContext.SaveChangesAsync();

            var model = new BallEditFormModel
            {
                Id = 1,
                Brand = "NewBrand",
                Model = "NewModel",
                Price = 12m,
                Quantity = 40,
                ImageUrl = "new.jpg"
            };

            var result = await ballService.EditBallAsync(model);

            Assert.That(result, Is.True);
            var updatedBall = await dbContext.Balls.FindAsync(1);
            Assert.That(updatedBall.Brand, Is.EqualTo("NewBrand"));
            Assert.That(updatedBall.ImageUrl, Is.EqualTo("new.jpg"));
        }

        [Test]
        public void GetBallForDeletingAsync_UserNotAdmin_ShouldThrowException()
        {
            var user = new ApplicationUser { Id = "user-id" }; 

            userManagerMock.Setup(x => x.FindByIdAsync("user-id")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

            var ex = Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await ballService.GetBallForDeletingAsync("user-id", 1));
            Assert.That(ex.Message, Is.EqualTo("You do not have permission to delete a ball."));
        }

        [Test]
        public async Task GetBallForDeletingAsync_AdminUserWithValidBall_ShouldReturnModel()
        {
            // Arrange
            var user = new ApplicationUser { Id = "admin-id" }; 
            var ball = new Ball
            {
                Id = 1,
                Brand = "Wilson",
                Model = "US Open",
                Price = 15.5m,
                Quantity = 100,
                ImageUrl = "wilson.jpg"
            };

            dbContext.Balls.Add(ball);
            await dbContext.SaveChangesAsync();

            userManagerMock.Setup(x => x.FindByIdAsync("admin-id")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

            // Act
            var result = await ballService.GetBallForDeletingAsync("admin-id", 1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Brand, Is.EqualTo("Wilson"));
            Assert.That(result.Model, Is.EqualTo("US Open"));
            Assert.That(result.ImageUrl, Is.EqualTo("wilson.jpg"));
        }

        [Test]
        public void GetBallForDeletingAsync_NonAdminUser_ShouldThrowUnauthorizedAccessException()
        {
            // Arrange
            var user = new ApplicationUser { Id = "user-id" }; 

            userManagerMock.Setup(x => x.FindByIdAsync("user-id")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

            // Act & Assert
            var ex = Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await ballService.GetBallForDeletingAsync("user-id", 1));
            Assert.That(ex.Message, Is.EqualTo("You do not have permission to delete a ball."));
        }

        [Test]
        public void GetBallForDeletingAsync_AdminUserButBallNotFound_ShouldThrowArgumentException()
        {
            // Arrange
            var user = new ApplicationUser { Id = "admin-id" }; 

            userManagerMock.Setup(x => x.FindByIdAsync("admin-id")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await ballService.GetBallForDeletingAsync("admin-id", 999));
            Assert.That(ex.Message, Is.EqualTo("Ball not found."));
        }

        [Test]
        public void DeleteBallAsync_NonExistingBall_ShouldThrowException()
        {
            var user = new ApplicationUser { Id = "admin-id" }; 
            var model = new BallDeleteViewModel
            {
                Id = 999,
                Brand = "NonExistent",
                Model = "None",
                ImageUrl = "none.jpg"
            };

            userManagerMock.Setup(x => x.FindByIdAsync("admin-id")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await ballService.DeleteBallAsync("admin-id", model));
            Assert.That(ex.Message, Is.EqualTo(BallNotFoundErrorMessage));
        }

        [Test]
        public async Task DeleteBallAsync_ExistingBall_ShouldDeleteAndReturnTrue()
        {
            var user = new ApplicationUser { Id = "admin-id" }; 
            var ball = new Ball
            {
                Id = 1,
                Brand = "Wilson",
                Model = "US Open",
                Price = 15.5m,
                Quantity = 100,
                ImageUrl = "usopen.jpg"
            };
            dbContext.Balls.Add(ball);
            await dbContext.SaveChangesAsync();

            var model = new BallDeleteViewModel
            {
                Id = 1,
                Brand = "Wilson",
                Model = "US Open",
                ImageUrl = "usopen.jpg"
            };

            userManagerMock.Setup(x => x.FindByIdAsync("admin-id")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

            var result = await ballService.DeleteBallAsync("admin-id", model);

            Assert.That(result, Is.True);
            var deletedBall = await dbContext.Balls.FindAsync(1);
            Assert.That(deletedBall, Is.Null);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}