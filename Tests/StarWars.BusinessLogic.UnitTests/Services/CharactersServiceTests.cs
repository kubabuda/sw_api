using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using StarWars.BusinessLogic.Interfaces;
using StarWars.BusinessLogic.Interfaces.Repositories;
using StarWars.BusinessLogic.Models;
using StarWars.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StarWars.BusinessLogic.UnitTests.Services
{
    [TestFixture]
    public class CharactersServiceTests
    {
        private const int pageSize = 2;
        private List<Character> _allCharacters;
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
                new Character { Name = "0", Planet = "0" },
                new Character { Name = "1", Planet = "1" },
                new Character { Name = "2", Planet = "2" },
                new Character { Name = "3", Planet = "3" },
                new Character { Name = "4", Planet = "4" },
                new Character { Name = "5", Planet = "5" },
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
            foreach (var character in result)
            {
                character.Name.Should().Be(names[i++]);
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetCharacters_ShouldReturnNotTooBigPage_GivenEveryPageNr(int pageNr)
        {
            // Act
            var result = _serviceUnderTest.GetCharacters(pageNr);

            // Assert
            result.Count().Should().BeLessOrEqualTo(pageSize);
        }

        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        public void GetCharacter_ShouldReturnCharacter_GivenValidName(string name)
        {
            // Act
            var result = _serviceUnderTest.GetCharacter(name);

            // Assert
            result.Name.Should().Be(name);
            result.Planet.Should().Be(name);
        }

        [TestCase("66")]
        public void GetCharacter_ShouldThrow_GivenUnknonwName(string name)
        {
            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => _serviceUnderTest.GetCharacter(name));
        }

        [Test]
        public void CreateCharacter_ShouldPassCreateToRepository_GivenCharacterToCreate()
        {
            // Arrange
            var character = new Character { Name = "Foo", Planet = "Bar" };

            // Act
            _serviceUnderTest.CreateCharacter(character);

            // Assert
            _repository.Received(1).Create(character);
        }

        [Test]
        public void UpdateCharacter_ShouldPassUpdateToRepository_GivenCharacterToCreateAndItsKey()
        {
            // Arrange
            var character = new Character { Name = "0", Planet = "Mars" };
            var name = "0";

            // Act
            _serviceUnderTest.UpdateCharacter(name, character);

            // Assert
            _repository.Received(1).Update(name, character);
        }
    }
}
