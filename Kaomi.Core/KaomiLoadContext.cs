using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Kaomi.Core
{
    public class KaomiLoadContext : AssemblyLoadContext
    {
        public KaomiLoadContext() : base(isCollectible: true)
        {
        }

        protected override Assembly Load(AssemblyName assemblyName) => null;
    }
}
