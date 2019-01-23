using System;
using System.Collections.Generic;
using System.Text;

namespace Kaomi.Core.Model
{
    public class ServerConsoleOutput : KaomiPlugin
    {
        ConsoleColor fore, back;

        public override void Initialize(string callingAssembly)
        {
            fore = ConsoleColor.Gray;
            back = ConsoleColor.Black;
        }

        public void WriteLine(object content, OutputKind kind = OutputKind.Info)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            switch (kind)
            {
                case OutputKind.Info:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case OutputKind.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case OutputKind.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }

            Console.WriteLine(content.ToString());

            Console.ForegroundColor = fore;
            Console.BackgroundColor = back;
        }

        internal void _WriteLine(object content)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.WriteLine($"[SYSTEM] {content.ToString()}");

            Console.ForegroundColor = fore;
            Console.BackgroundColor = back;
        }
    }

    public enum OutputKind
    {
        Info,
        Warning,
        Error
    }
}
