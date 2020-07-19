using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace StarWarsApi.IntegrationTests.Infrastructure
{
    public static class InMemoryDbConnectionFactory
    {
        public static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            return connection;
        }
    }
}
