using FluentAssertions;
using Newtonsoft.Json;
using StarWars.Api;
using StarWars.BusinessLogic.Models;
using StarWarsApi.IntegrationTests.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace StarWarsApi.IntegrationTests.Controllers
{
    public class CharactersControllerIntegrationTests: IClassFixture<StarWarsApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CharactersControllerIntegrationTests(StarWarsApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCharacters_shouldReturnCharactersList()
        {
            // Arrange - handled by application factory

            // Act
            var httpResponse = await _client.GetAsync($@"/characters");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var response = await UnpackResponse<IEnumerable<Character>>(httpResponse);
            response.First().Name.Should().Be("Luke Skywalker");
            response.Count().Should().Be(7);
        }

        // TODO create base StarWarsApiIntegrationTest
        private static async Task<T> UnpackResponse<T>(HttpResponseMessage httpResponse)
        {
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(stringResponse);
        }
    }
}
