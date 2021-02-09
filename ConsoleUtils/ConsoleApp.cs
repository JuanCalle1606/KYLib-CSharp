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
		protected ConsoleApp(string appName) : base(true) => Title = appName;
	}
}
