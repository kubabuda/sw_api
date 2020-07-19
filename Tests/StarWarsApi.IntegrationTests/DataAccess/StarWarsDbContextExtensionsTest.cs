using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StarWars.Api.Configuration;
using StarWars.BusinessLogic.Models;
using StarWars.DataAccess;
using StarWars.DataAccess.Infrastructure;
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
            dbContext.Seed();

            // Assert
            (await dbContext.Characters.CountAsync()).Should().Be(7);
            var luke_skywalker = dbContext.Characters.Where(u => u.Id == 1).Single();
            luke_skywalker.EpisodeCharacters.Select(e => e.Episode).Select(e => e.Name)
                .Should().BeEquivalentTo(new[] { "NEWHOPE", "EMPIRE", "JEDI" });
            luke_skywalker.Friends.Select(e => e.Friend).Select(e => e.Name)
                .Should().BeEquivalentTo(new[] { "Han Solo", "Leia Organa", "C-3PO", "R2-D2" });

        }

        [Test]
        public async Task ProjectTo_shouldGetDomainModelsQueryable_GivenEntityTable()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StarWarsDbContext>()
                .UseSqlite(_connection)
                .Options;
            var dbContext = new StarWarsDbContext(options);
            await dbContext.Database.MigrateAsync();
            dbContext.Seed();
            await dbContext.SaveChangesAsync();
            dbContext = new StarWarsDbContext(options); // get fresh one
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapping>();
            });
            IMapper mapper = new Mapper(configuration);

            // Act
            var result = mapper.ProjectTo<SwCharacter>(dbContext.Characters).ToList();

            // Assert
            result.Count().Should().Be(7);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
