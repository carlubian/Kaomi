using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Kaomi.ConsoleClient
{
    class Program
    {
        private static string UriEndpoint = "https://localhost:5001";
        private static string AsmLocation = Uri.EscapeDataString("https://onedrive.live.com/download?cid=5EF8A8FD6C11D715&resid=5EF8A8FD6C11D715%21626915&authkey=AGiNE3NP0WvSDzs");

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
            var resp = await client.GetAsync($"{UriEndpoint}/Kaomi/PullFromUri?asmName=HelloWorldProcess.dll&uri={AsmLocation}");
            Console.WriteLine($"Response: {await resp.Content.ReadAsStringAsync()}");
            Console.ReadLine();

            Console.WriteLine("Loading assembly into memory...");
            resp = await client.GetAsync($"{UriEndpoint}/Kaomi/Load?path=HelloWorldProcess.dll");
            Console.WriteLine($"Response: {await resp.Content.ReadAsStringAsync()}");
            Console.ReadLine();

            Console.WriteLine("Executing process...");
            resp = await client.GetAsync($"{UriEndpoint}/Kaomi/InstanceProcess?id=HelloWorldProcess&type=OneTimeHelloWorld");
            Console.WriteLine($"Response: {await resp.Content.ReadAsStringAsync()}");
            Console.ReadLine();

            Console.WriteLine("Unloading assembly from memory...");
            resp = await client.GetAsync($"{UriEndpoint}/Kaomi/Unload?path=HelloWorldProcess");
            Console.WriteLine($"Response: {await resp.Content.ReadAsStringAsync()}");
            Console.ReadLine();

            Console.ReadLine();
            client.Dispose();
        }
    }
}
