//using NUnit.Framework;
//using Moq;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using TennisAcademyApp.Data;
//using TennisAcademyApp.Data.Models;
//using TennisAcademyApp.Services.Core;
//using static TennisAcademyApp.GCommon.Validations.ErrorMessages.UserManagement;

//namespace TennisAcademyApp.Tests.Services
//{
//    [TestFixture]
//    public class UserServiceTests
//    {
//        private TennisAcademyDbContext dbContext;
//        private Mock<UserManager<ApplicationUser>> userManagerMock;
//        private Mock<RoleManager<IdentityRole>> roleManagerMock;
//        private UserService userService;

//        [SetUp]
//        public void SetUp()
//        {
//            var options = new DbContextOptionsBuilder<TennisAcademyDbContext>()
//                .UseInMemoryDatabase(Guid.NewGuid().ToString())
//                .Options;

//            dbContext = new TennisAcademyDbContext(options);

//            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
//            userManagerMock = new Mock<UserManager<ApplicationUser>>(
//                userStoreMock.Object, null, null, null, null, null, null, null, null);

//            var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
//            roleManagerMock = new Mock<RoleManager<IdentityRole>>(
//                roleStoreMock.Object, null, null, null, null);

//            userService = new UserService(userManagerMock.Object, roleManagerMock.Object, dbContext);
//        }

//        [Test]
//        public async Task AssignUserToRoleAsync_ShouldReturnFalse_IfUserNotFoundOrRoleMissing()
//        {
//            userManagerMock.Setup(u => u.FindByIdAsync("user1"))
//                .ReturnsAsync((ApplicationUser?)null);

//            var result = await userService.AssignUserToRoleAsync("user1", "Admin");
//            Assert.That(result, Is.False);

//            userManagerMock.Setup(u => u.FindByIdAsync("user2"))
//                .ReturnsAsync(new ApplicationUser { Id = "user2" });

//            roleManagerMock.Setup(r => r.RoleExistsAsync("InvalidRole"))
//                .ReturnsAsync(false);

//            result = await userService.AssignUserToRoleAsync("user2", "InvalidRole");
//            Assert.That(result, Is.False);
//        }

//        [Test]
//        public void AssignUserToRoleAsync_ShouldThrow_IfUserAlreadyInRole()
//        {
//            var user = new ApplicationUser { Id = "user1" };

//            userManagerMock.Setup(u => u.FindByIdAsync("user1")).ReturnsAsync(user);
//            roleManagerMock.Setup(r => r.RoleExistsAsync("Admin")).ReturnsAsync(true);
//            userManagerMock.Setup(u => u.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

//            Assert.ThrowsAsync<InvalidOperationException>(async () =>
//                await userService.AssignUserToRoleAsync("user1", "Admin"), UserAlreadyInRoleErrorMessage);
//        }

//        [Test]
//        public async Task AssignUserToRoleAsync_ShouldAddRole_WhenNotInRole()
//        {
//            var user = new ApplicationUser { Id = "user1" };

//            userManagerMock.Setup(u => u.FindByIdAsync("user1")).ReturnsAsync(user);
//            roleManagerMock.Setup(r => r.RoleExistsAsync("Admin")).ReturnsAsync(true);
//            userManagerMock.Setup(u => u.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);
//            userManagerMock.Setup(u => u.AddToRoleAsync(user, "Admin")).ReturnsAsync(IdentityResult.Success);

//            var result = await userService.AssignUserToRoleAsync("user1", "Admin");

//            Assert.That(result, Is.True);
//        }

//        [Test]
//        public async Task RemoveUserFromRoleAsync_ShouldReturnFalse_IfUserOrRoleMissing()
//        {
//            userManagerMock.Setup(u => u.FindByIdAsync("user1")).ReturnsAsync((ApplicationUser?)null);
//            var result = await userService.RemoveUserFromRoleAsync("user1", "Admin");
//            Assert.That(result, Is.False);

//            userManagerMock.Setup(u => u.FindByIdAsync("user2")).ReturnsAsync(new ApplicationUser { Id = "user2" });
//            roleManagerMock.Setup(r => r.RoleExistsAsync("InvalidRole")).ReturnsAsync(false);
//            result = await userService.RemoveUserFromRoleAsync("user2", "InvalidRole");
//            Assert.That(result, Is.False);
//        }

