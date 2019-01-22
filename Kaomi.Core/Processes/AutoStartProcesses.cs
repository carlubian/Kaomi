using Kaomi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaomi.Core
{
    internal class AutoStartProcesses : OneTimeProcess
    {
        private ServerConsoleOutput ServerConsole;
        private ConfigFileManager Config;

        public override void OnInitialize()
        {
            ServerConsole = Request<ServerConsoleOutput>();
            Config = Request<ConfigFileManager>();
            ServerConsole.WriteLine("Looking for startup processes...");
        }

        public override void DoWork()
        {
            // Temporary
            var type = Config.Read("Startup:ConfigProcess");
            KaomiLoader.Load("ConfigProcess.dll");
            KaomiLoader.InstanceProcess("ConfigProcess", type);
            //
            ServerConsole.WriteLine("All startup processes initialized.");
        }

        public override void OnFinalize()
        {

        }
        
        public override void OnUserMessage(string message)
        {

        }
    }
}
