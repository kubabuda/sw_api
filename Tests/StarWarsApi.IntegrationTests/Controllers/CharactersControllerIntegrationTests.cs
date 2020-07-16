using FluentAssertions;
using Newtonsoft.Json;
using StarWars.Api;
using StarWars.BusinessLogic.Models;
using StarWarsApi.IntegrationTests.Infrastructure;
using System.Collections.Generic;
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
        public void Test_shouldPass()
        {
            (2 + 2).Should().Be(4);
        }


        [Fact]
        public async Task Test_shouldFail()
        {
            // Act
            var httpResponse = await _client.GetAsync($@"/repositories");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var response = await UnpackResponse<IEnumerable<Character>>(httpResponse);
        }

        // TODO create base StarWarsApiIntegrationTest
        private static async Task<T> UnpackResponse<T>(HttpResponseMessage httpResponse)
        {
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(stringResponse);
        }
    }
}
