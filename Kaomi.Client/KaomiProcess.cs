using Kaomi.Client.Logic;
using Kaomi.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaomi.Client
{
    /// <summary>
    /// Represents one of the processes running
    /// on the Kaomi server.
    /// </summary>
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

        /// <summary>
        /// Checks whether this process has pending results.
        /// </summary>
        /// <returns></returns>
        public KaomiProcessHasResults HasResults()
        {
            var name = Id.Split(' ')[1];
            return Restquest.Get<KaomiProcessHasResults>(ip, port, $"Kaomi/HasResults?process={name}");
        }

        /// <summary>
        /// Returns all available results for this process,
        /// emptying the queue as a result.
        /// </summary>
        /// <returns></returns>
        public KaomiProcessResult GetResults()
        {
            var name = Id.Split(' ')[1];
            return Restquest.Get<KaomiProcessResult>(ip, port, $"Kaomi/GetResults?process={name}");
        }

        /// <summary>
        /// Sends a message to this process.
        /// </summary>
        /// <param name="message">Text message</param>
        public KaomiServerStatus SendMessage(string message)
        {
            var name = Id.Split(' ')[1];
            return Restquest.Get<KaomiServerStatus>(ip, port, $"Kaomi/SendMessage?process={name}&message={Uri.EscapeDataString(message)}");
        }
    }
}
