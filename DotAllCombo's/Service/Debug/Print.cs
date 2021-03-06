﻿namespace DotaAllCombo.Service.Debug
{
	using System;
	using Ensage;

    internal class Print
	{
		public class LogMessage
		{
			public static void Error(string text, params object[] arguments)
			{
				Game.PrintMessage("<font color='#ff0000'>" + text + "</font>");
			}

			public static void Success(string text, params object[] arguments)
			{
				Game.PrintMessage("<font color='#00ff00'>" + text + "</font>");
			}

			public static void Info(string text, params object[] arguments)
			{
				Game.PrintMessage("<font color='#ffffff'>" + text + "</font>");
			}
		} // Console class

		public class ConsoleMessage
		{
			public static void Encolored(string text, ConsoleColor color, params object[] arguments)
			{
				var clr = Console.ForegroundColor;
				Console.ForegroundColor = color;
				Console.WriteLine(text, arguments);
				Console.ForegroundColor = clr;
			}

			public static void Error(string text, params object[] arguments)
			{
				Encolored(text, ConsoleColor.Red, arguments);
			}

			public static void Success(string text, params object[] arguments)
			{
				Encolored(text, ConsoleColor.Green, arguments);
			}

			public static void Info(string text, params object[] arguments)
			{
				Encolored(text, ConsoleColor.Yellow, arguments);
			}
		} 
	} 
}
