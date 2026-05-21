using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using TennisAcademyApp.Data;
using TennisAcademyApp.Data.Models;
using TennisAcademyApp.GCommon.Validations;
using TennisAcademyApp.Services.Core;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.Coach;

namespace TennisAcademyApp.Tests.Services
{
    [TestFixture]
    public class FavouriteCoachServiceTests
    {
        private TennisAcademyDbContext dbContext;
        private Mock<UserManager<ApplicationUser>> userManagerMock;
        private FavouriteCoachService service;
        private ApplicationUser testUser;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<TennisAcademyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            dbContext = new TennisAcademyDbContext(options);

            var store = new Mock<IUserStore<ApplicationUser>>();
            userManagerMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            service = new FavouriteCoachService(dbContext, userManagerMock.Object);

            testUser = new ApplicationUser { Id = "user1", UserName = "test" };
        }

        [Test]
        public async Task GetFavouritesAsync_WhenNoFavourites_ReturnsEmptyList()
        {
            var result = await service.GetFavouritesAsync("user1");

            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetFavouritesAsync_WhenCoachHasNoImageUrl_UsesDefault()
        {
            var coach = new Coach { CoachId = 1, Name = "Coach1", Age = 30, ImageUrl = null, Description = "null", Nationality = "a" };
            var fav = new UserFavourite { UserId = "user1", CoachId = 1, Coach = coach };

            dbContext.Coaches.Add(coach);
            dbContext.UserFavourites.Add(fav);
            await dbContext.SaveChangesAsync();

            var result = (await service.GetFavouritesAsync("user1")).First();

            Assert.That(result.ImageUrl, Is.EqualTo(ValidationConstants.Coach.NoImageUrl));
        }

        [Test]
        public void AddFavouriteCoachAsync_WhenCoachNotFound_ThrowsException()
        {
            userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(testUser);

            var ex = Assert.ThrowsAsync<ArgumentException>(() => service.AddFavouriteCoachAsync("user1", 99));

            Assert.That(ex.Message, Is.EqualTo(CoachNotFoundErrorMessage));
        }

        [Test]
        public async Task AddFavouriteCoachAsync_WhenUserIsNull_ReturnsFalse()
        {
            userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

            var coach = new Coach {CoachId = 1, Name = "Coach1", Description = "null", Nationality = "null", };
            dbContext.Coaches.Add(coach);
            await dbContext.SaveChangesAsync();

            var result = await service.AddFavouriteCoachAsync("user1", 1);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task AddFavouriteCoachAsync_WhenAlreadyInFavourites_ThrowsException()
        {
            userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(testUser);

            var coach = new Coach { CoachId = 1, Name = "Coach1", Description = "null", Nationality = "a" };
            dbContext.Coaches.Add(coach);
            dbContext.UserFavourites.Add(new UserFavourite { UserId = "user1", CoachId = 1 });
            await dbContext.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(() => service.AddFavouriteCoachAsync("user1", 1));

            Assert.That(ex.Message, Is.EqualTo(CoachAlreadyAddedToFavouritesErrorMessage));
        }

        [Test]
        public async Task AddFavouriteCoachAsync_WhenValid_AddsCoach()
        {
            userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(testUser);

            var coach = new Coach {CoachId = 1, Name = "Coach1", Description = "null", Nationality = "null" };
            dbContext.Coaches.Add(coach);
            await dbContext.SaveChangesAsync();

            var result = await service.AddFavouriteCoachAsync("user1", 1);

            Assert.That(result, Is.True);
            Assert.That(dbContext.UserFavourites.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task RemoveFromFavouritesAsync_WhenUserIsNull_ReturnsFalse()
        {
            userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

            var result = await service.RemoveFromFavouritesAsync("user1", 1);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task RemoveFromFavouritesAsync_WhenCoachIsNull_ReturnsFalse()
        {
            userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(testUser);

            var result = await service.RemoveFromFavouritesAsync("user1", 1);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task RemoveFromFavouritesAsync_WhenNotInFavourites_ReturnsFalse()
        {
            userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(testUser);

            var coach = new Coach {CoachId = 1, Name = "Coach1", Description = "null", Nationality = "null" };
            dbContext.Coaches.Add(coach);
            await dbContext.SaveChangesAsync();

            var result = await service.RemoveFromFavouritesAsync("user1", 1);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task RemoveFromFavouritesAsync_WhenValid_RemovesAndReturnsTrue()
        {
            userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(testUser);

            var coach = new Coach { CoachId = 1, Name = "Coach1", Description = "null", Nationality = "a" };
            dbContext.Coaches.Add(coach);
            dbContext.UserFavourites.Add(new UserFavourite { UserId = "user1", CoachId = 1 });
            await dbContext.SaveChangesAsync();

            var result = await service.RemoveFromFavouritesAsync("user1", 1);

            Assert.That(result, Is.True);
            Assert.That(dbContext.UserFavourites.Count(), Is.EqualTo(0));
        }
        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
