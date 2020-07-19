using Microsoft.Data.Sqlite;
using NUnit.Framework;
using StarWars.BusinessLogic.Services;
using StarWarsApi.IntegrationTests.Infrastructure;
using System;
using System.Data.Common;

namespace StarWarsApi.IntegrationTests.Services
{
    [TestFixture]
    public class CharactersServiceTests : IDisposable
    {
        private DbConnection _connection;

        private readonly CharactersService _serviceUnderTests;

        [SetUp]
        public void Setup()
        {
            _connection = InMemoryDbConnectionFactory.CreateInMemoryDatabase();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
