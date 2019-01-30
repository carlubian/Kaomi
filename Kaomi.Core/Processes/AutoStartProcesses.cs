using Kaomi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaomi.Core
{
    internal class AutoStartProcesses : OneTimeProcess
    {
        private KaomiPluginConsole ServerConsole;
        private KaomiPluginConfiguration Config;

        public override void OnInitialize()
        {
            ServerConsole = Request<KaomiPluginConsole>();
            Config = Request<KaomiPluginConfiguration>();
            ServerConsole._WriteLine("Looking for startup processes...");
        }

        public override void DoWork()
        {
            foreach (var proc in Config.SettingsIn("Startup"))
            {
                try
                {
                    KaomiLoader.Load($"{proc.Key}.dll");
                }
                catch { } // The assembly could already be loaded
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
