using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StarWars.DataAccess;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace StarWarsApi.IntegrationTests.Infrastructure
{
    public static class InMemoryDbConnectionFactory
    {
        public static DbConnection CreateInMemoryDbConnection()
        {
            var connection = new SqliteConnection("Filename=:memory:"); // TODO: point to the same file that API appsettings.json[DatabasePath]
            connection.Open();

            return connection;
        }
    }
}
