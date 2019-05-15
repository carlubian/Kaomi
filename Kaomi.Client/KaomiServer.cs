using Kaomi.Client.Logic;
using Kaomi.Client.Model;
using System;
using System.Collections.Generic;

[assembly: CLSCompliant(true)]
namespace Kaomi.Client
{
    /// <summary>
    /// Wraps the connection to a Kaomi Server, identified by
    /// an IP address and a port. Any number of servers can be
    /// concurrently used, as long as each one has a unique
    /// IP-Port combination.
    /// </summary>
    public class KaomiServer
    {
        private readonly IpAddress address;
        private readonly int port;

        private KaomiServer(IpAddress address, int port)
        {
            this.address = address;
            this.port = port;
        }
        
        /// <summary>
        /// Establishes a connection to a Kaomi Server. This method only
        /// checks the validity of the parameters. To verify that a server
        /// is listening, call .IsListening() on the result of this call.
        /// </summary>
        /// <param name="ipAddress">IP Address</param>
        /// <param name="port">Port number</param>
        /// <returns></returns>
        public static KaomiServer ConnectTo(string ipAddress, int port = 5000)
        {
            if (port <= 0)
                throw new ArgumentException("Port number cannot be negative", nameof(port));

            if (Validate.IpAddress(ipAddress, out var validIp))
                return new KaomiServer(validIp, port);
            else
                throw new ArgumentException("IP address has an incorrect format", nameof(ipAddress));
        }

        /// <summary>
        /// Checks whether there is a Kaomi Server listening on the
        /// endpoint specified by this instance of KaomiServer.
        /// </summary>
        /// <returns></returns>
        public bool IsListening() => Validate.ServerPresentAt(address, port);

        /// <summary>
        /// Downloads a file into the server execution directory.
        /// If the file is a Zip file, it will be automatically
        /// decompressed.
        /// </summary>
        /// <param name="filename">Name and extension of the file</param>
        /// <param name="uri">Uri pointing to the file</param>
        /// <returns></returns>
        public KaomiServerStatus PullFromUri(string filename, Uri uri)
        {
            return Restquest.Get<KaomiServerStatus>(address, port, $"Kaomi/PullFromUri?fileName?{filename}&uri={uri.ToString()}");
        }

        /// <summary>
        /// Load a DLL file from the server local directory into
        /// memory, allowing for subsequent process instantiation.
        /// </summary>
        /// <param name="asmPath">Name and extension of the assembly file</param>
        /// <returns></returns>
        public KaomiAssembly LoadAssembly(string asmPath)
        {
            var kla = Restquest.Get<KaomiLoadedAssembly>(address, port, $"Kaomi/Load?path={asmPath}");

            if (kla.Valid())
                return new KaomiAssembly(address, port, this, asmPath);
            return null;
        }

        /// <summary>
        /// Returns a list of all assemblies loaded into
        /// the memory of the Kaomi Server.
        /// </summary>
        /// <returns></returns>
        public KaomiAssemblyList AllAssemblies()
        {
            var kal = Restquest.Get<KaomiAssemblyList>(address, port, "Kaomi/ListAssemblies");
            kal.ip = this.address;
            kal.port = this.port;
            kal.server = this;

            return kal;
        }
    }
}
