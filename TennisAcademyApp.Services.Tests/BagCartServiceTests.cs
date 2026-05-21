//using NUnit.Framework;
//using Moq;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using TennisAcademyApp.Data;
//using TennisAcademyApp.Data.Models;
//using TennisAcademyApp.Services.Core;
//using static TennisAcademyApp.GCommon.Validations.ErrorMessages.BagCart;

//namespace TennisAcademyApp.Tests.Services
//{
//    [TestFixture]
//    public class BagCartServiceTests
//    {
//        private TennisAcademyDbContext dbContext;
//        private Mock<UserManager<ApplicationUser>> userManagerMock;
//        private BagCartService bagCartService;

//        [SetUp]
//        public void SetUp()
//        {
//            var options = new DbContextOptionsBuilder<TennisAcademyDbContext>()
//                .UseInMemoryDatabase(Guid.NewGuid().ToString())
//                .Options;

//            dbContext = new TennisAcademyDbContext(options);

//            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
//            userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

//            bagCartService = new BagCartService(dbContext, userManagerMock.Object);
//        }

//        [Test]
//        public async Task GetAllBagsInCartAsync_ShouldReturnCorrectBags()
//        {
//            var userId = "user1";
//            var user = new ApplicationUser { Id = userId };

//            var bag = new Bag
//            {
//                Id = 1,
//                Brand = "Nike",
//                Model = "Air",
//                Price = 100m,
//                Quantity = 10,
//                ImageUrl = "nike.jpg"
//            };

//            var cartItem = new BagCart
//            {
//                BagId = bag.Id,
//                UserId = userId,
//                Quantity = 2,
//                Bag = bag
//            };

//            await dbContext.Bags.AddAsync(bag);
//            await dbContext.BagCart.AddAsync(cartItem);
//            await dbContext.SaveChangesAsync();

//            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

//            var result = (await bagCartService.GetAllBagsInCartAsync(userId)).ToList();

//            Assert.That(result.Count, Is.EqualTo(1));
//            Assert.That(result[0].Id, Is.EqualTo(bag.Id));
//            Assert.That(result[0].Brand, Is.EqualTo("Nike"));
//            Assert.That(result[0].Model, Is.EqualTo("Air"));
//            Assert.That(result[0].Price, Is.EqualTo(100m));
//            Assert.That(result[0].Quantity, Is.EqualTo(2));
//            Assert.That(result[0].TotalPrice, Is.EqualTo(200m));
//            Assert.That(result[0].ImageUrl, Is.EqualTo("nike.jpg"));
//        }

//        [Test]
//        public async Task AddBagToCartAsync_ShouldAddNewItem_WhenNotExists()
//        {
//            var userId = "user2";
//            var user = new ApplicationUser { Id = userId };

//            var bag = new Bag
//            {
//                Id = 1,
//                Brand = "Adidas",
//                Model = "UltraBoost",
//                Price = 150m,
//                Quantity = 5,
//                ImageUrl = "adidas.jpg"
//            };
//            await dbContext.Bags.AddAsync(bag);
//            await dbContext.SaveChangesAsync();

//            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

//            var result = await bagCartService.AddBagToCartAsync(userId, bag.Id, 3);

//            Assert.That(result, Is.True);

//            var cartItem = await dbContext.BagCart.FirstOrDefaultAsync(bc => bc.UserId == userId && bc.BagId == bag.Id);
//            Assert.That(cartItem, Is.Not.Null);
//            Assert.That(cartItem.Quantity, Is.EqualTo(3));

//            var updatedBag = await dbContext.Bags.FindAsync(bag.Id);
//            Assert.That(updatedBag.Quantity, Is.EqualTo(2)); // 5 - 3 = 2
//        }

//        [Test]
//        public async Task AddBagToCartAsync_ShouldUpdateQuantity_WhenAlreadyInCart()
//        {
//            var userId = "user3";
//            var user = new ApplicationUser { Id = userId };

//            var bag = new Bag
//            {
//                Id = 1,
//                Brand = "Puma",
//                Model = "Speed",
//                Price = 120m,
//                Quantity = 10,
//                ImageUrl = "puma.jpg"
//            };

//            var existingCartItem = new BagCart
//            {
//                BagId = bag.Id,
//                UserId = userId,
//                Quantity = 2
//            };

//            await dbContext.Bags.AddAsync(bag);
//            await dbContext.BagCart.AddAsync(existingCartItem);
//            await dbContext.SaveChangesAsync();

