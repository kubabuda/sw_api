using FluentAssertions;
using StarWars.Api;
using StarWars.BusinessLogic.Models;
using StarWars.DataAccess.Repository;
using StarWarsApi.FunctionalTests.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Xunit;

namespace StarWarsApi.FunctionalTests.Controllers
{
    public class CharactersControllerTests: ABaseFunctionalTest, IClassFixture<StarWarsApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CharacterRepository _repo;

        public CharactersControllerTests(StarWarsApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _repo = new CharacterRepository();
        }

        [Fact]
        public async Task GetCharacters_ShouldReturnCharactersList_parameterless()
        {
            // Arrange - handled by application factory
            var pageSize = 5; // as in configuration

            // Act
            var httpResponse = await _client.GetAsync($@"/characters");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var response = await UnpackResponse<IEnumerable<Character>>(httpResponse);
            response.First().Name.Should().Be("Luke Skywalker");
            response.Count().Should().Be(pageSize);
        }

        [Fact]
        public async Task GetCharater_ShouldGetCharacterByName_GivenValidName()
        {
            // Arrange
            var character = new Character
            {
                Name = "Han Solo",
                Episodes = new[] { "NEWHOPE", "EMPIRE", "JEDI" },
                Friends = new[] { "Luke Skywalker", "Leia Organa", "R2-D2" }
            };

            // Act
            string requestUri = $@"/characters/{HttpUtility.UrlEncode(character.Name)}";
            var httpResponse = await _client.GetAsync(requestUri);

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var response = await UnpackResponse<Character>(httpResponse);
            response.Name.Should().Be(character.Name);
            response.Episodes.Should().BeEquivalentTo(character.Episodes);
            response.Friends.Should().BeEquivalentTo(character.Friends);
        }

        [Fact]
        public async Task GetCharater_ShouldReturn404_GivenInvalidName()
        {
            // Arrange
            
            // Act
            string requestUri = $@"/characters/{HttpUtility.UrlEncode("Jason Bourne")}";
            var httpResponse = await _client.GetAsync(requestUri);

            // Assert
            httpResponse.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task PostCharater_ShouldAddNewCharacter_GivenCharacterObject()
        {
            // Arrange
            var charactersBefore = _repo.GetQueryable().Count();
            var newCharacter = new Character
            {
                Name = "Rey",
                Episodes = new[] { "FORCE_AWAKE", "LAST_JEDI", "SKYWALKER" },
                Friends = new string[] { }
            };
            var requestContent = GetJsonContent(newCharacter);

            // Act
            var httpResponse = await _client.PostAsync($@"/characters", requestContent);

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            _repo.GetQueryable().Count().Should().Be(charactersBefore + 1);
            _repo.GetQueryable().Where(r => r.Name == newCharacter.Name).Count().Should().Be(1);
        }

        [Fact]
        public async Task PutCharater_ShouldUpdateCharacter_GivenNameAndCharacterObject()
        {
            // Arrange
            var charactersBefore = _repo.GetQueryable().Count();
            var character = new Character
            {
                Name = "Luke Skywalker",
                Episodes = new[] { "NEWHOPE", "EMPIRE", "JEDI", "FORCE_AWAKE", "LAST_JEDI", "SKYWALKER" },
                Friends = new[] { "Luke Skywalker", "Leia Organa", "R2-D2", "Rey" }
            };
            var requestContent = GetJsonContent(character);
            var requestUri = $@"/characters/{HttpUtility.UrlEncode(character.Name)}";

            // Act
            var httpResponse = await _client.PutAsync(requestUri,
                requestContent);
            httpResponse.EnsureSuccessStatusCode();

            // Assert
            httpResponse.StatusCode.Should().Be(204);
            _repo.GetQueryable().Count().Should().Be(charactersBefore);
            var verificationResponse = await UnpackResponse<Character>(await _client.GetAsync(requestUri)); // kind of hacky solution
            verificationResponse.Name.Should().Be(character.Name);
            verificationResponse.Episodes.Should().BeEquivalentTo(character.Episodes);
            verificationResponse.Friends.Should().BeEquivalentTo(character.Friends);
        }

        [Fact]
        public async Task PutCharater_ShouldReturn400_GivenMismatchedNameAndCharacterObject()
        {
            // Arrange
            var character = new Character
            {
                Name = "Luke Skywalker",
                Episodes = new[] { "NEWHOPE", "EMPIRE", "JEDI", "FORCE_AWAKE", "LAST_JEDI", "SKYWALKER" },
                Friends = new[] { "Luke Skywalker", "Leia Organa", "R2-D2", "Rey" }
            };
            var requestContent = GetJsonContent(character);
            var requestUri = $@"/characters/{HttpUtility.UrlEncode("Han Solo")}";

            // Act
            var httpResponse = await _client.PutAsync(requestUri,
                requestContent);

            // Assert
            httpResponse.StatusCode.Should().Be(400);
        }
    }
}
