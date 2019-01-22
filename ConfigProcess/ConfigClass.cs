using Kaomi.Core.Model;
using System;
using System.Reflection;

namespace ConfigProcess
{
    public class ConfigClass : OneTimeProcess
    {
        public override void OnInitialize()
        {
            Console.WriteLine(Assembly.GetExecutingAssembly());
        }

        public override void DoWork()
        {

        }

        public override void OnFinalize()
        {

        }
        
        public override void OnUserMessage(string message)
        {

        }
    }
}
