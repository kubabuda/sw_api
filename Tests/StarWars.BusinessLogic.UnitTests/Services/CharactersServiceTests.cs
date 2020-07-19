using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using StarWars.BusinessLogic.Interfaces;
using StarWars.BusinessLogic.Interfaces.Repositories;
using StarWars.BusinessLogic.Models;
using StarWars.BusinessLogic.Services;
using StarWars.BusinessLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StarWars.BusinessLogic.UnitTests.Services
{
    [TestFixture]
    public class CharactersServiceTests
    {
        private const int pageSize = 2;
        private List<SwCharacter> _allCharacters;
        private IStarWarsApiConfiguration _configuration;
        private ICharacterRepository _repository;
        private IValidateActionsService _validator;

        private CharactersService _serviceUnderTest;

        [SetUp]
        public void SetUp()
        {
            _configuration = Substitute.For<IStarWarsApiConfiguration>();
            _configuration.PageSize.Returns(pageSize);

            _allCharacters = new List<SwCharacter>
            {
                new SwCharacter { Name = "0", Planet = "0" },
                new SwCharacter { Name = "1", Planet = "1" },
                new SwCharacter { Name = "2", Planet = "2" },
                new SwCharacter { Name = "3", Planet = "3" },
                new SwCharacter { Name = "4", Planet = "4" },
                new SwCharacter { Name = "5", Planet = "5" },
            };
            _repository = Substitute.For<ICharacterRepository>();
            _repository.GetQueryable().Returns(_allCharacters.AsQueryable());
            _validator = Substitute.For<IValidateActionsService>();
            
            _serviceUnderTest = new CharactersService(_configuration, _repository, _validator);
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
            var character = new SwCharacter { Name = "Foo", Planet = "Bar" };

            // Act
            _serviceUnderTest.CreateCharacter(character);

            // Assert
            _repository.Received(1).Create(character);
        }

        [Test]
        public void UpdateCharacter_ShouldPassUpdateToRepository_GivenCharacterToCreateAndItsKey()
        {
            // Arrange
            var character = new SwCharacter { Name = "0", Planet = "Mars" };
            var name = "0";
            _validator.IsValidUpdate(name, character).Returns(true);

            // Act
            _serviceUnderTest.UpdateCharacter(name, character);

            // Assert
            _repository.Received(1).Update(name, character);
        }

        [Test]
        public void UpdateCharacter_ShouldThrowInvalidOperationException_GivenMismatchedCharacterToCreateAndItsKey()
        {
            // Arrange
            var character = new SwCharacter { Name = "0", Planet = "Mars" };
            var name = "1";
            _validator.IsValidUpdate(name, character).Returns(false);

            // Act
            Assert.Throws<InvalidOperationException>(() => _serviceUnderTest.UpdateCharacter(name, character));

            // Assert
            _repository.Received(0).Update(name, character);
        }

        [Test]
        public void DeleteCharacter_ShouldRelayToRepository_GivenValidKey()
        {
            // Arrange
            var name = "1";

            // Act
            _serviceUnderTest.DeleteCharacter(name);

            // Assert
            _repository.Received(1).Delete(name);
        }
    }
}
