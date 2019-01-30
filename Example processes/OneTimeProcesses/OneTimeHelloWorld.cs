using Kaomi.Core.Model;
using System;

namespace OneTimeProcesses
{
    public class OneTimeHelloWorld : OneTimeProcess
    {
        private KaomiPluginConsole Console;

        public override void OnInitialize()
        {
            Console = Request<KaomiPluginConsole>();
        }

        public override void DoWork()
        {
            Console.WriteLine("[OneTimeHelloWorld] Hello, world from a Kaomi one-time process.");
        }

        public override void OnFinalize()
        {

        }
        
        public override void OnUserMessage(string message)
        {

        }
    }
}
