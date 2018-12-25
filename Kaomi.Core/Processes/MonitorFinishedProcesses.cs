using Kaomi.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaomi.Core
{
    /// <summary>
    /// System process that monitors the process queue
    /// removing finished processes.
    /// </summary>
    /// <remarks>
    /// This process can access the internal field prcs
    /// in KaomiLoader, unlike user processes.
    /// </remarks>
    internal class MonitorFinishedProcesses : KaomiProcess
    {
        public override void OnInitialize()
        {
            // Iterate once every 10 seconds
            base.IterationDelay = TimeSpan.FromSeconds(10);
        }

        public override void OnIteration()
        {
            // These processes are either finished or about to be finalized
            var prToRemove = KaomiLoader.prcs.Where(kvp => kvp.Value.Finalize)
                .Select(kvp => kvp.Key)
                .ToArray();

            foreach (var pr in prToRemove)
                KaomiLoader.prcs.Remove(pr);
        }

        public override void OnFinalize()
        {
            // System processes should only finalize when shutting down Kaomi
            Console.WriteLine($"System process {nameof(MonitorFinishedProcesses)} has been terminated.");
        }
        
        public override void OnUserMessage(string message)
        {
            // MonitorFinishedProcesses has no user commands
        }
    }
}
