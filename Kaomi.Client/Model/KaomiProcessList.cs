using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaomi.Client.Model
{
    public class KaomiProcessList
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("processes")]
        internal IEnumerable<string> processes { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }

        internal IpAddress ip;
        internal int port;
        internal KaomiAssembly assembly;

        public IEnumerable<KaomiProcess> ProcessList
        {
            get
            {
                if (processes is null)
                    yield break;
                else
                    foreach (var process in processes.Where(p => p.Split(' ')[0]
                        .Replace("[", "").Replace("]", "").Equals(assembly.Id)))
                        yield return new KaomiProcess(ip, port, assembly, process.Split(' ')[1]);
            }
        }

        public bool Valid()
        {
            if (Error is null && processes != null)
                return true;
            return false;
        }
    }
}
