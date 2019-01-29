using System;
using System.Collections.Generic;
using System.Text;

namespace Kaomi.Client.Model
{
    internal class IpAddress
    {
        private readonly byte[] _content;

        internal IpAddress(byte[] content)
        {
            _content = content;
        }

        public override string ToString() => $"{_content[0]}.{_content[1]}.{_content[2]}.{_content[3]}";
    }
}
