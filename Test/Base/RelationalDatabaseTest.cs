using Data.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Test.Base
{
    public class RelationalDatabaseTest : BaseTest
    {
        protected DbContextOptions<RamosiContext> options;
        protected SqliteConnection connection;

        [SetUp]
        public void Setup()
        {
            connection = new SqliteConnection("DataSource=one;Mode=memory");
            connection.Open();

            options = new DbContextOptionsBuilder<RamosiContext>()
                .UseSqlite(connection)
                .Options;

            // Create the schema
            using (var context = new RamosiContext(options))
                context.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            if (connection.State != System.Data.ConnectionState.Closed)
                connection.Close();
        }
    }
}