using Kaomi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigProcess
{
    public class MessageProcess : KaomiProcess
    {
        private ServerConsoleOutput ServerConsole;

        public override void OnInitialize()
        {
            base.IterationDelay = TimeSpan.FromSeconds(10);
            ServerConsole = Request<ServerConsoleOutput>();
        }

        public override void OnIteration()
        {

        }

        public override void OnFinalize()
        {

        }
        
        public override void OnUserMessage(string message)
        {
            ServerConsole.WriteLine($"[MessageProcess] {message}");
        }
    }
}
