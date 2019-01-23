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
            ServerConsole._WriteLine("Looking for startup processes...");
        }

        public override void DoWork()
        {
            foreach (var proc in Config.SettingsIn("Startup"))
            {
                KaomiLoader.Load($"{proc.Key}.dll");
                KaomiLoader.InstanceProcess(proc.Key, proc.Value);
            }

            ServerConsole._WriteLine("All startup processes initialized.");
        }

        public override void OnFinalize()
        {

        }
        
        public override void OnUserMessage(string message)
        {

        }
    }
}
