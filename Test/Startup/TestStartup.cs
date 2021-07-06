using Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Test.Startup
{
    public class TestStartup // : Startup
    {
        //TODO: diff between IHostingEnviornment and IWebHostEnvironment?
        public TestStartup(IHostEnvironment env) // : base(env)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            var options = new DbContextOptionsBuilder<RamosiContext>()
                .UseInMemoryDatabase("InMemory")
                .Options;
            services.AddSingleton(x => new RamosiContext(options));

            // TODO: Enable once the startup has been added
            /*
            services
                .AddMvc()
                .AddApplicationPart(typeof(Startup).Assembly)
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            */
        }

        //TODO: diff between IHostingEnviornment and IWebHostEnvironment?
        public void ConfigureTest(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}