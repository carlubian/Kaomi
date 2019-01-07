using ConfigAdapter;
using Kaomi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorldProcess
{
    public class ConfigHelloWorld : OneTimeProcess
    {
        private string Message { get; set; }

        public override void OnInitialize()
        {
            Message = Config.From("HelloWorldConfig.ini").Read("Settings:Message");
        }

        public override void DoWork()
        {
            Console.WriteLine($"{Message} from a One-Time Kaomi process.");
        }

        public override void OnUserMessage(string message)
        {

        }

        public override void OnFinalize()
        {

        }
    }
}
