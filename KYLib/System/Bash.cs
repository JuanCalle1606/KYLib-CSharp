using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KYLib.System
{
	/// <summary>
	/// Clase que provee metodos para ejecutar comandos por medio de la terminal o para abrir otros programas.
	/// </summary>
	public static class Bash
	{
		/// <summary>
		/// Ejecuta un proceso en el terminal y devuelve la salida de ese proceso.
		/// </summary>
		/// <remarks>
		/// NO se debe utilizar este metodo para ejecutar procesos que soliciten entrada de datos ya que este metodo solo captura las salidas, si se usa en un metodo que solicita entrada se producira un bloqueo porque siempre se estara esperando por una entrada.
		/// </remarks>
		/// <param name="bash">Comando a ejecutar.</param>
		/// <returns>Se devuelve todo lo capturado de la salida estandar del programa ejecutado.</returns>
		/// <exception cref="NotSupportedException">Se produce cuando no se conoce el sistema operativo.</exception>
		public static string GetCommand(string bash)
		{
			string dev;
			using (var process = CreateProcess(bash, true))
			{
				process.Start();
				process.WaitForExit();
				dev = process.StandardOutput.ReadToEnd();
			}
			return dev;
		}

		/// <summary>
		/// Ejecuta un proceso en el terminal y devuelve la salida de forma asincronica de ese proceso.
		/// </summary>
		/// <remarks>
		/// NO se debe utilizar este metodo para ejecutar procesos que soliciten entrada de datos ya que este metodo solo captura las salidas, si se usa en un metodo que solicita entrada se producira un bloqueo porque siempre se estara esperando por una entrada.
		/// </remarks>
		/// <param name="bash">Comando a ejecutar.</param>
		/// <returns>Se devuelve todo lo capturado de la salida estandar del programa ejecutado.</returns>
		/// <exception cref="NotSupportedException">Se produce cuando no se conoce el sistema operativo.</exception>
		public static async Task<string> GetCommandAsync(string bash) =>
			await Task.Run(async () =>
			{
				string dev = null;
				using (var process = CreateProcess(bash, true))
				{
					process.Start();
					process.WaitForExit();
					dev = await process.StandardOutput.ReadToEndAsync();
				}
				return dev;
			});

		/// <summary>
		/// Ejecuta un comando en consola y no intercepta ni su salida ni su entrada por lo que es interactivo y el usuario puede verlo en consola.
		/// </summary>
		/// <remarks>
		/// Este metodo solo debe ser usado en aplicaciones de consola, de lo contrario nunca se terminara de ejecutar sin un proceso solicita entrada del usuario.
		/// </remarks>
		/// <param name="bash">Comando a ejecutar.</param>
		/// <exception cref="NotSupportedException">Se produce cuando no se conoce el sistema operativo.</exception>
		public static void RunCommand(string bash)
		{
			using (var process = CreateProcess(bash, false))
			{
				process.Start();
				process.WaitForExit();
			}
		}

		/// <summary>
		/// Crea un proceso de ejecucion
		/// </summary>
		private static Process CreateProcess(string bash, bool redirect)
		{
			if (Info.CurrentSystem == OS.Unknow)
				throw new NotSupportedException("El sistema operativo actual no es conocido por lo tanto no se pueden ejecutar ordenes por medio de la clase Bash.");
			string escapedArgs = bash.Replace("\"", "\\\"");
			string[] path = Info.GetTerminalPath(Info.CurrentSystem).Split(' ');

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