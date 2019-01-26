using Kaomi.Core.Model;
using System;
using System.Reflection;

namespace ConfigProcess
{
    public class ResultsClass : KaomiProcess
    {
        private ServerConsoleOutput ServerConsole;
        private ConfigFileManager Config;

        public override void OnInitialize()
        {
            ServerConsole = Request<ServerConsoleOutput>();
            Config = Request<ConfigFileManager>();
            base.IterationDelay = TimeSpan.FromMinutes(1);
        }

        public override void OnIteration()
        {
            var message = Config.Read("Message");
            base.NotifyResult(message);
            ServerConsole.WriteLine("Process result is now on result queue.", OutputKind.Warning);
        }

        public override void OnFinalize()
        {

        }
        
        public override void OnUserMessage(string message)
        {

        }
    }
}
