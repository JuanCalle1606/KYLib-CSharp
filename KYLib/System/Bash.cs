using System;
using System.Diagnostics;

namespace KYLib.System
{
	/// <summary>
	/// Clase que provee metodos para ejecutar comandos por medio de la terminal o para abrir otros programas.
	/// </summary>
	public static class Bash
	{

		/// <summary>
		/// Ejecuta un comando por terminal.
		/// </summary>
		/// <param name="bash">Comando a ejecutar, con parametros</param>
		public static string Command(string bash)
		{
			throw new NotImplementedException("El bash multiplataforma aun no ha sido implementado");
		}

		/// <summary>
		/// Ejecuta un comando por terminal en sistemas unix.
		/// Este metodo espera a que el proceso acabe para luego devolver toda su salida.
		/// </summary>
		/// <param name="bash">Comando a ejecutar, con parametros</param>
		/// <returns>Devuelve la salida del progarama.</returns>
		public static string GetCommandUnix(string bash)
		{
			Process process = CommandUnixCore(bash, true);
			string result = "";
			process.OutputDataReceived += (o, e) => result += e.Data + Environment.NewLine;
			process.Start();
			process.BeginOutputReadLine();
			process.WaitForExit();
			process.Close();
			process.Dispose();
			return result;
		}

		public static void GetCommandUnix(string bash, Action<string> callback)
		{
			Process process = CommandUnixCore(bash, true);
			process.OutputDataReceived += (o, e) => callback?.Invoke(e.Data);
			process.Start();
			process.BeginOutputReadLine();
			process.WaitForExit();
			process.Close();
			process.Dispose();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="bash"></param>
		public static void CommandUnix(string bash)
		{
			Process process = CommandUnixCore(bash, false);
			process.Start();
			process.WaitForExit();
			process.Close();
			process.Dispose();
		}

		private static Process CommandUnixCore(string bash, bool redirect)
		{
			string escapedArgs = bash.Replace("\"", "\\\"");
			return new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "/bin/bash",
					Arguments = $"-c \"{escapedArgs}\"",
					RedirectStandardOutput = redirect,
					UseShellExecute = false,
					CreateNoWindow = true
				},
				EnableRaisingEvents = true
			};
		}
	}
}