using Kaomi.Core.Model;
using System;

namespace HelloWorldProcess
{
    public class OneTimeHelloWorld : OneTimeProcess
    {
        public override void OnFinalize() => throw new NotImplementedException();
        public override void OnInitialize() => throw new NotImplementedException();
        public override void OnIteration() => Console.WriteLine("Hello World from Kaomi process.");
        public override void OnUserMessage(string message) => throw new NotImplementedException();
    }
}
