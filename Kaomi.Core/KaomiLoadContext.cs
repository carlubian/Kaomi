using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Kaomi.Core
{
    public class KaomiLoadContext : AssemblyLoadContext
    {
        public KaomiLoadContext() : base(isCollectible: true)
        {
            this.Resolving += (loadContext, name) =>
            {
                using (var stream = File.OpenRead($"{name.Name}.dll"))
                    return this.LoadFromStream(stream);
            };
        }

        protected override Assembly Load(AssemblyName assemblyName) => null;
    }
}
