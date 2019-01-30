using Kaomi.Client.Logic;
using Kaomi.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaomi.Client
{
    public class KaomiProcess
    {
        private IpAddress ip;
        private int port;
        public string Id { get; }

        internal KaomiProcess(IpAddress ip, int port, string id)
        {
            this.ip = ip;
            this.port = port;
            Id = id;
        }

        public bool HasResults()
        {
            var name = Id.Split(' ')[1];
            var hasResults = Restquest.Get<KaomiProcessHasResults>(ip, port, $"Kaomi/HasResults?process={name}");
            return hasResults.Valid() ? 
                hasResults.HasResults : 
                throw new ArgumentException($"Process {Id} had a problem checking for results.");
        }

        public KaomiProcessResult GetResults()
        {
            var name = Id.Split(' ')[1];
            return Restquest.Get<KaomiProcessResult>(ip, port, $"Kaomi/GetResults?process={name}");
        }

        public void SendMessage(string message)
        {
            var name = Id.Split(' ')[1];
            Restquest.Get<KaomiServerStatus>(ip, port, $"Kaomi/SendMessage?process={name}&message={Uri.EscapeDataString(message)}");
        }
    }
}
