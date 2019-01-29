using System;
using System.Collections.Generic;
using System.Text;

namespace Kaomi.Client.Model
{
    internal class KaomiProcessList
    {
        public int count { get; set; }
        public IEnumerable<string> processes { get; set; }
        public string error { get; set; }

        public bool Valid()
        {
            if (error is null && processes != null)
                return true;
            return false;
        }
    }
}
