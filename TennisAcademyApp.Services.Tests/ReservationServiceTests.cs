//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Diagnostics;
//using Moq;
//using NUnit.Framework;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using TennisAcademyApp.Data;
//using TennisAcademyApp.Data.Models;
//using TennisAcademyApp.Services.Core;
//using TennisAcademyApp.Services.Core.Contracts;
//using TennisAcademyApp.ViewModels.Reservation;

//namespace TennisAcademyApp.Tests.Services
//{
//    [TestFixture]
//    public class ReservationServiceTests
//    {
//        private TennisAcademyDbContext dbContext;
//        private Mock<UserManager<ApplicationUser>> userManagerMock;
//        private Mock<IDateTimeProvider> dateTimeMock;
//        private ReservationService service;
//        private ApplicationUser existingUser;
//        private DateTime fixedNow;

//        [SetUp]
//        public void SetUp()
//        {
//            var options = new DbContextOptionsBuilder<TennisAcademyDbContext>()
//                .UseInMemoryDatabase(Guid.NewGuid().ToString())
//                .ConfigureWarnings(x =>
//                {
//                    x.Ignore(InMemoryEventId.TransactionIgnoredWarning);
//                    x.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning);
//                })
//                .EnableSensitiveDataLogging()
//                .Options;

//            dbContext = new TennisAcademyDbContext(options);

//            var store = new Mock<IUserStore<ApplicationUser>>();

//            userManagerMock =
//                new Mock<UserManager<ApplicationUser>>
//                (
//                    store.Object,
//                    null,
//                    null,
//                    null,
//                    null,
//                    null,
//                    null,
//                    null,
//                    null
//                );

//            fixedNow = new DateTime(2026, 5, 22, 10, 0, 0);

//            dateTimeMock = new Mock<IDateTimeProvider>();

//            dateTimeMock
//                .Setup(x => x.Now)
//                .Returns(() => fixedNow);

//            service = new ReservationService(
//                dbContext,
//                userManagerMock.Object,
//                dateTimeMock.Object);

//            existingUser = new ApplicationUser
//            {
//                Id = "user-1",
//                UserName = "tester",
//                Email = "tester@test.com"
//            };

//            dbContext.Database.EnsureCreated();
//        }

//        private async Task SeedBasicEntitiesAsync()
//        {
//            if (await dbContext.Coaches.AnyAsync())
//                return;

//            await dbContext.AddRangeAsync(
//                new Coach
//                {
//                    CoachId = 1,
//                    Name = "Coach1",
//                    NameBg = "Треньор1"
//                },

//                new Surface
//                {
//                    Id = 1,
//                    Name = "Court1",
//                    NameBg = "Корт1",
//                    ImageUrl = "img"
//                },

//                new TrainingType
//                {
//                    Id = 1,
//                    Name = "Tennis",
//                    NameBg = "Тенис"
//                });

//            await dbContext.SaveChangesAsync();
//        }

//        [Test]
//        public async Task GetUserReservationsAsync_WhenUserHasReservations_ReturnsListMapped()
//        {
//            userManagerMock
//                .Setup(x => x.FindByIdAsync(existingUser.Id))
//                .ReturnsAsync(existingUser);

//            await SeedBasicEntitiesAsync();

//            await dbContext.Reservations.AddAsync(
//                new Reservation
//                {
//                    Id = 11,
//                    PlayerId = existingUser.Id,
//                    CoachId = 1,
//                    SurfaceId = 1,
//                    TrainingTypeId = 1,
//                    Date = fixedNow.AddDays(2),
//                    Duration = 60
//                });

//            await dbContext.SaveChangesAsync();

//            var result =
//                await service.GetUserReservationsAsync(existingUser.Id);

//            Assert.That(result, Is.Not.Null);
//            Assert.That(result.Count(), Is.EqualTo(1));
//            Assert.That(result.First().ReservationId, Is.EqualTo(11));
//        }

//        [Test]
//        public async Task GetUserReservationsAsync_ExpiredReservations_AutoDeleted()
//        {
//            userManagerMock
//                .Setup(x => x.FindByIdAsync(existingUser.Id))
//                .ReturnsAsync(existingUser);

//            await SeedBasicEntitiesAsync();

//            await dbContext.Reservations.AddRangeAsync(

//                new Reservation
//                {
//                    Id = 21,
//                    PlayerId = existingUser.Id,
//                    CoachId = 1,
//                    SurfaceId = 1,
//                    TrainingTypeId = 1,
//                    Date = fixedNow.AddHours(-1),
//                    Duration = 60,
//                    IsCompleted = false
//                },

//                new Reservation
//                {
//                    Id = 22,
//                    PlayerId = existingUser.Id,
//                    CoachId = 1,
//                    SurfaceId = 1,
//                    TrainingTypeId = 1,
//                    Date = fixedNow.AddDays(1),
//                    Duration = 60,
//                    IsCompleted = false
//                });

//            await dbContext.SaveChangesAsync();

//            var result =
//                await service.GetUserReservationsAsync(existingUser.Id);

//            Assert.That(result.Count(), Is.EqualTo(1));

//            Assert.That(
//                result.First().ReservationId,
//                Is.EqualTo(22));

//            var oldReservation =
//                await dbContext.Reservations.FindAsync(21);

//            Assert.That(oldReservation.IsCompleted, Is.True);
//        }

//        [Test]
//        public async Task CreateReservationAsync_Success_ReturnsTrue()
//        {
//            userManagerMock
//                .Setup(x => x.FindByIdAsync(existingUser.Id))
//                .ReturnsAsync(existingUser);

