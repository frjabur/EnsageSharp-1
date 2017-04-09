using System;

namespace BeAwarePlus
{
    internal static class Others
    {
        public static void Init()
        {
            PrintSuccess(">>>>>> BeAwarePlus Loaded!");
        }
        internal static void PrintSuccess(string text, params object[] arguments)
        {
            PrintEncolored(text, ConsoleColor.Green, arguments);
        }
        private static void PrintEncolored(string text, ConsoleColor color, params object[] arguments)
        {
            var clr = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text, arguments);
            Console.ForegroundColor = clr;
        }
    }
}
