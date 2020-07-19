using NUnit.Framework;
using StarWars.BusinessLogic.Models;
using StarWars.BusinessLogic.Services;

namespace StarWars.BusinessLogic.UnitTests.Services
{
    [TestFixture]
    public class ValidateActionsServiceTests
    {
        private ValidateActionsService _serviceUnderTests;

        [SetUp]
        public void Setup()
        {
            _serviceUnderTests = new ValidateActionsService();
        }

        [Test]
        public void ValidateUpdate_ReturnsTrue_WhenNameMatchesUpdatedObject()
        {
            var character = new SwCharacter { Name = "0", Planet = "Mars" };
        }

        [Test]
        public void ValidateUpdate_ReturnsFalse_WhenNameMismatchedWithUpdatedObject()
        {

        }
    }
}
