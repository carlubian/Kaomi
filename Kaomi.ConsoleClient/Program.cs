using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Kaomi.ConsoleClient
{
    class Program
    {
        private static string UriEndpoint = "https://localhost:5001";
        private static string AsmLocation = Uri.EscapeDataString("https://onedrive.live.com/download?cid=5EF8A8FD6C11D715&resid=5EF8A8FD6C11D715%21626915&authkey=AGiNE3NP0WvSDzs");

        // Dependencies
        private static string ConfigAdapter = Uri.EscapeDataString("https://onedrive.live.com/download?cid=5EF8A8FD6C11D715&resid=5EF8A8FD6C11D715%21643205&authkey=AHjB_haZbk8vwoc");
        private static string DotNetExtensions = Uri.EscapeDataString("https://onedrive.live.com/download?cid=5EF8A8FD6C11D715&resid=5EF8A8FD6C11D715%21643206&authkey=ALhw9x4mhOPiMzg");
        private static string HJson = Uri.EscapeDataString("https://onedrive.live.com/download?cid=5EF8A8FD6C11D715&resid=5EF8A8FD6C11D715%21643207&authkey=AGNUr09eKCvYQpk");
        private static string INIFileParser = Uri.EscapeDataString("https://onedrive.live.com/download?cid=5EF8A8FD6C11D715&resid=5EF8A8FD6C11D715%21643208&authkey=AGZpeDbsvxeQZlk");
        // Configuration file
        private static string ConfigFile = Uri.EscapeDataString("https://onedrive.live.com/download?cid=5EF8A8FD6C11D715&resid=5EF8A8FD6C11D715%21643211&authkey=AAr4IfVa32lCooo");

        static void Main(string[] args)
        {
            DoMain().Wait();
        }

        private static async Task DoMain()
        {
            Console.WriteLine("Kaomi console test client.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Make sure Kaomi.WebAPI is running on Kestrel before continuing.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadLine();

            var client = new HttpClient();

            Console.WriteLine("Pulling assembly from URI...");
            await client.GetAsync($"{UriEndpoint}/Kaomi/PullFromUri?asmName=ConfigAdapter.dll&uri={ConfigAdapter}");
            await client.GetAsync($"{UriEndpoint}/Kaomi/PullFromUri?asmName=DotNet.Misc.Extensions.dll&uri={DotNetExtensions}");
            await client.GetAsync($"{UriEndpoint}/Kaomi/PullFromUri?asmName=HJson.dll&uri={HJson}");
            await client.GetAsync($"{UriEndpoint}/Kaomi/PullFromUri?asmName=INIFileParserDotNetCore.dll&uri={INIFileParser}");
            await client.GetAsync($"{UriEndpoint}/Kaomi/PullFromUri?asmName=HelloWorldConfig.ini&uri={ConfigFile}");
            var resp = await client.GetAsync($"{UriEndpoint}/Kaomi/PullFromUri?asmName=HelloWorldProcess.dll&uri={AsmLocation}");
            Console.WriteLine($"Response: {await resp.Content.ReadAsStringAsync()}");
            Console.ReadLine();

            Console.WriteLine("Loading assembly into memory...");
            resp = await client.GetAsync($"{UriEndpoint}/Kaomi/Load?path=HelloWorldProcess.dll");
            Console.WriteLine($"Response: {await resp.Content.ReadAsStringAsync()}");
            Console.ReadLine();

            Console.WriteLine("Executing process...");
            resp = await client.GetAsync($"{UriEndpoint}/Kaomi/InstanceProcess?id=HelloWorldProcess&type=ConfigHelloWorld");
            Console.WriteLine($"Response: {await resp.Content.ReadAsStringAsync()}");
            Console.WriteLine("Active processes:");
            resp = await client.GetAsync($"{UriEndpoint}/Kaomi/ListProcesses");
            Console.WriteLine(await resp.Content.ReadAsStringAsync());
            Console.ReadLine();

            Console.WriteLine("Unloading assembly from memory...");
            resp = await client.GetAsync($"{UriEndpoint}/Kaomi/Unload?path=HelloWorldProcess");
            Console.WriteLine($"Response: {await resp.Content.ReadAsStringAsync()}");
            Console.ReadLine();

            Console.WriteLine("Active processes:");
            resp = await client.GetAsync($"{UriEndpoint}/Kaomi/ListProcesses");
            Console.WriteLine(await resp.Content.ReadAsStringAsync());
            Console.ReadLine();
            client.Dispose();
        }
    }
}
