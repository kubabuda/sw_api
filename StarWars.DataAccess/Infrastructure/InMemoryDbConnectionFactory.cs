using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace StarWars.DataAccess.Infrastructure
{
    public static class DataAccessConstants
    {
        public const string InMemoryDbPath = "Filename=:memory:";
    }

    public static class InMemoryDbConnectionFactory
    {
        // for test usage
        public static DbConnection CreateInMemoryDbConnection()
        {
            var connection = new SqliteConnection(DataAccessConstants.InMemoryDbPath);
            connection.Open();

            return connection;
        }
    }
}
