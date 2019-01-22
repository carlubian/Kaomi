using Kaomi.Core.Model;
using System;
using System.Reflection;

namespace ConfigProcess
{
    public class ConfigClass : OneTimeProcess
    {
        private ServerConsoleOutput ServerConsole;
        private ConfigFileManager Config;

        public override void OnInitialize()
        {
            ServerConsole = Request<ServerConsoleOutput>();
            Config = Request<ConfigFileManager>();
        }

        public override void DoWork()
        {
            var message = Config.Read("Message");
            ServerConsole.WriteLine(message, OutputKind.Warning);
        }

        public override void OnFinalize()
        {

        }
        
        public override void OnUserMessage(string message)
        {

        }
    }
}
