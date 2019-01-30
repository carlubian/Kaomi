using Kaomi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContinuousProcesses
{
    public class FinalizeProcess : KaomiProcess
    {
        private KaomiPluginConsole Console;
        private int maxIter = 3;
        private int iter = 0;

        public override void OnInitialize()
        {
            Console = Request<KaomiPluginConsole>();
            IterationDelay = TimeSpan.FromSeconds(10);
        }

        public override void OnIteration()
        {
            iter++;

            if (iter == maxIter)
                RequestFinalization = true;

            Console.WriteLine($"[FinalizeProcess] Running iteration {iter}/{maxIter}");
        }

        public override void OnFinalize()
        {

        }
        
        public override void OnUserMessage(string message)
        {

        }
    }
}
