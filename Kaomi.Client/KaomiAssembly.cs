using Kaomi.Client.Logic;
using Kaomi.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaomi.Client
{
    /// <summary>
    /// Represents one of the assemblies loaded
    /// in the Kaomi Server.
    /// </summary>
    public class KaomiAssembly
    {
        internal IpAddress ip;
        internal int port;
        public string Id { get; }

        internal KaomiAssembly(IpAddress ip, int port, string id)
        {
            this.ip = ip;
            this.port = port;
            Id = id;
        }

        /// <summary>
        /// Unloads this assembly from the server. All of its
        /// processes will be finalized before doing so.
        /// </summary>
        /// <returns></returns>
        public KaomiServerStatus Unload()
        {
            return Restquest.Get<KaomiServerStatus>(ip, port, $"Kaomi/Unload?path={Id}");
        }

        /// <summary>
        /// Instance and run a process contained in this assembly.
        /// </summary>
        /// <param name="name">Name of the process</param>
        /// <returns></returns>
        public KaomiServerStatus InstanceProcess(string name)
        {
            return Restquest.Get<KaomiServerStatus>(ip, port, $"Kaomi/InstanceProcess?id={Id}&type={name}");
            //TODO Return a KaomiProcess instead?
        }
    }
}
