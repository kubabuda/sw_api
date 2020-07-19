using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using StarWars.Api.Configuration;
using StarWars.BusinessLogic.Services.Interfaces;
using StarWars.DataAccess.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsApi.IntegrationTests.Services
{
    [TestFixture]
    public class CharactersServiceTests
    {
        private ICharactersService _serviceUnderTests;

        [SetUp]
        public async Task Setup()
        {
            IConfiguration configuration = Substitute.For<IConfiguration>();
            configuration["PageSize"].Returns("5");
            configuration["DatabasePath"].Returns(DataAccessConstants.InMemoryDbPath);

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);
            services.Bootstrap(configuration);
            // resolve tested component
            var provider = services.BuildServiceProvider();
            _serviceUnderTests = provider.GetRequiredService<ICharactersService>();
        }

        [TestCase(1, new[] { "Luke Skywalker", "Darth Vader", "Han Solo", "Leia Organa", "Wilhuff Tarkin" })]
        [TestCase(2, new[] { "C-3PO", "R2-D2" })]
        public void GetCharacters_ShouldReturnNthPage_GivenPageNr(int pageNr, string[] names)
        {
            // Act
            var result = _serviceUnderTests.GetCharacters(pageNr).ToList();

            // Assert
            result.Count.Should().Be(names.Length);
            int i = 0;
            foreach (var character in result)
            {
                character.Name.Should().Be(names[i++]);
            }
        }
    }
}
