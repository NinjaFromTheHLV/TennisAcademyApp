using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisAcademyApp.Data;
using TennisAcademyApp.Data.Models;
using TennisAcademyApp.Services.Core;
using TennisAcademyApp.ViewModels.Reservation;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.Reservation;

namespace TennisAcademyApp.Tests.Services
{
    [TestFixture]
    public class ReservationServiceTests
    {
        private TennisAcademyDbContext dbContext;
        private Mock<IUserStore<IdentityUser>> userStoreMock;
        private Mock<UserManager<IdentityUser>> userManagerMock;
        private ReservationService service;

        private IdentityUser existingUser;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<TennisAcademyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            dbContext = new TennisAcademyDbContext(options);

            userStoreMock = new Mock<IUserStore<IdentityUser>>();
            userManagerMock = new Mock<UserManager<IdentityUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            service = new ReservationService(dbContext, userManagerMock.Object);

            existingUser = new IdentityUser { Id = "user-1", UserName = "tester" };
        }


        private async Task SeedBasicEntitiesAsync()
        {
            var coach = new Coach 
            { 
                CoachId = 1, 
                Name = "Coach1", 
                Age = 44, 
                Description = "ssssssssssssssss", 
                Nationality = "pulnaasa",
            };
            var surface = new Surface { Id = 1, Name = "Court1", ImageUrl = "img" };
            var training = new TrainingType { Id = 1, Name = "Tennis" };

            await dbContext.AddRangeAsync(coach, surface, training);
            await dbContext.SaveChangesAsync();
        }