//        [Test]
//        public async Task RemoveUserFromRoleAsync_ShouldReturnTrue_WhenRoleRemoved()
//        {
//            var user = new ApplicationUser { Id = "user1" };

//            userManagerMock.Setup(u => u.FindByIdAsync("user1")).ReturnsAsync(user);
//            roleManagerMock.Setup(r => r.RoleExistsAsync("Admin")).ReturnsAsync(true);
//            userManagerMock.Setup(u => u.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);
//            userManagerMock.Setup(u => u.RemoveFromRoleAsync(user, "Admin")).ReturnsAsync(IdentityResult.Success);

//            var result = await userService.RemoveUserFromRoleAsync("user1", "Admin");

//            Assert.That(result, Is.True);
//        }

//        [Test]
//        public async Task RemoveUserAsync_ShouldReturnFalse_IfUserNotFound()
//        {
//            userManagerMock.Setup(u => u.FindByIdAsync("user1")).ReturnsAsync((ApplicationUser?)null);

//            var result = await userService.RemoveUserAsync("user1");

//            Assert.That(result, Is.False);
//        }

//        [Test]
//        public async Task RemoveUserAsync_ShouldRemoveAllRelatedDataAndUser()
//        {
//            var user = new ApplicationUser { Id = "user1" };

//            userManagerMock.Setup(u => u.FindByIdAsync("user1")).ReturnsAsync(user);
//            userManagerMock.Setup(u => u.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

//            // Seed COach
//            var coach = new Coach 
//            { 
//                CoachId = 1, 
//                Name = "Ivan Ivanov", 
//                Age = 40, 
//                Description = "sdjoadjoasdjaodsd", 
//                ImageUrl = "coach.jpg",
//                Nationality = "bulgarin"
//            };
//            await dbContext.Coaches.AddAsync(coach);

//            var racket = new Racket
//            {
//                Id = 1,
//                Brand = "Wilson",
//                Model = "Pro Staff",
//                Price = 250m,
//                Quantity = 5,
//                ImageUrl = "racket.jpg"
//            };
//            await dbContext.Rackets.AddAsync(racket);

//            // Seed Ball
//            var ball = new Ball 
//            {
//                Id = 1,
//                Brand = "Wilson",
//                Model = "Pro Staff",
//                Price = 250m,
//                Quantity = 5,
//                ImageUrl = "racket.jpg"
//            };
//            await dbContext.Balls.AddAsync(ball);

//            // Seed Bag
//            var bag = new Bag 
//            {
//                Id = 1,
//                Brand = "Wilson",
//                Model = "Pro Staff",
//                Price = 250m,
//                Quantity = 5,
//                ImageUrl = "racket.jpg"
//            };
//            await dbContext.Bags.AddAsync(bag);

//            // Seed related user data
//            await dbContext.UserFavourites.AddAsync(new UserFavourite { UserId = "user1", CoachId = coach.CoachId });
//            await dbContext.RacketCart.AddAsync(new RacketCart { UserId = "user1", RacketId = racket.Id, Quantity = 1 });
//            await dbContext.BallCart.AddAsync(new BallCart { UserId = "user1", BallId = ball.Id, Quantity = 2 });
//            await dbContext.BagCart.AddAsync(new BagCart { UserId = "user1", BagId = bag.Id, Quantity = 1 });

//            await dbContext.SaveChangesAsync();

//            var result = await userService.RemoveUserAsync("user1");

//            Assert.That(result, Is.True);

//            Assert.That(await dbContext.UserFavourites.AnyAsync(uf => uf.UserId == "user1"), Is.False);
//            Assert.That(await dbContext.RacketCart.AnyAsync(rc => rc.UserId == "user1"), Is.False);
//            Assert.That(await dbContext.BallCart.AnyAsync(bc => bc.UserId == "user1"), Is.False);
//            Assert.That(await dbContext.BagCart.AnyAsync(bc => bc.UserId == "user1"), Is.False);

//            userManagerMock.Verify(u => u.DeleteAsync(user), Times.Once);
//        }



//        [TearDown]
//        public void TearDown()
//        {
//            dbContext.Dispose();
//        }
//    }
//}
