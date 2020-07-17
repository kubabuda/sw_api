using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using StarWars.BusinessLogic.Interfaces;
using StarWars.BusinessLogic.Interfaces.Repositories;
using StarWars.BusinessLogic.Models;
using StarWars.BusinessLogic.Services;
using System.Collections.Generic;
using System.Linq;

namespace StarWars.BusinessLogic.UnitTests.Services
{
    [TestFixture]
    public class CharactersServiceTests
    {
        private const int pageSize = 2;
        private IEnumerable<Character> _allCharacters;
        private IStarWarsApiConfiguration _configuration;
        private ICharacterRepository _repository;
        private CharactersService _serviceUnderTest;

        [SetUp]
        public void SetUp()
        {
            _configuration = Substitute.For<IStarWarsApiConfiguration>();
            _configuration.PageSize.Returns(pageSize);

            _allCharacters = new List<Character>
            {
                new Character { Name = "0" }, new Character { Name = "1" }, new Character { Name = "2" },
                new Character { Name = "3" }, new Character { Name = "4" }, new Character { Name = "5" },
            };
            _repository = Substitute.For<ICharacterRepository>();
            _repository.GetQueryable().Returns(_allCharacters.AsQueryable());

            _serviceUnderTest = new CharactersService(_configuration, _repository);
        }

        [TestCase(1, new[] { "0", "1" })]
        [TestCase(2, new[] { "2", "3" })]
        [TestCase(3, new[] { "4", "5" })]
        public void GetCharacters_ShouldReturnNthPage_GivenPageNr(int pageNr, string[] names)
        {
            // Act
            var result = _serviceUnderTest.GetCharacters(pageNr);

            // Assert
            int i = 0;
            foreach(var character in result)
            {
                character.Name.Should().Be(names[i]);
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetCharacters_ShouldRetunrNotTooBigPage_GivenEveryPageNr(int pageNr)
        {
            // Act
            var result = _serviceUnderTest.GetCharacters(pageNr);

            // Assert
            result.Count().Should().BeLessOrEqualTo(pageSize);
        }
    }
}
