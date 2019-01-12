using System;
using System.Collections.Generic;
using System.Text;

namespace Kaomi.Core.Model
{
    /// <summary>
    /// Represents a process that will run continuously
    /// on a Kaomi Task Host.
    /// </summary>
    public abstract class KaomiProcess : MarshalByRefObject
    {
        internal bool RequestFinalization { get; set; } = false;

        public TimeSpan IterationDelay { get; set; } = TimeSpan.FromSeconds(1);

        public T Request<T>() where T: KaomiPlugin, new()
        {
            var plugin = new T();
            plugin.Initialize();
            return plugin;
        }

        public abstract void OnInitialize();

        public abstract void OnIteration();

        public abstract void OnUserMessage(string message);

        public abstract void OnFinalize();
    }
}
