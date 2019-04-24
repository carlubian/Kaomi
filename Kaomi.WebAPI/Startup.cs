using Kaomi.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace Kaomi.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;

            // Make sure KaomiLoader class is loaded into memory.
            KaomiLoader.IsActive();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddMvcOptions(opt =>
                {
                    opt.EnableEndpointRouting = false;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            app.UseMvc();
            lifetime.ApplicationStopping.Register(this.OnShutdown);
        }

        private void OnShutdown()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            foreach (var assembly in KaomiLoader.ListProcesses()
                .Select(pr => pr.Split(' ')[0].Replace("[", "").Replace("]", ""))
                .Distinct()
                .Where(asm => asm != "System"))
            {
                Console.WriteLine($"[SYSTEM] Unloading assembly {assembly}");
                KaomiLoader.Unload(assembly);
            }
        }
    }
}
