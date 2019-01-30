using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaomi.Client.Model
{
    public class KaomiProcessResult
    {
        [JsonProperty("process")]
        public string Process { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("results")]
        public IEnumerable<string> Results { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }

        public bool Valid()
        {
            if (Error is null && Results != null)
                return true;
            return false;
        }
    }
}
