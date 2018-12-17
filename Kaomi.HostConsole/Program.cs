using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Kaomi.HostConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:1701/");
            ServiceHost selfHost = new ServiceHost(typeof(WCF.Kaomi), baseAddress);
            selfHost.AddServiceEndpoint(typeof(WCF.IKaomi), new WSHttpBinding(), "Kaomi");

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            selfHost.Description.Behaviors.Add(smb);

            selfHost.Open();
            Console.WriteLine("The service is ready.");
            Console.WriteLine("Press <ENTER> to terminate service.");
            Console.WriteLine();
            Console.ReadLine();

            selfHost.Close();
        }
    }
}
