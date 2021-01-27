using System;
using System.Collections.Generic;
using System.Text;

namespace KYLib.ConsoleUtils
{
	public static class Cons
	{

		public static string DefaultText { get; set; } = "";

		public static string DefaultErrorText { get; set; } = "";

		public static string Line { get => Console.ReadLine(); set => Console.WriteLine(value); }

		public static object Key { get => Console.ReadKey(); set => Console.ReadKey(); }

		public static int Int {
			get{
				try{
					Line = DefaultText;
					return int.Parse(Line);
				}
				catch (Exception){
					Line = DefaultErrorText;
					return Int;
				}
			}
		}

		public static int GetInt(string text,string errorText){
			try{
				Line = text;
				return int.Parse(Line);
			}
			catch (Exception){
				Line = errorText;
				return GetInt("", errorText);
			}
		}

		public static void Trace(object obj) => Console.WriteLine(obj);
	}
}
