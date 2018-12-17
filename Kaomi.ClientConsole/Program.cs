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
            var uri = new Uri("https://onedrive.live.com/download?cid=5EF8A8FD6C11D715&resid=5EF8A8FD6C11D715%21613990&authkey=ADGf8kUzDqCd7AM");

            var fileinfo = client.DownloadAssembly("CustomProject", uri);
            var id = client.CreateAppDomain(fileinfo);

            foreach (var appd in client.ListAppDomains())
                Console.WriteLine(appd);

            Console.ReadLine();
        }
    }
}
