using Kaomi.Core.Model;
using System;

namespace HelloWorldProcess
{
    public class OneTimeHelloWorld : OneTimeProcess
    {
        public override void OnInitialize() => Console.WriteLine("Initializing process...");

        public override void DoWork() => Console.WriteLine("Hello, World from a one time Kaomi Process.");

        public override void OnFinalize() => Console.WriteLine("Finalizing process...");

        public override void OnUserMessage(string message) => Console.WriteLine($"User has sent {message}");
    }
}
