using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaomi.Client.Model
{
    public class KaomiProcessHasResults
    {
        [JsonProperty("process")]
        public string Process { get; set; }
        [JsonProperty("hasResults")]
        public bool HasResults { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }

        public bool Valid()
        {
            return Error is null && Process != null;
        }
    }
}
