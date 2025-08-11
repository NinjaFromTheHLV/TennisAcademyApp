using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisAcademyApp.Data;
using TennisAcademyApp.Data.Models;
using TennisAcademyApp.Services.Core;
using TennisAcademyApp.ViewModels.Bag;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.Bag;

namespace TennisAcademyApp.Tests.Services
{
    [TestFixture]
    public class BagServiceTests
    {
        private TennisAcademyDbContext dbContext;
        private Mock<UserManager<IdentityUser>> userManagerMock;
        private BagService bagService;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<TennisAcademyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new TennisAcademyDbContext(options);

            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            userManagerMock = new Mock<UserManager<IdentityUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            bagService = new BagService(dbContext, userManagerMock.Object);
        }

        [Test]
        public async Task GetAllBagsAsync_ShouldReturnAllBagsWithCorrectProperties()
        {
            dbContext.Bags.Add(new Bag
            {
                Id = 1,
                Brand = "Wilson",
                Model = "Pro",
                Price = 150.5m,
                Quantity = 10,
                ImageUrl = "image1.jpg",
                BagCarts = new List<BagCart>()
            });
            dbContext.Bags.Add(new Bag
            {
                Id = 2,
                Brand = "Head",
                Model = "Extreme",
                Price = 200m,
                Quantity = 5,
                ImageUrl = "image2.jpg",
                BagCarts = new List<BagCart>()
            });
            await dbContext.SaveChangesAsync();

            var result = (await bagService.GetAllBagsAsync()).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Brand, Is.EqualTo("Wilson"));
            Assert.That(result[0].ImageUrl, Is.EqualTo("image1.jpg"));
            Assert.That(result[1].Brand, Is.EqualTo("Head"));
            Assert.That(result[1].ImageUrl, Is.EqualTo("image2.jpg"));
        }

        [Test]
        public void FindBagByIdAsync_WithNullId_ShouldThrowArgumentException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await bagService.FindBagByIdAsync(null));
            Assert.That(ex.Message, Is.EqualTo(BagCannotBeNullErrorMessage));
        }

        [Test]
        public void FindBagByIdAsync_WithNonExistingId_ShouldThrowArgumentException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await bagService.FindBagByIdAsync(999));
            Assert.That(ex.Message, Is.EqualTo(BagNotFoundErrorMessage));
        }

        [Test]
        public async Task FindBagByIdAsync_WithValidId_ShouldReturnBag()
        {
            var bag = new Bag
            {
                Id = 1,
                Brand = "Babolat",
                Model = "Classic",
                Price = 120m,
                Quantity = 3,
                ImageUrl = "classic.jpg"
            };
            dbContext.Bags.Add(bag);
            await dbContext.SaveChangesAsync();

            var result = await bagService.FindBagByIdAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Brand, Is.EqualTo("Babolat"));
            Assert.That(result.ImageUrl, Is.EqualTo("classic.jpg"));
        }

        [Test]
        public async Task AddBagAsync_UserIsAdmin_ShouldAddBagAndReturnTrue()
        {
            var user = new IdentityUser { Id = "admin-id" };
            var model = new BagCreateInputModel
            {
                Brand = "Yonex",
                Model = "VCORE",
                Price = 180m,
                Quantity = 7,
                ImageUrl = "yonex.jpg"
            };

            userManagerMock.Setup(x => x.FindByIdAsync("admin-id")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

            var result = await bagService.AddBagAsync("admin-id", model);

            Assert.That(result, Is.True);
            var bagInDb = await dbContext.Bags.FirstOrDefaultAsync(b => b.Brand == "Yonex");
            Assert.That(bagInDb, Is.Not.Null);
            Assert.That(bagInDb.ImageUrl, Is.EqualTo("yonex.jpg"));
        }

        [Test]
        public async Task AddBagAsync_UserNotAdmin_ShouldNotAddBagAndReturnFalse()
        {
            var user = new IdentityUser { Id = "user-id" };
            var model = new BagCreateInputModel
            {
                Brand = "Yonex",
                Model = "VCORE",
                Price = 180m,
                Quantity = 7,
                ImageUrl = "yonex.jpg"
            };

            userManagerMock.Setup(x => x.FindByIdAsync("user-id")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

            var result = await bagService.AddBagAsync("user-id", model);

            Assert.That(result, Is.False);
            var bagInDb = await dbContext.Bags.FirstOrDefaultAsync(b => b.Brand == "Yonex");
            Assert.That(bagInDb, Is.Null);
        }

        [Test]
        public void GetBagForEditingAsync_UserNotAdmin_ShouldThrowException()
        {
            var user = new IdentityUser { Id = "user-id" };

            userManagerMock.Setup(x => x.FindByIdAsync("user-id")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await bagService.GetBagForEditingAsync("user-id", 1));
            Assert.That(ex.Message, Is.EqualTo("You do not have permission to edit bags."));
        }

        [Test]
        public void GetBagForEditingAsync_UserIdNull_ShouldThrowException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await bagService.GetBagForEditingAsync(null, 1));
            Assert.That(ex.Message, Is.EqualTo("You do not have permission to edit bags."));
        }

        [Test]
        public async Task GetBagForEditingAsync_AdminUserWithValidBag_ShouldReturnModel()
        {
            var user = new IdentityUser { Id = "admin-id" };
            var bag = new Bag
            {
                Id = 1,
                Brand = "Wilson",
                Model = "Pro Staff",
                Price = 210m,
                Quantity = 4,
                ImageUrl = "wilson.jpg"
            };
            dbContext.Bags.Add(bag);
            await dbContext.SaveChangesAsync();

            userManagerMock.Setup(x => x.FindByIdAsync("admin-id")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

            var result = await bagService.GetBagForEditingAsync("admin-id", 1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Brand, Is.EqualTo("Wilson"));
            Assert.That(result.ImageUrl, Is.EqualTo("wilson.jpg"));
        }

        [Test]
        public void EditBagAsync_NonExistingBag_ShouldThrowException()
        {
            var model = new BagEditFormModel
            {
                Id = 999,
                Brand = "Fake",
                Model = "None",
                Price = 0m,
                Quantity = 0,
                ImageUrl = "none.jpg"
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await bagService.EditBagAsync(model));
            Assert.That(ex.Message, Is.EqualTo(BagNotFoundErrorMessage));
        }

        [Test]
        public async Task EditBagAsync_ExistingBag_ShouldUpdateAndReturnTrue()
        {
            var bag = new Bag
            {
                Id = 1,
                Brand = "OldBrand",
                Model = "OldModel",
                Price = 100m,
                Quantity = 2,
                ImageUrl = "old.jpg"
            };
            dbContext.Bags.Add(bag);
            await dbContext.SaveChangesAsync();

            var model = new BagEditFormModel
            {
                Id = 1,
                Brand = "NewBrand",
                Model = "NewModel",
                Price = 150m,
                Quantity = 5,
                ImageUrl = "new.jpg"
            };

            var result = await bagService.EditBagAsync(model);

            Assert.That(result, Is.True);
            var updatedBag = await dbContext.Bags.FindAsync(1);
            Assert.That(updatedBag.Brand, Is.EqualTo("NewBrand"));
            Assert.That(updatedBag.ImageUrl, Is.EqualTo("new.jpg"));
        }

        [Test]
        public void GetBagForDeletingAsync_UserNotAdmin_ShouldThrowException()
        {
            var user = new IdentityUser { Id = "user-id" };

            userManagerMock.Setup(x => x.FindByIdAsync("user-id")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await bagService.GetBagForDeletingAsync("user-id", 1));
            Assert.That(ex.Message, Is.EqualTo("You do not have permission to delete bags."));
        }

        [Test]
        public void GetBagForDeletingAsync_UserIdNull_ShouldThrowException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await bagService.GetBagForDeletingAsync(null, 1));
            Assert.That(ex.Message, Is.EqualTo("You do not have permission to delete bags."));
        }

        [Test]
        public async Task GetBagForDeletingAsync_AdminUserWithValidBag_ShouldReturnModel()
        {
            var user = new IdentityUser { Id = "admin-id" };
            var bag = new Bag
            {
                Id = 1,
                Brand = "Babolat",
                Model = "Pure Drive",
                Price = 220m,
                Quantity = 3,
                ImageUrl = "babolat.jpg"
            };
            dbContext.Bags.Add(bag);
            await dbContext.SaveChangesAsync();

            userManagerMock.Setup(x => x.FindByIdAsync("admin-id")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

            var result = await bagService.GetBagForDeletingAsync("admin-id", 1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Brand, Is.EqualTo("Babolat"));
            Assert.That(result.ImageUrl, Is.EqualTo("babolat.jpg"));
        }

        [Test]
        public void DeleteBagAsync_NonExistingBag_ShouldThrowException()
        {
            var user = new IdentityUser { Id = "admin-id" };
            var model = new BagDeleteViewModel
            {
                Id = 999,
                Brand = "NonExistent",
                Model = "None",
                ImageUrl = "none.jpg"
            };

            userManagerMock.Setup(x => x.FindByIdAsync("admin-id")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await bagService.DeleteBagAsync("admin-id", model));
            Assert.That(ex.Message, Is.EqualTo(BagNotFoundErrorMessage));
        }

        [Test]
        public async Task DeleteBagAsync_ExistingBag_ShouldDeleteAndReturnTrue()
        {
            var user = new IdentityUser { Id = "admin-id" };
            var bag = new Bag
            {
                Id = 1,
                Brand = "Wilson",
                Model = "Ultra",
                Price = 190m,
                Quantity = 6,
                ImageUrl = "ultra.jpg"
            };
            dbContext.Bags.Add(bag);
            await dbContext.SaveChangesAsync();

            var model = new BagDeleteViewModel
            {
                Id = 1,
                Brand = "Wilson",
                Model = "Ultra",
                ImageUrl = "ultra.jpg"
            };

            userManagerMock.Setup(x => x.FindByIdAsync("admin-id")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

            var result = await bagService.DeleteBagAsync("admin-id", model);

            Assert.That(result, Is.True);
            var deletedBag = await dbContext.Bags.FindAsync(1);
            Assert.That(deletedBag, Is.Null);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
