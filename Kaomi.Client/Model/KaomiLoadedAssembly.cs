using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaomi.Client.Model
{
    public class KaomiLoadedAssembly
    {
        [JsonProperty("asmId")]
        public string Assembly { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }

        public bool Valid()
        {
            if (Assembly != null && Error is null)
                return true;
            else
                return false;
        }
    }
}
