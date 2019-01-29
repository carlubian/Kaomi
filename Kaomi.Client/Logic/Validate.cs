using Kaomi.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaomi.Client.Logic
{
    internal static class Validate
    {
        internal static bool IpAddress(string text, out IpAddress address)
        {
            address = null;
            var parts = text.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 4)
                return false;

            if (!parts.All(str => byte.TryParse(str, out _)))
                return false;

            address = new IpAddress(parts.Select(str => byte.Parse(str)).ToArray());
            return true;
        }

        internal static bool ServerPresentAt(IpAddress address, int port)
        {
            var status = Restquest.Get<KaomiServerStatus>(address, port, "Kaomi");

            return status.Valid();
        }
    }
}