//            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

//            var result = await bagCartService.AddBagToCartAsync(userId, bag.Id, 3);

//            Assert.That(result, Is.True);

//            var cartItem = await dbContext.BagCart.FirstOrDefaultAsync(bc => bc.UserId == userId && bc.BagId == bag.Id);
//            Assert.That(cartItem.Quantity, Is.EqualTo(5)); // 2 + 3

//            var updatedBag = await dbContext.Bags.FindAsync(bag.Id);
//            Assert.That(updatedBag.Quantity, Is.EqualTo(7)); // 10 - 3 = 7
//        }

//        [Test]
//        public void AddBagToCartAsync_ShouldThrow_WhenInvalidQuantity()
//        {
//            var userId = "user4";
//            var user = new ApplicationUser { Id = userId };

//            var bag = new Bag
//            {
//                Id = 1,
//                Brand = "Reebok",
//                Model = "Classic",
//                Price = 90m,
//                Quantity = 5,
//                ImageUrl = "reebok.jpg"
//            };

//            dbContext.Bags.Add(bag);
//            dbContext.SaveChanges();

//            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

//            Assert.ThrowsAsync<InvalidOperationException>(async () =>
//                await bagCartService.AddBagToCartAsync(userId, bag.Id, 0), InvalidQuantityErrorMessage);

//            Assert.ThrowsAsync<InvalidOperationException>(async () =>
//                await bagCartService.AddBagToCartAsync(userId, bag.Id, 10), InvalidQuantityErrorMessage);

//            Assert.ThrowsAsync<InvalidOperationException>(async () =>
//                await bagCartService.AddBagToCartAsync(userId, 999, 1), InvalidQuantityErrorMessage);
//        }

//        [Test]
//        public async Task RemoveBagFromCartAsync_ShouldRemoveItem_WhenExists()
//        {
//            var userId = "user5";
//            var user = new ApplicationUser { Id = userId };

//            var bag = new Bag
//            {
//                Id = 1,
//                Brand = "Asics",
//                Model = "Gel",
//                Price = 130m,
//                Quantity = 10,
//                ImageUrl = "asics.jpg"
//            };

//            var cartItem = new BagCart
//            {
//                BagId = bag.Id,
//                UserId = userId,
//                Quantity = 2
//            };

//            await dbContext.Bags.AddAsync(bag);
//            await dbContext.BagCart.AddAsync(cartItem);
//            await dbContext.SaveChangesAsync();

//            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

//            var result = await bagCartService.RemoveBagFromCartAsync(userId, bag.Id);

//            Assert.That(result, Is.True);

//            var itemInDb = await dbContext.BagCart.FirstOrDefaultAsync(bc => bc.UserId == userId && bc.BagId == bag.Id);
//            Assert.That(itemInDb, Is.Null);
//        }

//        [Test]
//        public void RemoveBagFromCartAsync_ShouldThrow_WhenItemNotFound()
//        {
//            var userId = "user6";
//            var user = new ApplicationUser { Id = userId };

//            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

//            Assert.ThrowsAsync<InvalidOperationException>(async () =>
//                await bagCartService.RemoveBagFromCartAsync(userId, 999), BagNotFoundInCartErrorMessage);
//        }

//        [Test]
//        public async Task CheckOutAllBagsAsync_ShouldRemoveAllItems()
//        {
//            var userId = "user7";
//            var user = new ApplicationUser { Id = userId };

//            var cartItems = new List<BagCart>
//            {
//                new BagCart { BagId = 1, UserId = userId, Quantity = 1 },
//                new BagCart { BagId = 2, UserId = userId, Quantity = 2 }
//            };

//            await dbContext.BagCart.AddRangeAsync(cartItems);
//            await dbContext.SaveChangesAsync();

//            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

//            var result = await bagCartService.CheckOutAllBagsAsync(userId);

//            Assert.That(result, Is.True);

//            var remainingItems = await dbContext.BagCart.Where(bc => bc.UserId == userId).ToListAsync();
//            Assert.That(remainingItems.Count, Is.EqualTo(0));
//        }

//        [Test]
//        public async Task CheckOutAllBagsAsync_ShouldReturnFalse_WhenNoItems()
//        {
//            var userId = "user8";
//            var user = new ApplicationUser { Id = userId };

//            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

//            var result = await bagCartService.CheckOutAllBagsAsync(userId);

//            Assert.That(result, Is.False);
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            dbContext.Dispose();
//        }
//    }
//}
