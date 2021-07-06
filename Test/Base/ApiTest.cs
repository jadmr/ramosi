using System.Net.Http;
using Data.Context;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Test.Base
{
    public class ApiTest : BaseTest
    {
        protected TestServer server;
        protected HttpClient client;

        [SetUp]
        public void SetUp()
        {
            // TODO: Enable once the Startup has been added
            /*
            var builder = new WebHostBuilder()
                .UseEnvironment("Test")
                .UseStartup<TestStartup>();

            server = new TestServer(builder);
            client = server.CreateClient();
            */

            var context = server.Host.Services.GetService(typeof(RamosiContext)) as RamosiContext;
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}