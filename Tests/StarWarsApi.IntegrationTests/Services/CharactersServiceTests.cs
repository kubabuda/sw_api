using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using StarWars.Api.Configuration;
using StarWars.BusinessLogic.Services.Interfaces;
using StarWars.DataAccess;
using StarWars.DataAccess.Infrastructure;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsApi.IntegrationTests.Services
{
    [TestFixture]
    public class CharactersServiceTests : IDisposable
    {
        private DbConnection _connection;

        private ICharactersService _serviceUnderTests;

        [SetUp]
        public async Task Setup()
        {
            _connection = InMemoryDbConnectionFactory.CreateInMemoryDbConnection();
            IConfiguration configuration = Substitute.For<IConfiguration>();
            configuration["PageSize"].Returns("5");
            configuration["DatabasePath"].Returns(DataAccessConstants.InMemoryDbPath);

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);
            services.Bootstrap(configuration);
            // overwrites
            services.AddDbContext<StarWarsDbContext>(options =>
                options.UseSqlite(_connection));
            // resolve tested component
            var provider = services.BuildServiceProvider();
            _serviceUnderTests = provider.GetRequiredService<ICharactersService>();
        }

        [TestCase(1, new[] { "Luke Skywalker", "Darth Vader", "Han Solo", "Leia Organa", "Wilhuff Tarkin" })]
        [TestCase(2, new[] { "C-3PO", "R2-D2" })]
        public async Task GetCharacters_ShouldReturnNthPage_GivenPageNr(int pageNr, string[] names)
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

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
