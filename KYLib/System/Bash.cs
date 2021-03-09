using System;
using System.Diagnostics;

namespace KYLib.System
{
	/// <summary>
	/// Clase que provee metodos para ejecutar comandos por medio de la terminal o para abrir otros programas.
	/// </summary>
	public static class Bash
	{
		///	GetCommand: ejecuta y espera a por la salida
		/// GetCommandAsync: ejecuta en segundo plano y cuando acaba obtiene la salida.
		/// RunCommand: ejecuta directamente en la terminal de forma interactiva, no se controla ni entrada ni salida controlada.
		/// Command:ejecuta en segundo plano pero controlando tanto la salida como la entrada.

		/// <summary>
		/// Ejecuta un proceso en el terminal y devuelve la salida de ese proceso.
		/// </summary>
		/// <remarks>
		/// NO se debe utilizar este metodo para ejecutar procesos que soliciten entrada de datos ya que este metodo solo captura las salidas, si se usa en un metodo que solicita entrada se producira un bloqueo porque siempre se estara esperando por una entrada.
		/// </remarks>
		/// <param name="bash"></param>
		/// <returns></returns>
		public static string GetCommand(string bash) => GetCommand(Info.CurrentSystem, bash);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="os"></param>
		/// <param name="bash"></param>
		/// <returns></returns>
		public static string GetCommand(OS os, string bash)
		{
			string dev;
			using (var process = CreateProcess(os, bash, true))
			{
				process.Start();
				process.WaitForExit();
				dev = process.StandardOutput.ReadToEnd();
			}
			return dev;
		}

		private static Process CreateProcess(OS os, string bash, bool redirect)
		{
			string escapedArgs = bash.Replace("\"", "\\\"");
			string[] path = Info.GetTerminalPath(os).Split(' ');

			return new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = path[0],
					Arguments = $"{path[1]} \"{escapedArgs}\"",
					RedirectStandardOutput = redirect,
					UseShellExecute = false,
					CreateNoWindow = true
				},
				EnableRaisingEvents = true
			};
		}
	}
}