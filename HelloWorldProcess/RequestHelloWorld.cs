using Kaomi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorldProcess
{
    public class RequestHelloWorld : OneTimeProcess
    {
        private ServerConsoleOutput ServerConsole;

        public override void OnInitialize()
        {
            ServerConsole = Request<ServerConsoleOutput>();
        }

        public override void DoWork()
        {
            ServerConsole.WriteLine("Information message", OutputKind.Info);
            ServerConsole.WriteLine("Warning message", OutputKind.Warning);
            ServerConsole.WriteLine("Error message", OutputKind.Error);
        }

        public override void OnUserMessage(string message)
        {

        }

        public override void OnFinalize()
        {

        }
    }
}
