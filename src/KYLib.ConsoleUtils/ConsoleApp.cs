using System;
using System.Collections.Generic;
using System.Text;

namespace KYLib.ConsoleUtils
{
	/// <summary>
	/// Representa una app de consola, esta clase debe ser heredada.
	/// </summary>
	public abstract class ConsoleApp : ConsoleMenu
	{
		/// <summary>
		/// Crea una nueva aplicación de consola.
		/// </summary>
		protected ConsoleApp(string appName) : base(true)
		{
			if (Console.IsOutputRedirected || Console.IsInputRedirected)
				throw new NotSupportedException("No se puede crear una aplicación de consola si se ridirige la entrada o la salida.");
			Title = appName + Environment.NewLine;
			Console.Title = appName;
		}
	}
}
