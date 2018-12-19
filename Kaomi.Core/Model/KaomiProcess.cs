using System;
using System.Collections.Generic;
using System.Text;

namespace Kaomi.Core.Model
{
    public abstract class KaomiProcess : MarshalByRefObject
    {
        internal abstract bool OneTime();

        public abstract void OnInitialize();

        public abstract void OnIteration();

        public abstract void OnUserMessage(string message);

        public abstract void OnFinalize();
    }
}
