using FluentAssertions;
using Newtonsoft.Json;
using StarWars.Api;
using StarWars.BusinessLogic.Models;
using StarWarsApi.FunctionalTests.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        // TODO create base StarWarsApiIntegrationTest
        private static async Task<T> UnpackResponse<T>(HttpResponseMessage httpResponse)
        {
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(stringResponse);
        }
    }
}
