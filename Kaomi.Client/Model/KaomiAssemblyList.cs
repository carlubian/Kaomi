using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaomi.Client.Model
{
    public class KaomiAssemblyList
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("assemblies")]
        internal IEnumerable<string> assemblies { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }

        internal KaomiServer server;
        internal IpAddress ip;
        internal int port;

        public IEnumerable<KaomiAssembly> AssemblyList
        {
            get
            {
                if (assemblies is null)
                    yield break;
                else
                    foreach (var assembly in assemblies)
                        yield return new KaomiAssembly(ip, port, server, assembly);
            }
        }

        public bool Valid()
        {
            if (Error is null && assemblies != null)
                return true;
            return false;
        }
    }
}
