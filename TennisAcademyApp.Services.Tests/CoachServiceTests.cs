//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Moq;
//using NUnit.Framework;
//using TennisAcademyApp.Data;
//using TennisAcademyApp.Data.Models;
//using TennisAcademyApp.Services.Core;
//using TennisAcademyApp.ViewModels.Coach;
//using static TennisAcademyApp.GCommon.Validations.ErrorMessages.Coach;

//namespace TennisAcademyApp.Tests
//{
//    public class CoachServiceTests
//    {
//        private TennisAcademyDbContext dbContext;
//        private Mock<UserManager<ApplicationUser>> userManagerMock = null!;
//        private CoachService coachService = null!;

//        [SetUp]
//        public void Setup()
//        {
//            var options = new DbContextOptionsBuilder<TennisAcademyDbContext>()
//                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//                .Options;

//            dbContext = new TennisAcademyDbContext(options);

//            dbContext.Coaches.AddRange(new List<Coach>
//            {
//                new Coach { CoachId = 1, Name = "John Doe", Age = 35, Description = "Desc4444444441", ImageUrl = null, Nationality = "USA" },
//                new Coach { CoachId = 2, Name = "Jane Smith", Age = 40, Description = "Desc4444444442", ImageUrl = "image2.jpg", Nationality = "UK" }
//            });
//            dbContext.UserFavourites.Add(new UserFavourite { UserId = "user1", CoachId = 1 });
//            dbContext.SaveChanges();

//            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
//            userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object,
//                null, null, null, null, null, null, null, null);

//            var inMemorySettings = new Dictionary<string, string> {
//            {"CoachSettings:DefaultPassword", "TestCoachPassword123!"}
//            };

//            IConfiguration configuration = new ConfigurationBuilder()
//                .AddInMemoryCollection(inMemorySettings)
//                .Build();

//            coachService = new CoachService(dbContext, userManagerMock.Object, configuration);
//        }

//        [Test]
//        public async Task GetCoachDetailsAsync_ReturnsCorrectDetails_AndIsInUserFavorites()
//        {
//            var user = new ApplicationUser { Id = "user1" };
//            userManagerMock.Setup(um => um.FindByIdAsync("user1")).ReturnsAsync(user);

//            var result = await coachService.GetCoachDetailsAsync("user1", 1);

//            Assert.That(result, Is.Not.Null);
//            Assert.That(result.CoachId, Is.EqualTo(1));
//            Assert.That(result.CoachName, Is.EqualTo("John Doe"));
//            Assert.That(result.CoachAge, Is.EqualTo(35));
//            Assert.That(result.Description, Is.EqualTo("Desc4444444441"));
//            Assert.That(result.ImageUrl, Is.Null.Or.Empty);
//            Assert.That(result.Nationality, Is.EqualTo("USA"));
//            Assert.That(result.IsInUserFavorites, Is.True);
//        }

//        [Test]
//        public void GetCoachDetailsAsync_Throws_WhenCoachNotFound()
//        {
//            var user = new ApplicationUser { Id = "user1" };
//            userManagerMock.Setup(um => um.FindByIdAsync("user1")).ReturnsAsync(user);

//            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
//                await coachService.GetCoachDetailsAsync("user1", 999));

//            Assert.That(ex.Message, Is.EqualTo(CoachNotFoundErrorMessage));
//        }

//        [Test]
//        public void AddCoachAsync_Throws_WhenUserIsNotAdmin()
//        {
//            var user = new ApplicationUser { Id = "user1" };
//            userManagerMock.Setup(um => um.FindByIdAsync("user1")).ReturnsAsync(user);
//            userManagerMock.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

//            var inputModel = new AddCoachInputModel
//            {
//                Name = "New Coach",
//                Age = 29,
//                Description = "Desc",
//                ImageUrl = "img.jpg",
//                Nationality = "BG"
//            };

//            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
//                await coachService.AddCoachAsync("user1", inputModel));

//            Assert.That(ex.Message, Is.EqualTo("You must be an admin to add a coach."));
//        }

