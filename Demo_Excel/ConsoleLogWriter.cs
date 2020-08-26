using System;

namespace Demo_Excel
{
    internal class ConsoleLogWriter
    {
        public static void WritelineMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
        }
    }
}