//            await SeedBasicEntitiesAsync();

//            var model =
//                new ReservationCreateInputModel
//                {
//                    CoachId = 1,
//                    SurfaceId = 1,
//                    TrainingTypeId = 1,
//                    Date = fixedNow.AddHours(3),
//                    Duration = 60
//                };

//            var result =
//                await service.CreateReservationAsync(
//                    existingUser.Id,
//                    model);

//            Assert.That(result, Is.True);

//            Assert.That(
//                dbContext.Reservations.Count(),
//                Is.EqualTo(1));
//        }

//        [Test]
//        public void CreateReservationAsync_InvalidUser_Throws()
//        {
//            userManagerMock
//                .Setup(x => x.FindByIdAsync("missing"))
//                .ReturnsAsync((ApplicationUser)null);

//            var model =
//                new ReservationCreateInputModel
//                {
//                    CoachId = 1,
//                    SurfaceId = 1,
//                    TrainingTypeId = 1,
//                    Date = fixedNow.AddHours(3),
//                    Duration = 60
//                };

//            Assert.ThrowsAsync<ArgumentException>(
//                async () =>
//                await service.CreateReservationAsync(
//                    "missing",
//                    model));
//        }

//        [Test]
//        public async Task CreateReservationAsync_WhenCoachBusy_Throws()
//        {
//            userManagerMock
//                .Setup(x => x.FindByIdAsync(existingUser.Id))
//                .ReturnsAsync(existingUser);

//            await SeedBasicEntitiesAsync();

//            await dbContext.Reservations.AddAsync(
//                new Reservation
//                {
//                    CoachId = 1,
//                    SurfaceId = 1,
//                    TrainingTypeId = 1,
//                    Date = fixedNow.AddHours(3),
//                    Duration = 60
//                });

//            await dbContext.SaveChangesAsync();

//            var model =
//                new ReservationCreateInputModel
//                {
//                    CoachId = 1,
//                    SurfaceId = 1,
//                    TrainingTypeId = 1,
//                    Date = fixedNow.AddHours(3)
//                        .AddMinutes(30),
//                    Duration = 60
//                };

//            Assert.ThrowsAsync<ArgumentException>(
//                async () =>
//                await service.CreateReservationAsync(
//                    existingUser.Id,
//                    model));
//        }

//        [Test]
//        public async Task CreateReservationAsync_InvalidDuration_Throws()
//        {
//            userManagerMock
//                .Setup(x => x.FindByIdAsync(existingUser.Id))
//                .ReturnsAsync(existingUser);

//            await SeedBasicEntitiesAsync();

//            var model =
//                new ReservationCreateInputModel
//                {
//                    CoachId = 1,
//                    SurfaceId = 1,
//                    TrainingTypeId = 1,
//                    Date = fixedNow.AddHours(3),
//                    Duration = 70
//                };

//            Assert.ThrowsAsync<ArgumentException>(
//                async () =>
//                await service.CreateReservationAsync(
//                    existingUser.Id,
//                    model));
//        }

//        [Test]
//        public void DateValidationAsync_PastDate_Throws()
//        {
//            var model =
//                new ReservationCreateInputModel
//                {
//                    Date = fixedNow.AddMinutes(-5),
//                    Duration = 60
//                };

//            Assert.ThrowsAsync<ArgumentException>(
//                async () =>
//                await service.DateValidationAsync(model));
//        }

//        [Test]
//        public void DateValidationAsync_LessThanTwoHours_Throws()
//        {
//            var model =
//                new ReservationCreateInputModel
//                {
//                    Date = fixedNow.AddMinutes(30),
//                    Duration = 60
//                };

//            Assert.ThrowsAsync<ArgumentException>(
//                async () =>
//                await service.DateValidationAsync(model));
//        }

//        [Test]
//        public async Task DateValidation_ExactlyTwoHours_Works()
//        {
//            var model =
//                new ReservationCreateInputModel
//                {
//                    Date = fixedNow.AddHours(2),
//                    Duration = 60
//                };

//            Assert.DoesNotThrowAsync(
//                async () =>
//                await service.DateValidationAsync(model));
//        }

//        [Test]
//        public void DateValidationAsync_Sunday_Throws()
//        {
//            var model =
//                new ReservationCreateInputModel
//                {
//                    Date = new DateTime(
//                        2026,
//                        5,
//                        24,
//                        10,
//                        0,
//                        0),
//                    Duration = 60
//                };

//            Assert.ThrowsAsync<ArgumentException>(
//                async () =>
//                await service.DateValidationAsync(model));
//        }

//        [Test]
//        public void DateValidation_BeforeOpening_Throws()
//        {
//            var model =
//                new ReservationCreateInputModel
//                {
//                    Date = new DateTime(
//                        2026,
//                        5,
//                        23,
//                        7,
//                        0,
//                        0),
//                    Duration = 60
//                };

//            Assert.ThrowsAsync<ArgumentException>(
//                async () =>
//                await service.DateValidationAsync(model));
//        }

//        [Test]
//        public void DateValidation_AfterClosing_Throws()
//        {
//            var model =
//                new ReservationCreateInputModel
//                {
//                    Date = new DateTime(
//                        2026,
//                        5,
//                        23,
//                        19,
//                        30,
//                        0),
//                    Duration = 60
//                };

//            Assert.ThrowsAsync<ArgumentException>(
//                async () =>
//                await service.DateValidationAsync(model));
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            dbContext.ChangeTracker.Clear();
//            dbContext.Dispose();
//        }
//    }
//}