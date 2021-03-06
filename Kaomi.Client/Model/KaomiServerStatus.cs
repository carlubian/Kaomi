﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaomi.Client.Model
{
    public class KaomiServerStatus
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }

        public bool Valid()
        {
            if (Status != null && Error is null)
                return true;
            else
                return false;
        }
    }
}
