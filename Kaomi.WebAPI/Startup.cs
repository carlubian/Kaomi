using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaomi.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kaomi.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // Make sure KaomiLoader class is loaded into memory.
            KaomiLoader.IsActive();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApplicationLifetime lifetime)
        {
            app.UseMvc();
            lifetime.ApplicationStopping.Register(OnShutdown);
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