        [Test]
        public async Task GetUserReservationsAsync_WhenUserHasReservations_ReturnsListMapped()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);
            await SeedBasicEntitiesAsync();

            var r = new Reservation
            {
                Id = 11,
                PlayerId = existingUser.Id,
                CoachId = 1,
                SurfaceId = 1,
                TrainingTypeId = 1,
                Date = DateTime.Now.AddDays(2),
                Duration = 60,
                IsDeleted = false
            };
            await dbContext.Reservations.AddAsync(r);
            await dbContext.SaveChangesAsync();

            // act
            var result = await service.GetUserReservationsAsync(existingUser.Id);

            // assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().CoachName, Is.EqualTo("Coach1"));
            Assert.That(result.First().TrainingTypeName, Is.EqualTo("Tennis"));
            Assert.That(result.First().ReservationId, Is.EqualTo(11));
            Assert.That(result.First().Date, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public async Task GetUserReservationsAsync_WhenUserNotFound_ReturnsEmptyList()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                           .ReturnsAsync((IdentityUser)null);

            // act
            var result = await service.GetUserReservationsAsync("nonexistent");

            // assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetUserReservationsAsync_ExpiredReservations_AutoDeletedAndNotReturned()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);
            await SeedBasicEntitiesAsync();

            // expired reservation (past) and future one
            await dbContext.Reservations.AddRangeAsync(new Reservation
            {
                Id = 21,
                PlayerId = existingUser.Id,
                CoachId = 1,
                SurfaceId = 1,
                TrainingTypeId = 1,
                Date = DateTime.Now.AddHours(-1),
                IsDeleted = false
            },
            new Reservation
            {
                Id = 22,
                PlayerId = existingUser.Id,
                CoachId = 1,
                SurfaceId = 1,
                TrainingTypeId = 1,
                Date = DateTime.Now.AddDays(1),
                IsDeleted = false
            });
            await dbContext.SaveChangesAsync();

            // act
            var result = await service.GetUserReservationsAsync(existingUser.Id);

            // assert: expired one should be auto-marked deleted and not included
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().ReservationId, Is.EqualTo(22));

            // also check DB state
            var expired = await dbContext.Reservations.FindAsync(21);
            Assert.That(expired.IsDeleted, Is.True);
        }



        [Test]
        public void CreateReservationAsync_UserNull_ThrowsArgumentException()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser)null);

            var model = new ReservationCreateInputModel
            {
                CoachId = 1,
                SurfaceId = 1,
                TrainingTypeId = 1,
                Date = DateTime.Now.AddDays(3).AddHours(3),
                Duration = 60,
                Note = "note"
            };

            // act & assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => service.CreateReservationAsync("nouser", model));
            Assert.That(ex.Message, Does.Contain("Please log in and try again").IgnoreCase);
        }

        [Test]
        public async Task CreateReservationAsync_InvalidDuration_Throws()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);
            await SeedBasicEntitiesAsync();

            var model = new ReservationCreateInputModel
            {
                CoachId = 1,
                SurfaceId = 1,
                TrainingTypeId = 1,
                Date = DateTime.Now.AddDays(3).AddHours(3),
                Duration = 30, // invalid (not 60 or 120)
                Note = "note"
            };

            // act & assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => service.CreateReservationAsync(existingUser.Id, model));
            Assert.That(ex.Message, Does.Contain("Duration").IgnoreCase);
        }

        [Test]
        public async Task CreateReservationAsync_CoachNotAvailable_ThrowsAndDoesNotCreate()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);
            await SeedBasicEntitiesAsync();

            var conflictDate = DateTime.Now.AddDays(2).Date.AddHours(10); // 10:00 two days later
            // existing reservation that conflicts
            var user = new IdentityUser { Id = "user-2", UserName = "conflictUser" };
            await dbContext.Reservations.AddAsync(new Reservation
            {
                Id = 301,
                CoachId = 1,
                Date = conflictDate,
                PlayerId = user.Id,
                Duration = 60
            });
            await dbContext.SaveChangesAsync();

            var model = new ReservationCreateInputModel
            {
                CoachId = 1,
                SurfaceId = 1,
                TrainingTypeId = 1,
                Date = conflictDate, // same time
                Duration = 60,
                Note = "note"
            };

            // act & assert: IsCoachAvailableAtTheTimeAsync runs before DateValidationAsync so conflict causes exception
            var ex = Assert.ThrowsAsync<ArgumentException>(() => service.CreateReservationAsync(existingUser.Id, model));
            Assert.That(ex.Message, Does.Contain("available").IgnoreCase);

            // ensure not created
            var anyNew = dbContext.Reservations.Count(r => r.PlayerId == existingUser.Id);
            Assert.That(anyNew, Is.EqualTo(0));
        }

        [Test]
        public async Task CreateReservationAsync_DateValidationFails_Throws()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);
            await SeedBasicEntitiesAsync();

            // Create model with past date (so DateValidationAsync will throw)
            var model = new ReservationCreateInputModel
            {
                CoachId = 1,
                SurfaceId = 1,
                TrainingTypeId = 1,
                Date = DateTime.Now.AddHours(-5),
                Duration = 60,
                Note = "note"
            };

            // To avoid coach-availability throwing first, ensure no conflicting reservation exists
            // act & assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => service.CreateReservationAsync(existingUser.Id, model));
            Assert.That(ex.Message, Does.Contain(PastDateErrorMessage).IgnoreCase);
        }

        [Test]
        public async Task CreateReservationAsync_SuccessfulCreation_ReturnsTrueAndPersists()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);
            await SeedBasicEntitiesAsync();

            var model = new ReservationCreateInputModel
            {
                CoachId = 1,
                SurfaceId = 1,
                TrainingTypeId = 1,
                Date = DateTime.Now.AddDays(3).Date.AddHours(9), // valid time
                Duration = 60,
                Note = "ok"
            };

            // act
            var res = await service.CreateReservationAsync(existingUser.Id, model);

            // assert
            Assert.That(res, Is.True);

            var created = dbContext.Reservations.FirstOrDefault(r => r.PlayerId == existingUser.Id);
            Assert.That(created, Is.Not.Null);
            Assert.That(created.CoachId, Is.EqualTo(1));
            Assert.That(created.SurfaceId, Is.EqualTo(1));
            Assert.That(created.TrainingTypeId, Is.EqualTo(1));
            Assert.That(created.Note, Is.EqualTo("ok"));
        }


        [Test]
        public async Task AutoReservationDeleteAsync_WhenNoExpired_ReturnsFalse()
        {
            // arrange - future reservation only
            await dbContext.Reservations.AddAsync(new Reservation
            {
                Id = 401,
                Date = DateTime.Now.AddHours(2),
                PlayerId = existingUser.Id,
                IsDeleted = false
            });
            await dbContext.SaveChangesAsync();

            // act
            var result = await service.AutoReservationDeleteAsync();

            // assert
            Assert.That(result, Is.False);
            var r = await dbContext.Reservations.FindAsync(401);
            Assert.That(r.IsDeleted, Is.False);
        }

        [Test]
        public async Task AutoReservationDeleteAsync_WhenExpired_MarksDeletedAndReturnsTrue()
        {
            // arrange
            await dbContext.Reservations.AddAsync(new Reservation
            {
                Id = 402,
                Date = DateTime.Now.AddMinutes(-10),
                PlayerId = existingUser.Id,
                IsDeleted = false
            });
            await dbContext.SaveChangesAsync();

            // act
            var result = await service.AutoReservationDeleteAsync();

            // assert
            Assert.That(result, Is.True);
            var r = await dbContext.Reservations.FindAsync(402);
            Assert.That(r.IsDeleted, Is.True);
        }


        [Test]
        public void GetUserReservationDetailsAsync_UserNull_Throws()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser)null);

            // act & assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => service.GetUserReservationDetailsAsync("no", 1));
            Assert.That(ex.Message, Does.Contain("Please log in and try again").IgnoreCase);
        }

        [Test]
        public async Task GetUserReservationDetailsAsync_IdNull_ReturnsNull()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);

            // act
            var result = await service.GetUserReservationDetailsAsync(existingUser.Id, null);

            // assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetUserReservationDetailsAsync_ReservationNotFound_Throws()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);

            // act & assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => service.GetUserReservationDetailsAsync(existingUser.Id, 9999));
            Assert.That(ex.Message, Does.Contain("not found").IgnoreCase);
        }

        [Test]
        public async Task GetUserReservationDetailsAsync_Success_ReturnsViewModel()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);
            await SeedBasicEntitiesAsync();

            var reservation = new Reservation
            {
                Id = 501,
                PlayerId = existingUser.Id,
                CoachId = 1,
                SurfaceId = 1,
                TrainingTypeId = 1,
                Date = DateTime.Now.AddDays(2),
                Duration = 120,
                Note = "hello"
            };
            await dbContext.Reservations.AddAsync(reservation);
            await dbContext.SaveChangesAsync();

            // act
            var details = await service.GetUserReservationDetailsAsync(existingUser.Id, 501);

            // assert
            Assert.That(details, Is.Not.Null);
            Assert.That(details.CoachName, Is.EqualTo("Coach1"));
            Assert.That(details.SurfaceName, Is.EqualTo("Court1"));
            Assert.That(details.ImageUrl, Is.EqualTo("img"));
            Assert.That(details.Duration, Is.EqualTo(120));
            Assert.That(details.Note, Is.EqualTo("hello"));
        }


        [Test]
        public void GetUserReservationForDeletingAsync_UserNull_Throws()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser)null);

            // act & assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => service.GetUserReservationForDeletingAsync("no", 1));
            Assert.That(ex.Message, Does.Contain("Please log in and try again").IgnoreCase);
        }

        [Test]
        public async Task GetUserReservationForDeletingAsync_IdNull_ReturnsNull()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);

            // act
            var result = await service.GetUserReservationForDeletingAsync(existingUser.Id, null);

            // assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetUserReservationForDeletingAsync_ReservationNotFound_Throws()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);

            // act & assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => service.GetUserReservationForDeletingAsync(existingUser.Id, 9999));
            Assert.That(ex.Message, Does.Contain("not found").IgnoreCase);
        }

        [Test]
        public async Task GetUserReservationForDeletingAsync_Success_ReturnsDeleteViewModel()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);
            await SeedBasicEntitiesAsync();

            var reservation = new Reservation
            {
                Id = 601,
                PlayerId = existingUser.Id,
                CoachId = 1,
                SurfaceId = 1,
                TrainingTypeId = 1,
                Date = DateTime.Now.AddDays(3),
                IsDeleted = false
            };
            await dbContext.Reservations.AddAsync(reservation);
            await dbContext.SaveChangesAsync();

            // act
            var model = await service.GetUserReservationForDeletingAsync(existingUser.Id, 601);

            // assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Id, Is.EqualTo(601));
            Assert.That(model.SurfaceName, Is.EqualTo("Court1"));
            Assert.That(model.ImageUrl, Is.EqualTo("img"));
        }


        [Test]
        public void DeleteReservationAsync_UserNull_Throws()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser)null);

            var model = new ReservationDeleteViewModel { Id = 701 };

            // act & assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => service.DeleteReservationAsync("no", model));
            Assert.That(ex.Message, Does.Contain("Please log in and try again").IgnoreCase);
        }

        [Test]
        public void DeleteReservationAsync_ReservationNull_Throws()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);

            var model = new ReservationDeleteViewModel { Id = 702 };

            // act & assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => service.DeleteReservationAsync(existingUser.Id, model));
            Assert.That(ex.Message, Does.Contain("not found").IgnoreCase);
        }

        [Test]
        public async Task DeleteReservationAsync_PlayerMatches_MarksDeleted_ReturnsTrue()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);

            var reservation = new Reservation
            {
                Id = 703,
                PlayerId = existingUser.Id,
                Date = DateTime.Now.AddDays(1),
                IsDeleted = false
            };
            await dbContext.Reservations.AddAsync(reservation);
            await dbContext.SaveChangesAsync();

            var model = new ReservationDeleteViewModel { Id = 703 };

            // act
            var result = await service.DeleteReservationAsync(existingUser.Id, model);

            // assert
            Assert.That(result, Is.True);
            var r = await dbContext.Reservations.FindAsync(703);
            Assert.That(r.IsDeleted, Is.True);
        }

        [Test]
        public async Task DeleteReservationAsync_PlayerDifferent_ReturnsTrueButDoesNotDelete()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);

            var reservation = new Reservation
            {
                Id = 704,
                PlayerId = "another-user",
                Date = DateTime.Now.AddDays(1),
                IsDeleted = false
            };
            await dbContext.Reservations.AddAsync(reservation);
            await dbContext.SaveChangesAsync();

            var model = new ReservationDeleteViewModel { Id = 704 };

            // act
            var result = await service.DeleteReservationAsync(existingUser.Id, model);

            // assert: service returns true (per implementation) but reservation not deleted
            Assert.That(result, Is.True);
            var r = await dbContext.Reservations.FindAsync(704);
            Assert.That(r.IsDeleted, Is.False);
        }



        [Test]
        public async Task GetUserReservationHistoryAsync_ReturnsOnlyDeletedReservations()
        {
            // arrange
            userManagerMock.Setup(u => u.FindByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);
            await SeedBasicEntitiesAsync();

            await dbContext.Reservations.AddRangeAsync(new Reservation
            {
                Id = 801,
                PlayerId = existingUser.Id,
                CoachId = 1,
                SurfaceId = 1,
                TrainingTypeId = 1,
                IsDeleted = true
            },
            new Reservation
            {
                Id = 802,
                PlayerId = existingUser.Id,
                CoachId = 1,
                SurfaceId = 1,
                TrainingTypeId = 1,
                IsDeleted = false
            });
            await dbContext.SaveChangesAsync();

            // act
            var history = await service.GetUserReservationHistoryAsync(existingUser.Id);

            // assert
            Assert.That(history, Is.Not.Null);
            Assert.That(history.Count(), Is.EqualTo(1));
            Assert.That(history.First().ReservationId, Is.EqualTo(801));
            Assert.That(history.First().CoachName, Is.EqualTo("Coach1"));
        }


        [Test]
        public void DateValidationAsync_PastDate_Throws()
        {
            // arrange
            var model = new ReservationCreateInputModel
            {
                Date = DateTime.Now.AddMinutes(-5),
                Duration = 60
            };

            // act & assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => service.DateValidationAsync(model));
            Assert.That(ex.Message, Does.Contain(PastDateErrorMessage).IgnoreCase);
        }

        [Test]
        public void DateValidationAsync_LessThanTwoHours_Throws()
        {
            // arrange
            var model = new ReservationCreateInputModel
            {
                Date = DateTime.Now.AddMinutes(60), // less than 2 hours from now
                Duration = 60
            };

            // act & assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => service.DateValidationAsync(model));
            Assert.That(ex.Message, Does.Contain("two").IgnoreCase.Or.Contains("hours").IgnoreCase);
        }

        [Test]
        public void DateValidationAsync_MoreThan14Days_Throws()
        {
            // arrange
            var model = new ReservationCreateInputModel
            {
                Date = DateTime.Now.AddDays(15),
                Duration = 60
            };

            // act & assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => service.DateValidationAsync(model));
            Assert.That(ex.Message, Does.Contain("future").IgnoreCase.Or.Contains("14").IgnoreCase);
        }

        [Test]
        public void DateValidationAsync_Sunday_Throws()
        {
            // arrange: find next Sunday
            var nextSunday = DateTime.Now;
            while (nextSunday.DayOfWeek != DayOfWeek.Sunday)
                nextSunday = nextSunday.AddDays(1);

            nextSunday = new DateTime(nextSunday.Year, nextSunday.Month, nextSunday.Day, 10, 0, 0);

            var model = new ReservationCreateInputModel
            {
                Date = nextSunday,
                Duration = 60
            };

            // act & assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => service.DateValidationAsync(model));
            Assert.That(ex.Message, Does.Contain("Sunday").IgnoreCase);
        }

        [Test]
        public void DateValidationAsync_Before8OrAfter20_Throws()
        {
            // arrange: start before 8:00
            var today = DateTime.Now.AddDays(3).Date;
            var before8 = today.AddHours(7); // 07:00
            var modelBefore = new ReservationCreateInputModel { Date = before8, Duration = 60 };

            var after20Start = today.AddHours(20).AddMinutes(1); // start after 20:00
            var modelAfter = new ReservationCreateInputModel { Date = after20Start, Duration = 60 };

            // act & assert before 8
            var ex1 = Assert.ThrowsAsync<ArgumentException>(() => service.DateValidationAsync(modelBefore));
            Assert.That(ex1.Message, Does.Contain("Selected time").IgnoreCase.Or.Contains("time").IgnoreCase);

            // act & assert after 20
            var ex2 = Assert.ThrowsAsync<ArgumentException>(() => service.DateValidationAsync(modelAfter));
            Assert.That(ex2.Message, Does.Contain("Selected time").IgnoreCase.Or.Contains("time").IgnoreCase);
        }

        [Test]
        public void DateValidationAsync_ValidDate_DoesNotThrow()
        {
            // arrange - valid date: >2 hours, within 14 days, not sunday, between 8 and 20
            var target = DateTime.Now.AddDays(3).Date.AddHours(10);
            var model = new ReservationCreateInputModel { Date = target, Duration = 60 };

            // act & assert: no exception
            Assert.That(async () => await service.DateValidationAsync(model), Throws.Nothing);
        }


        [Test]
        public async Task IsCoachAvailableAtTheTimeAsync_WhenFree_ReturnsTrue()
        {
            // arrange
            var model = new ReservationCreateInputModel
            {
                CoachId = 10,
                Date = DateTime.Now.AddDays(3).Date.AddHours(9),
                Duration = 60
            };

            // ensure no conflicting reservations
            // act
            var result = await service.IsCoachAvailableAtTheTimeAsync(model);

            // assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task IsCoachAvailableAtTheTimeAsync_WhenConflict_Throws()
        {
            // arrange
            var start = DateTime.Now.AddDays(3).Date.AddHours(12);
            var existing = new Reservation
            {
                CoachId = 20,
                Date = start,
                PlayerId = existingUser.Id,
                Duration = 60
            };
            await dbContext.Reservations.AddAsync(existing);
            await dbContext.SaveChangesAsync();

            var model = new ReservationCreateInputModel
            {
                CoachId = 20,
                Date = start.AddMinutes(30), // overlaps
                Duration = 60
            };

            // act & assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => service.IsCoachAvailableAtTheTimeAsync(model));
            Assert.That(ex.Message, Does.Contain("not available").IgnoreCase.Or.Contains("Coach").IgnoreCase);
        }
        [TearDown]
        public void TearDown()
        {
            // Clean up in-memory database after each test
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
            userStoreMock = null;
            userManagerMock = null;
            service = null;
        }
    }
}
