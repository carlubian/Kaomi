using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public IEnumerable<KaomiProcess> ProcessList
        {
            get
            {
                if (processes is null)
                    yield break;
                else
                    foreach (var process in processes)
                        yield return new KaomiProcess(ip, port, process);
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
