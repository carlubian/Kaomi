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
        public KaomiAssembly Assembly { get; }
        public string Id { get; }

        internal KaomiProcess(IpAddress ip, int port, KaomiAssembly assembly, string id)
        {
            this.ip = ip;
            this.port = port;
            Assembly = assembly;
            Id = id;
        }

        /// <summary>
        /// Checks whether this process has pending results.
        /// </summary>
        /// <returns></returns>
        public KaomiProcessHasResults HasResults()
        {
            return Restquest.Get<KaomiProcessHasResults>(ip, port, $"Kaomi/HasResults?process={Id}");
        }

        /// <summary>
        /// Returns all available results for this process,
        /// emptying the queue as a result.
        /// </summary>
        /// <returns></returns>
        public KaomiProcessResult GetResults()
        {
            return Restquest.Get<KaomiProcessResult>(ip, port, $"Kaomi/GetResults?process={Id}");
        }

        /// <summary>
        /// Sends a message to this process.
        /// </summary>
        /// <param name="message">Text message</param>
        public KaomiServerStatus SendMessage(string message)
        {
            return Restquest.Get<KaomiServerStatus>(ip, port, $"Kaomi/SendMessage?process={Id}&message={Uri.EscapeDataString(message)}");
        }

        public override bool Equals(object obj)
        {
            if (obj is KaomiProcess kp)
                return kp.ip == this.ip && kp.port == this.port && kp.Id == this.Id;

            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = -2006699598;
            hashCode = hashCode * -1521134295 + EqualityComparer<IpAddress>.Default.GetHashCode(ip);
            hashCode = hashCode * -1521134295 + port.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            return hashCode;
        }
    }
}
