using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StarWars.DataAccess;
using StarWarsApi.IntegrationTests.Infrastructure;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsApi.IntegrationTests.DataAccess
{
    public class StarWarsDbContextExtensionsTest : IDisposable
    {
        private DbConnection _connection;

        [SetUp]
        public void Setup()
        {
            _connection = InMemoryDbConnectionFactory.CreateInMemoryDbConnection();
        }

        [Test]
        public async Task SeedAsync_ShouldPopulateDb_WithoutParameters()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StarWarsDbContext>()
                .UseSqlite(_connection)
                .Options;
            var dbContext = new StarWarsDbContext(options);
            await dbContext.Database.MigrateAsync();

            // Act
            await dbContext.SeedAsync();

            // Assert
            (await dbContext.Characters.CountAsync()).Should().Be(7);
            var luke_skywalker = dbContext.Characters.Where(u => u.Id == 1).Single();
            luke_skywalker.EpisodeCharacters.Select(e => e.Episode).Select(e => e.Name)
                .Should().BeEquivalentTo(new[] { "NEWHOPE", "EMPIRE", "JEDI" });
            luke_skywalker.Friends.Select(e => e.Friend).Select(e => e.Name)
                .Should().BeEquivalentTo(new[] { "Han Solo", "Leia Organa", "C-3PO", "R2-D2" });

        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
