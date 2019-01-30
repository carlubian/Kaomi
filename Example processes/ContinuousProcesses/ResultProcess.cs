using Kaomi.Core.Model;
using System;

namespace ContinuousProcesses
{
    public class ResultProcess : KaomiProcess
    {
        private int min = 0;

        public override void OnInitialize()
        {
            IterationDelay = TimeSpan.FromMinutes(1);
        }

        public override void OnIteration()
        {
            NotifyResult($"{nameof(ResultProcess)} has been executing for {min++} minutes.");
        }

        public override void OnFinalize()
        {

        }
        
        public override void OnUserMessage(string message)
        {

        }
    }
}
