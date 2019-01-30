using Kaomi.Client;
using System;
using System.Linq;

namespace Kaomi.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteHeader("Enter Kaomi Server IP address:");
            var ip = Console.ReadLine();

            WriteHeader("Enter Kaomi Server port (normally 5000):");
            var port = Console.ReadLine();

            // Connect to a server and verify connection
            WriteHeader("Connecting to server...");
            var server = KaomiServer.ConnectTo(ip, int.Parse(port));
            Console.WriteLine($"Kaomi Server listening?: {server.IsListening()}");

            // List active processes in the server
            Console.WriteLine();
            var processList = server.AllProcesses();
            Console.WriteLine($"Process list valid?: {processList.Valid()}");
            
            WriteHeader("List of active processes:");
            foreach (var process in processList.ProcessList)
                Console.WriteLine(process.Id);
            WriteHeader("Finished listing processes.");

            // Get results from a process
            Console.WriteLine();
            WriteHeader("Write the index of a process (starting at 0):");
            var index = int.Parse(Console.ReadLine());

            var prc = processList.ProcessList.Skip(index).First();
            Console.WriteLine($"Process {prc.Id} has results?: {prc.HasResults()}");
            WriteHeader($"List results of {prc.Id}:");

            var results = prc.GetResults();
            WriteHeader($"Showing {results.Count} results...");
            foreach (var result in results.Results)
                Console.WriteLine(result);
            WriteHeader("Finished listing results.");

            // Send message to a process
            WriteHeader($"Write a message for {prc.Id}:");
            var msg = Console.ReadLine();
            prc.SendMessage(msg);
            WriteHeader("Message sent to the process.");

            Console.ReadLine();
        }

        private static void WriteHeader(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
