using Kaomi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContinuousProcesses
{
    public class MessageProcess : KaomiProcess
    {
        private KaomiPluginConsole Console;

        public override void OnInitialize()
        {
            Console = Request<KaomiPluginConsole>();
            IterationDelay = TimeSpan.FromSeconds(15);
        }

        public override void OnIteration()
        {

        }

        public override void OnFinalize()
        {

        }
        
        public override void OnUserMessage(string message)
        {
            Console.WriteLine($"[MessageProcess] Received message: {message}");
        }
    }
}
