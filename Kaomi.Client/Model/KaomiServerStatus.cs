using System;
using System.Collections.Generic;
using System.Text;

namespace Kaomi.Client.Model
{
    internal class KaomiServerStatus
    {
        public string status { get; set; }

        public string error { get; set; }

        internal bool Valid()
        {
            if (status != null && error is null)
                return true;
            else
                return false;
        }
    }
}
