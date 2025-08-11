using Moq;
using NUnit.Framework;
using TennisAcademyApp.Services.Core.Contracts;
using Assert = NUnit.Framework.Assert;

namespace TennisAcademyApp.Services.Tests
{
    [TestFixture]
    public class CoachServiceTests
    {
        private Mock<ICoachService> coachServiceMock;

        [SetUp]
        public void SetUp()
        {
            coachServiceMock = new Mock<ICoachService>();
        }
        [Test]
        public void PassAlways()
        {
            Assert.Pass();
        }
    }
}
