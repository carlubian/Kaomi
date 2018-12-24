using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kaomi.Core.Model
{
    internal class KaomiTaskHost
    {
        internal string AssemblyId { get; set; }
        internal KaomiProcess Process { get; set; }
        internal bool Finalize { get; set; }
        internal string UserCommand { get; set; }

        private Task Task { get; set; }

        /// <summary>
        /// Create a new Task Host object.
        /// </summary>
        /// <param name="assemblyId">ID of the assembly that contains the type to run.</param>
        /// <param name="process">Instance of KaomiProcessd</param>
        public KaomiTaskHost(string assemblyId, KaomiProcess process)
        {
            AssemblyId = assemblyId;
            Process = process;
            Finalize = false;
            UserCommand = null;

            ScaffoldAndRun();
        }

        /// <summary>
        /// Creates the wrapper task and starts it.
        /// </summary>
        private void ScaffoldAndRun()
        {
            Task = new Task(RunProcess, TaskCreationOptions.LongRunning);
            Task.Start();
        }

        /// <summary>
        /// Logic to run a KaomiProcess. This method will run inside a Task.
        /// </summary>
        private void RunProcess()
        {
            // Initialize the process
            Process.OnInitialize();

            // Main process loop
            while (!Finalize)
            {
                // One-Time processes should iterate only once
                if (Process is OneTimeProcess)
                    Finalize = true;

                // Check for user messages
                if (UserCommand != null)
                {
                    Process.OnUserMessage(UserCommand);
                    UserCommand = null;
                }

                // Do an iteration
                Process.OnIteration();

                // Check whether process is requesting to finalize
                if (Process.RequestFinalization)
                    Finalize = true;

                // TODO if process iteration can be delayed, it will go here.
            }

            // Finalize the process
            Process.OnFinalize();
        }
    }
}
