using Kaomi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneTimeProcesses
{
    public class OneTimeWithConfig : OneTimeProcess
    {
        private KaomiPluginConsole Console;
        private KaomiPluginConfiguration Config;

        public override void OnInitialize()
        {
            Console = Request<KaomiPluginConsole>();
            Config = Request<KaomiPluginConfiguration>();
        }

        public override void DoWork()
        {
            var message = Config.Read("Settings:Message");
            Console.WriteLine($"[OneTimeWithConfig] {message} from a Kaomi one-time process.");
        }

        public override void OnFinalize()
        {

        }
        
        public override void OnUserMessage(string message)
        {

        }
    }
}
