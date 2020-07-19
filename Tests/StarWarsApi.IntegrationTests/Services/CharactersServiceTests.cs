using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StarWars.BusinessLogic.Services;
using StarWars.DataAccess;
using StarWars.DataAccess.Models;
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

        private readonly CharactersService _serviceUnderTests;

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            return connection;
        }

        [Test]
        public async Task Pass()
        {
            // Arrange
            _connection = CreateInMemoryDatabase();

            var options = new DbContextOptionsBuilder<StarWarsDbContext>()
                .UseSqlite(_connection)
                .Options;

            // act
            var dbContext = new StarWarsDbContext(options);
            dbContext.Database.EnsureCreated();

            // Assert
            //var luk = dbContext.Characters.Where(u => u.Id == 1).Single();
            //var yy = luk.Episodes.Select(e => e.Episode).ToArray();
            //    //.Select(e => e.Name)
            //    //.Should().BeEquivalentTo(new[] { "NEWHOPE", "EMPIRE", "JEDI" });
            (await dbContext.Characters.CountAsync()).Should().Be(7);
        }
        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
