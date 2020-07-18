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

        public CharactersControllerTests(StarWarsApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCharacters_shouldReturnCharactersList()
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
        public async Task GetCharater_shouldGetCharacterByName()
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
        public async Task GetCharater_shouldUpdateCharacter()
        {
            // Arrange
            
            // Act
            string requestUri = $@"/characters/{HttpUtility.UrlEncode("Jason Bourne")}";
            var httpResponse = await _client.GetAsync(requestUri);

            // Assert
            httpResponse.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task PostCharater_shouldAddNewCharacter()
        {
            // Arrange
            var repo = new CharacterRepository();
            var charactersBefore = repo.GetQueryable().Count();
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
            repo.GetQueryable().Count().Should().Be(charactersBefore + 1);
            repo.GetQueryable().Where(r => r.Name == newCharacter.Name).Count().Should().Be(1);
        }

    }
}
