using System;
using System.Collections.Generic;
using System.Text;

namespace Kaomi.Core.Model
{
    public abstract class KaomiPlugin
    {
        public abstract void Initialize(string callingAssembly);
    }
}
