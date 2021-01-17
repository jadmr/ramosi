using System;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Test.Base
{
    public class InMemoryDatabaseTest : BaseTest
    {
        protected DbContextOptions<RamosiContext> options;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<RamosiContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
        }
    }
}