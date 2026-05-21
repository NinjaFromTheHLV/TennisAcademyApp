using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisAcademyApp.Data;
using TennisAcademyApp.Data.Models;
using TennisAcademyApp.Services.Core;
using TennisAcademyApp.ViewModels.Racket;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.Racket;

namespace TennisAcademyApp.Tests
{
    [TestFixture]
    public class RacketServiceTests
    {
        private TennisAcademyDbContext dbContext;
        private Mock<UserManager<ApplicationUser>> mockUserManager;
        private RacketService service;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<TennisAcademyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            dbContext = new TennisAcademyDbContext(options);

            mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            service = new RacketService(dbContext, mockUserManager.Object);
        }
        [Test]
        public async Task GetAllRacketsAsync_ShouldReturnAllRackets_WithImageUrl()
        {
            // Arrange
            var racket1 = new Racket { Brand = "Wilson", Model = "Pro Staff", Price = 200, Quantity = 5, ImageUrl = "http://image.com/wilson.jpg" };
            var racket2 = new Racket { Brand = "Babolat", Model = "Pure Drive", Price = 180, Quantity = 3, ImageUrl = "http://image.com/babolat.jpg" };
            dbContext.Rackets.AddRange(racket1, racket2);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await service.GetAllRacketsAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.Any(r => r.ImageUrl == "http://image.com/wilson.jpg"), Is.True);
            Assert.That(result.Any(r => r.ImageUrl == "http://image.com/babolat.jpg"), Is.True);
        }

        [Test]
        public void FindRacketByIdAsync_ShouldThrow_WhenIdIsNull()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await service.FindRacketByIdAsync(null);
            });

            Assert.That(ex.Message, Is.EqualTo(RacketCannotBeNullErrorMessage));
        }

        [Test]
        public async Task FindRacketByIdAsync_ShouldReturnRacket_WhenExists()
        {
            // Arrange
            var racket = new Racket { Brand = "Yonex", Model = "Ezone", Price = 220, Quantity = 4, ImageUrl = "http://image.com/yonex.jpg" };
            dbContext.Rackets.Add(racket);
            await dbContext.SaveChangesAsync();

            // Act
            var found = await service.FindRacketByIdAsync(racket.Id);

            // Assert
            Assert.That(found.ImageUrl, Is.EqualTo("http://image.com/yonex.jpg"));
        }

        [Test]
        public void FindRacketByIdAsync_ShouldThrow_WhenNotFound()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await service.FindRacketByIdAsync(999);
            });

            Assert.That(ex.Message, Is.EqualTo(RacketNotFoundErrorMessage));
        }

        [Test]
        public async Task AddRacketAsync_ShouldAdd_WhenUserIsAdmin()
        {
            // Arrange
            var adminUser = new ApplicationUser { Id = "admin1" };
            mockUserManager.Setup(m => m.FindByIdAsync("admin1")).ReturnsAsync(adminUser);
            mockUserManager.Setup(m => m.IsInRoleAsync(adminUser, "Admin")).ReturnsAsync(true);

            var model = new RacketCreateInputModel
            {
                Brand = "Head",
                Model = "Speed",
                Price = 210,
                Quantity = 6,
                ImageUrl = "http://image.com/head.jpg"
            };

            // Act
            var result = await service.AddRacketAsync("admin1", model);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(dbContext.Rackets.Count(), Is.EqualTo(1));
            Assert.That(dbContext.Rackets.First().ImageUrl, Is.EqualTo("http://image.com/head.jpg"));
        }

        [Test]
        public void AddRacketAsync_ShouldThrow_WhenNotAdmin()
        {
            // Arrange
            var normalUser = new ApplicationUser { Id = "user1" };
            mockUserManager.Setup(m => m.FindByIdAsync("user1")).ReturnsAsync(normalUser);
            mockUserManager.Setup(m => m.IsInRoleAsync(normalUser, "Admin")).ReturnsAsync(false);

            var model = new RacketCreateInputModel
            {
                Brand = "Prince",
                Model = "Tour",
                Price = 190,
                Quantity = 2,
                ImageUrl = "http://image.com/prince.jpg"
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await service.AddRacketAsync("user1", model);
            });

            Assert.That(ex.Message, Is.EqualTo("You have to be an Admin to add rackets"));
        }

        [Test]
        public async Task GetRacketForEdittingAsync_ShouldReturnModel_WhenAdmin()
        {
            // Arrange
            var adminUser = new ApplicationUser { Id = "admin2" };
            mockUserManager.Setup(m => m.FindByIdAsync("admin2")).ReturnsAsync(adminUser);
            mockUserManager.Setup(m => m.IsInRoleAsync(adminUser, "Admin")).ReturnsAsync(true);

            var racket = new Racket { Brand = "Dunlop", Model = "FX", Price = 170, Quantity = 8, ImageUrl = "http://image.com/dunlop.jpg" };
            dbContext.Rackets.Add(racket);
            await dbContext.SaveChangesAsync();

            // Act
            var model = await service.GetRacketForEdittingAsync("admin2", racket.Id);

            // Assert
            Assert.That(model.ImageUrl, Is.EqualTo("http://image.com/dunlop.jpg"));
        }

        [Test]
        public void GetRacketForEdittingAsync_ShouldThrow_WhenNotAdmin()
        {
            // Arrange
            var normalUser = new ApplicationUser { Id = "user2" };
            mockUserManager.Setup(m => m.FindByIdAsync("user2")).ReturnsAsync(normalUser);
            mockUserManager.Setup(m => m.IsInRoleAsync(normalUser, "Admin")).ReturnsAsync(false);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await service.GetRacketForEdittingAsync("user2", 1);
            });

            Assert.That(ex.Message, Is.EqualTo("You have to be an Admin to edit rackets"));
        }

        [Test]
        public async Task EditRacketAsync_ShouldUpdateImageUrl()
        {
            // Arrange
            var racket = new Racket { Brand = "Slazenger", Model = "V98", Price = 150, Quantity = 5, ImageUrl = "http://image.com/old.jpg" };
            dbContext.Rackets.Add(racket);
            await dbContext.SaveChangesAsync();

            var editModel = new RacketEditFormModel
            {
                Id = racket.Id,
                Brand = "Slazenger",
                Model = "V100",
                Price = 160,
                Quantity = 10,
                ImageUrl = "http://image.com/new.jpg"
            };

            // Act
            var result = await service.EditRacketAsync(editModel);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(dbContext.Rackets.First().ImageUrl, Is.EqualTo("http://image.com/new.jpg"));
            Assert.That(editModel.Model, Is.EqualTo("V100"));
        }

        [Test]
        public async Task GetRacketForDeletingAsync_ShouldReturnModel_WhenAdmin()
        {
            // Arrange
            var adminUser = new ApplicationUser { Id = "admin3" };
            mockUserManager.Setup(m => m.FindByIdAsync("admin3")).ReturnsAsync(adminUser);
            mockUserManager.Setup(m => m.IsInRoleAsync(adminUser, "Admin")).ReturnsAsync(true);

            var racket = new Racket { Brand = "Volkl", Model = "V1", Price = 130, Quantity = 2, ImageUrl = "http://image.com/volkl.jpg" };
            dbContext.Rackets.Add(racket);
            await dbContext.SaveChangesAsync();

            // Act
            var model = await service.GetRacketForDeletingAsync("admin3", racket.Id);

            // Assert
            Assert.That(model.ImageUrl, Is.EqualTo("http://image.com/volkl.jpg"));
        }

        [Test]
        public async Task DeleteRacketAsync_ShouldRemoveRacket()
        {
            // Arrange
            var adminUser = new ApplicationUser { Id = "admin4" };
            mockUserManager.Setup(m => m.FindByIdAsync("admin4")).ReturnsAsync(adminUser);
            mockUserManager.Setup(m => m.IsInRoleAsync(adminUser, "Admin")).ReturnsAsync(true);

            var racket = new Racket { Brand = "Tecnifibre", Model = "TF40", Price = 140, Quantity = 1, ImageUrl = "http://image.com/tf40.jpg" };
            dbContext.Rackets.Add(racket);
            await dbContext.SaveChangesAsync();

            var deleteModel = new RacketDeleteViewModel
            {
                Id = racket.Id,
                Brand = racket.Brand,
                Model = racket.Model,
                ImageUrl = racket.ImageUrl
            };

            // Act
            var result = await service.DeleteRacketAsync("admin4", racket.Id);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(dbContext.Rackets.Any(), Is.False);
        }
        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
            service = null;
            mockUserManager = null;
        }
    }
}