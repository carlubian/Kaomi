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
            var id = client.CreateAppDomain(fileinfo);

            Console.WriteLine("Current AppDomains:");
            foreach (var appd in client.ListAppDomains())
                Console.WriteLine(appd.ToString());

            Console.ReadKey();
            Console.WriteLine("Attempting to instance component...");

            var pr = client.InstanceProcess(id, "HelloWorldProcess.OneTimeHelloWorld");

            Console.WriteLine("Process successfully instanced");

            Console.ReadLine();
        }
    }
}
