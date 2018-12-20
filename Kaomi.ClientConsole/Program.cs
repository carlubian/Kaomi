using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaomi.ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new KaomiWCF.KaomiClient();
            var uri = new Uri("https://onedrive.live.com/download?cid=5EF8A8FD6C11D715&resid=5EF8A8FD6C11D715%21626915&authkey=AGiNE3NP0WvSDzs");

            var fileinfo = client.DownloadAssembly("HelloWorldProcess", uri);

            Console.WriteLine("Assembly downloaded.");
            Console.ReadKey();
            Console.WriteLine("Attempting to instance component...");

            var pr = client.InstanceProcess(fileinfo, "HelloWorldProcess.OneTimeHelloWorld");

            Console.WriteLine("Process successfully instanced");
            Console.ReadKey();
            Console.WriteLine("Currently loaded processes:");

            foreach (var pid in client.ListProcesses())
                Console.WriteLine(pid);

            Console.ReadKey();
            Console.WriteLine("Attempting to unload process...");

            client.UnloadProcess(client.ListProcesses().First());

            Console.WriteLine("Process successfully unloaded");

            Console.ReadLine();
        }
    }
}
