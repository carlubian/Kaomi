using Kaomi.Client;
using System;

namespace Kaomi.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Kaomi Server IP address:");
            var ip = Console.ReadLine();

            Console.WriteLine("Enter Kaomi Server port (normally 5000):");
            var port = Console.ReadLine();

            // Connect to a server and verify connection
            Console.WriteLine("Connecting to server...");
            var server = KaomiServer.ConnectTo(ip, int.Parse(port));
            Console.WriteLine($"Kaomi Server listening?: {server.IsListening()}");

            // List active processes in the server
            Console.WriteLine();
            Console.WriteLine("Currently loaded processes:");
            foreach (var proc in server.AllProcesses())
                Console.WriteLine(proc);
            Console.WriteLine("Finished listing processes.");

            Console.ReadLine();
        }
    }
}
