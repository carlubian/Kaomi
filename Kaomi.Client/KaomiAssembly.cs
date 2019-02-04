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
        public KaomiServer Server { get; }

        internal KaomiAssembly(IpAddress ip, int port, KaomiServer server, string id)
        {
            this.ip = ip;
            this.port = port;
            Server = server;
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
        public KaomiProcess InstanceProcess(string name)
        {
            var kss = Restquest.Get<KaomiServerStatus>(ip, port, $"Kaomi/InstanceProcess?id={Id}&type={name}");

            if (kss.Valid())
                return new KaomiProcess(ip, port, this, name);
            return null;
        }

        /// <summary>
        /// Returns a list of all active processes from this assembly
        /// loaded into the memory of the Kaomi Server.
        /// </summary>
        /// <returns></returns>
        public KaomiProcessList AllProcesses()
        {
            var kpl = Restquest.Get<KaomiProcessList>(ip, port, "Kaomi/ListProcesses");
            kpl.ip = this.ip;
            kpl.port = this.port;
            kpl.assembly = this;

            return kpl;
        }

        public override bool Equals(object obj)
        {
            if (obj is KaomiAssembly ka)
                return ka.ip == this.ip && ka.port == this.port && ka.Id == this.Id;
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
