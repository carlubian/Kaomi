using ConfigAdapter.Xml;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Kaomi.WebAPI
{
    public class Program
    {
        public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = XmlConfig.From("Kaomi.WebAPI.xml");
            if (!int.TryParse(config.Read("Server:Port"), out var port))
                port = 5000;

            return WebHost.CreateDefaultBuilder(args)
                          .UseStartup<Startup>()
                          .UseUrls($"http://*:{port}");
        }
    }
}
