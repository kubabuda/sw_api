using FluentAssertions;
using Microsoft.Data.Sqlite;
using NUnit.Framework;
using StarWars.BusinessLogic.Services;
using System;
using System.Data.Common;

namespace StarWarsApi.IntegrationTests.Services
{
    [TestFixture]
    public class CharactersServiceTests : IDisposable
    {
        private readonly DbConnection _connection;

        private readonly CharactersService _serviceUnderTests;

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        [Test]
        public void Pass()
        {
            // Arrange
            var result = true;

            // Assert
            result.Should().BeTrue();
        }
        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
