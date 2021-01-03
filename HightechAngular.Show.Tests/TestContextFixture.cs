using HightechAngular.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HightechAngular.Show.Tests
{
    public class TestContextFixture
    {
        public IHost Host { get; }
        
        public TestContextFixture()
        {
            // var cfg = new ConfigurationBuilder().Build();
            // var startup = new Startup(cfg);
            // ServiceCollection = new ServiceCollection();
            // startup.ConfigureServices(ServiceCollection);

            var hb = Program.CreateHostBuilder(null);
            Host = hb.Build();
            var dbc = Host.Services.GetService<DbContext>();
            dbc.Database.Migrate();

            Host.InitAsync().Wait();
        }

        public ControllerActionBuilder<T> With<T>() 
            where T : ControllerBase, new()
        {
            return new ControllerActionBuilder<T>(this);
        }
    }
}