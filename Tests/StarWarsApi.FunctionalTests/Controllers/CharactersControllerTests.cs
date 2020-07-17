using FluentAssertions;
using Newtonsoft.Json;
using StarWars.Api;
using StarWars.BusinessLogic.Models;
using StarWars.DataAccess.Repository;
using StarWarsApi.FunctionalTests.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StarWarsApi.FunctionalTests.Controllers
{
    public class CharactersControllerTests: IClassFixture<StarWarsApplicationFactory<Startup>>
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
        public async Task CreateCharater_shouldAddNewCharacter()
        {
            // Arrange
            var repo = new CharacterRepository();
            var charactersBefore = repo.GetQueryable().Count();
            var newCharacter = new Character { Name = "Rey", Episodes = new[] { "FORCE AWAKE", "LAST JEDI", "SKYWALKER" } };
            
            // Act
            var httpResponse = await _client.PostAsync($@"/characters", GetJsonContent(newCharacter));

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            repo.GetQueryable().Count().Should().Be(charactersBefore + 1);
            repo.GetQueryable().Where(r => r.Name == newCharacter.Name).Count().Should().Be(1);
        }

        private static HttpContent GetJsonContent(Character newCharacter)
        {
            return new StringContent(JsonConvert.SerializeObject(newCharacter), Encoding.UTF8, "application/json");
        }

        // TODO create BaseFunctionalTest
        private static async Task<T> UnpackResponse<T>(HttpResponseMessage httpResponse)
        {
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(stringResponse);
        }
    }
}