//        [Test]
//        public async Task AddCoachAsync_AddsCoach_WhenUserIsAdmin()
//        {
//            var user = new ApplicationUser { Id = "adminUser" };
//            userManagerMock.Setup(um => um.FindByIdAsync("adminUser")).ReturnsAsync(user);
//            userManagerMock.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

//            var inputModel = new AddCoachInputModel
//            {
//                Name = "New Coach",
//                Age = 29,
//                Description = "Desc",
//                ImageUrl = "img.jpg",
//                Nationality = "BG"
//            };

//            var result = await coachService.AddCoachAsync("adminUser", inputModel);

//            Assert.That(result, Is.True);
//            Assert.That(await dbContext.Coaches.AnyAsync(c => c.Name == "New Coach"), Is.True);
//        }

//        [Test]
//        public void GetCoachForEdittingAsync_Throws_WhenUserIsNotAdmin()
//        {
//            var user = new ApplicationUser { Id = "user1" };
//            userManagerMock.Setup(um => um.FindByIdAsync("user1")).ReturnsAsync(user);
//            userManagerMock.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

//            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
//                await coachService.GetCoachForEdittingAsync("user1", 1));

//            Assert.That(ex.Message, Is.EqualTo("You must be an admin to edit a coach."));
//        }

//        [Test]
//        public async Task GetCoachForEdittingAsync_ReturnsCorrectModel_WhenUserIsAdmin()
//        {
//            var user = new ApplicationUser { Id = "adminUser" };
//            userManagerMock.Setup(um => um.FindByIdAsync("adminUser")).ReturnsAsync(user);
//            userManagerMock.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

//            var result = await coachService.GetCoachForEdittingAsync("adminUser", 1);

//            Assert.That(result, Is.Not.Null);
//            Assert.That(result.CoachId, Is.EqualTo(1));
//            Assert.That(result.Name, Is.EqualTo("John Doe"));
//            Assert.That(result.Age, Is.EqualTo(35));
//            Assert.That(result.Description, Is.EqualTo("Desc4444444441"));
//            Assert.That(result.ImageUrl, Is.Null.Or.Empty);
//            Assert.That(result.Nationality, Is.EqualTo("USA"));
//        }


//        [Test]
//        public void EdittedCoachAsync_Throws_WhenCoachNotFound()
//        {
//            var user = new ApplicationUser { Id = "user1" };
//            userManagerMock.Setup(um => um.FindByIdAsync("user1")).ReturnsAsync(user);

//            var model = new CoachEditInputModel
//            {
//                CoachId = 999,
//                Name = "Edited Name",
//                Age = 36,
//                Description = "Edited Desc",
//                ImageUrl = "img.jpg",
//                Nationality = "BG"
//            };

//            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
//                await coachService.EdittedCoachAsync("user1", model));

//            Assert.That(ex.Message, Is.EqualTo(CoachNotFoundErrorMessage));
//        }

//        [Test]
//        public async Task EdittedCoachAsync_UpdatesCoach_WhenValid()
//        {
//            var user = new ApplicationUser { Id = "user1" };
//            userManagerMock.Setup(um => um.FindByIdAsync("user1")).ReturnsAsync(user);

//            var model = new CoachEditInputModel
//            {
//                CoachId = 1,
//                Name = "Edited Name",
//                Age = 36,
//                Description = "Edited Desc",
//                ImageUrl = "img.jpg",
//                Nationality = "BG"
//            };

//            var result = await coachService.EdittedCoachAsync("user1", model);

//            Assert.That(result, Is.True);

//            var updatedCoach = await dbContext.Coaches.FindAsync(1);
//            Assert.That(updatedCoach!.Name, Is.EqualTo("Edited Name"));
//            Assert.That(updatedCoach.Age, Is.EqualTo(36));
//            Assert.That(updatedCoach.Description, Is.EqualTo("Edited Desc"));
//            Assert.That(updatedCoach.ImageUrl, Is.EqualTo("img.jpg"));
//            Assert.That(updatedCoach.Nationality, Is.EqualTo("BG"));
//        }

