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

            // List loaded assemblies in the server
            Console.WriteLine();
            var assemblyList = server.AllAssemblies();
            Console.WriteLine($"Assembly list valid?: {assemblyList.Valid()}");
            
            WriteHeader("List of loaded assemblies and their processes:");
            foreach (var assembly in assemblyList.AssemblyList)
            {
                Console.WriteLine(assembly.Id);
                foreach (var process in assembly.AllProcesses().ProcessList)
                    Console.WriteLine($"    {process.Id}");
            }
            WriteHeader("Finished listing assemblies.");

            // Get results from a process
            Console.WriteLine();
            WriteHeader("Write the name of an assembly:");
            var asm = Console.ReadLine();
            WriteHeader($"Write the name of a process inside {asm}:");
            var proc = Console.ReadLine();

            var prc = assemblyList.AssemblyList.First(a => a.Id.Equals(asm))
                .AllProcesses().ProcessList.First(p => p.Id.Equals(proc));
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
