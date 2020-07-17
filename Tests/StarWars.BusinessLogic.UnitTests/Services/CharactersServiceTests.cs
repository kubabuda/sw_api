using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using StarWars.BusinessLogic.Interfaces;
using StarWars.BusinessLogic.Services;
using System.Linq;

namespace StarWars.BusinessLogic.UnitTests.Services
{
    [TestFixture]
    public class CharactersServiceTests
    {
        const int pageSize = 2;
        private IStarWarsApiConfiguration _configuration;
        private CharactersService _serviceUnderTest;

        [SetUp]
        public void SetUp()
        {
            _configuration = Substitute.For<IStarWarsApiConfiguration>();
            _configuration.PageSize.Returns(pageSize);

            _serviceUnderTest = new CharactersService(_configuration);
        }

        [Test]
        public void GetCharacters_ShouldReturnNthPage_GivenPageNr()
        {
            // Act
            var result = _serviceUnderTest.GetCharacters(1);

            // Assert
            result.Count().Should().BeLessOrEqualTo(pageSize);
        }
    }
}