//        [Test]
//        public void GetCoachForDeletingAsync_Throws_WhenUserIsNotAdmin()
//        {
//            var user = new ApplicationUser { Id = "user1" };
//            userManagerMock.Setup(um => um.FindByIdAsync("user1")).ReturnsAsync(user);
//            userManagerMock.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

//            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
//                await coachService.GetCoachForDeletingAsync("user1", 1));

//            Assert.That(ex.Message, Is.EqualTo("You must be an admin to delete a coach."));
//        }

//        [Test]
//        public async Task GetCoachForDeletingAsync_ReturnsCorrectModel_WhenUserIsAdmin()
//        {
//            var user = new ApplicationUser { Id = "adminUser" };
//            userManagerMock.Setup(um => um.FindByIdAsync("adminUser")).ReturnsAsync(user);
//            userManagerMock.Setup(um => um.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

//            var result = await coachService.GetCoachForDeletingAsync("adminUser", 1);

//            Assert.That(result, Is.Not.Null);
//            Assert.That(result.CoachId, Is.EqualTo(1));
//            Assert.That(result.Name, Is.EqualTo("John Doe"));
//            Assert.That(result.ImageUrl, Is.Null.Or.Empty);
//        }

//        [Test]
//        public void DeletedCoachAsync_Throws_WhenCoachNotFound()
//        {
//            var user = new ApplicationUser { Id = "user1" };
//            userManagerMock.Setup(um => um.FindByIdAsync("user1")).ReturnsAsync(user);

//            var model = new DeleteCoachViewModel { CoachId = 999, Name = "Nonexistent", ImageUrl = null };

//            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
//                await coachService.DeletedCoachAsync("user1", model));

//            Assert.That(ex.Message, Is.EqualTo(CoachNotFoundErrorMessage));
//        }

//        [Test]
//        public async Task DeletedCoachAsync_DeletesCoach_WhenValid()
//        {
//            var user = new ApplicationUser { Id = "user1" };
//            userManagerMock.Setup(um => um.FindByIdAsync("user1")).ReturnsAsync(user);

//            var coach = new Coach 
//            { 
//                CoachId = 3, 
//                Name = "John Doe", 
//                Age = 35, 
//                Description = "Desc4444444441", 
//                ImageUrl = null,
//                Nationality = "USA" 
//            };  
//            dbContext.Coaches.Add(coach);
//            await dbContext.SaveChangesAsync();

//            var model = new DeleteCoachViewModel 
//            { 
//                CoachId = coach.CoachId, 
//                Name = coach.Name, 
//                ImageUrl = coach.ImageUrl 
//            };

//            var result = await coachService.DeletedCoachAsync("user1", model);

//            Assert.That(result, Is.True);
//            Assert.That(await dbContext.Coaches.FindAsync(3), Is.Null);
//        }

//        [Test]
//        public void GetCoachByIdAsync_Throws_WhenIdIsNull()
//        {
//            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
//                await coachService.GetCoachByIdAsync(null));

//            Assert.That(ex.Message, Is.EqualTo(CoachCannotBeNullErrorMessage));
//        }

//        [Test]
//        public void GetCoachByIdAsync_Throws_WhenCoachNotFound()
//        {
//            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
//                await coachService.GetCoachByIdAsync(999));

//            Assert.That(ex.Message, Is.EqualTo(CoachNotFoundErrorMessage));
//        }

//        [Test]
//        public async Task GetCoachByIdAsync_ReturnsCoach_WhenFound()
//        {
//            var coach = await coachService.GetCoachByIdAsync(1);

//            Assert.That(coach, Is.Not.Null);
//            Assert.That(coach.CoachId, Is.EqualTo(1));
//            Assert.That(coach.Name, Is.EqualTo("John Doe"));
//        }
//        [TearDown]
//        public void TearDown()
//        {
//            dbContext.Database.EnsureDeleted();
//            dbContext.Dispose();
//        }
//    }
//}
