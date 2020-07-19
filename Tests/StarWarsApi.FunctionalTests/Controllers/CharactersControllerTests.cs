using FluentAssertions;
using StarWars.Api;
using StarWars.BusinessLogic.Models;
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
        private readonly int _pageSize = 5; // as in configuration

        public CharactersControllerTests(StarWarsApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }


        [Fact]
        public async Task GetCharacters_ShouldReturnCharactersList_parameterless()
        {
            // Arrange - handled by application factory

            // Act
            var httpResponse = await _client.GetAsync($@"/characters");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var response = await UnpackResponse<IEnumerable<SwCharacter>>(httpResponse);
            response.First().Name.Should().Be("Luke Skywalker");
            response.Count().Should().Be(_pageSize);
        }

        [Fact]
        public async Task GetCharacters_ShouldReturnNthPage_GivenPageNr()
        {
            // Arrange
            var totalSize = 7; 
            var pageNr = 2;

            // Act
            var httpResponse = await _client.GetAsync($@"/characters?pageNr={pageNr}");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var response = await UnpackResponse<IEnumerable<SwCharacter>>(httpResponse);
            response.First().Name.Should().Be("C-3PO");
            response.Count().Should().Be(totalSize - _pageSize);
        }

        [Fact]
        public async Task GetCharater_ShouldGetCharacterByName_GivenValidName()
        {
            // Arrange
            var character = new SwCharacter
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
            var response = await UnpackResponse<SwCharacter>(httpResponse);
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
            var charactersBefore = await GetCharactersCount();
            var newCharacter = new SwCharacter
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
            var charactersAfter = await GetAllCharacters();
            charactersAfter.Count().Should().Be(charactersBefore + 1);
            charactersAfter.Where(r => r.Name == newCharacter.Name).Count().Should().Be(1);
        }

        [Fact]
        public async Task PutCharater_ShouldUpdateCharacter_GivenNameAndCharacterObject()
        {
            // Arrange
            var charactersBefore = await GetCharactersCount();
            var character = new SwCharacter
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
            var charactersAfter = await GetAllCharacters();
            charactersAfter.Count().Should().Be(charactersBefore);
            var verificationResponse = await UnpackResponse<SwCharacter>(await _client.GetAsync(requestUri));
            verificationResponse.Name.Should().Be(character.Name);
            verificationResponse.Episodes.Should().BeEquivalentTo(character.Episodes);
            verificationResponse.Friends.Should().BeEquivalentTo(character.Friends);
        }

        [Fact]
        public async Task PutCharater_ShouldReturn400_GivenMismatchedNameAndCharacterObject()
        {
            // Arrange
            var character = new SwCharacter
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

        [Fact]
        public async Task DeleteCharacter_ShouldAddNewCharacter_GivenCharacterObject()
        {
            int charactersBefore = await GetCharactersCount();
            var name = "Wilhuff Tarkin";

            // Act
            var httpResponse = await _client.DeleteAsync($@"/characters/{HttpUtility.UrlEncode(name)}");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var charactersAfter = await GetAllCharacters();
            charactersAfter.Where(r => r.Name == name).Count().Should().Be(0);
            charactersAfter.Count().Should().Be(charactersBefore - 1);
        }

        private async Task<int> GetCharactersCount()
        {
            // Arrange
            return (await GetAllCharacters()).Count();
        }

        private async Task<IEnumerable<SwCharacter>> GetAllCharacters()
        {
            var result = new List<SwCharacter>();
            IEnumerable<SwCharacter> page = null;
            var pageNr = 1;
            do
            {
                var httpResponse = await _client.GetAsync($@"/characters?pageNr={pageNr++}");
                httpResponse.EnsureSuccessStatusCode();
                page = await UnpackResponse<IEnumerable<SwCharacter>>(httpResponse);
                result.AddRange(page);
            }
            while (page.Count() == _pageSize);

            return result;
        }
    }
}
