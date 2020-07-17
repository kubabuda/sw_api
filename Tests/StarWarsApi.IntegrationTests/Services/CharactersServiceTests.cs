using FluentAssertions;
using NUnit.Framework;
using StarWars.BusinessLogic.Services;

namespace StarWarsApi.IntegrationTests.Services
{
    [TestFixture]
    public class CharactersServiceTests
    {
        CharactersService _serviceUnderTests;

        [Test]
        public void Pass()
        {
            // Arrange
            var result = true;

            // Assert
            result.Should().BeTrue();
        }
    }
}
