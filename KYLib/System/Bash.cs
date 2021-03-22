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
		/// <exception cref="PlatformNotSupportedException">Se produce cuando no se conoce el sistema operativo.</exception>
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
		/// <exception cref="PlatformNotSupportedException">Se produce cuando no se conoce el sistema operativo.</exception>
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
		/// Inicia un proceso de un programa y lo controla.
		/// </summary>
		/// <remarks>
		/// A diferencia de los otros metodos de la clase <see cref="Bash"/> este metodo no ejecuta un comando por medio del terminal sino que directamente ejecuta el programa por su archivo ejecutable esto permite manejar el error de archivo no encontrado directamente una vez llamado a este metodo, igual a diferencia de todos los otros metodo este si devuelve el proceso que crea por lo que permite un mayor control sobre lo que ocurre con el proceso.
		/// </remarks>
		/// <param name="file">Nombre del archivo de programa a ejecutar.</param>
		/// <param name="args">Argumentos opcionales para pasar al programa.</param>
		/// <param name="stdout">Opcionalemente se puede pasar una acción que se llame cada vez que se reciba una salida estandar, en caso de no pasarse una acción la salida estandar puede seguir siendo administrada por medio del objeto de proceso devuelto.</param>
		/// <param name="stderr">Opcionalemente se puede pasar una acción que se llame cada vez que se reciba una salida del error estandar, en caso de no pasarse una acción el error estandar puede seguir siendo administrado por medio del objeto de proceso devuelto.</param>
		/// <param name="stdin">Este metodo crea una acción que devuelve por medio de este parametro, llame a esta acción cada ves que quiera escribir algo en la entrada estandar del programa, en caso de ignorar esta acción aun puede administrar la entrada estandar por dmdio del objeto de proceso devuelto.</param>
		/// <returns>Devuelve un objeto de proceso que representa al programa invocado corriendo.</returns>
		public static Process Start(string file, string args, Action<string> stdout, Action<string> stderr, out Action<string> stdin)
		{
			var process = CreateProcess(file, args, true, true);
			process.OutputDataReceived += (o, e) => stdout?.Invoke(e.Data);
			process.ErrorDataReceived += (o, e) => stderr?.Invoke(e.Data);
			process.Start();
			process.BeginOutputReadLine();
			process.BeginErrorReadLine();
			stdin = s =>
			{
				process.StandardInput.WriteLine(s);
				process.StandardInput.Flush();
			};
			return process;
		}

		/// <summary>
		/// Ejecuta un proceso en el terminal y espera por sus salidas.
		/// </summary>
		/// <remarks>
		/// NO se debe utilizar este metodo para ejecutar procesos que soliciten entrada de datos ya que este metodo solo captura las salidas, si se usa en un metodo que solicita entrada se producira un bloqueo porque siempre se estara esperando por una entrada.
		/// </remarks>
		/// <param name="bash">Comando a ejecutar.</param>
		/// <param name="callback">Acción que recibe las salidas del proceso cada vez que son recibidas.</param>
		public static async Task CommandAsync(string bash, Action<string> callback) =>
		await Task.Run(() =>
		{
			using (var process = CreateProcess(bash, true))
			{
				process.OutputDataReceived += (o, e) => callback?.Invoke(e.Data);
				process.Start();
				process.BeginOutputReadLine();
				process.WaitForExit();
			}
		});

		/// <summary>
		/// Ejecuta un comando en consola y no intercepta ni su salida ni su entrada por lo que es interactivo y el usuario puede verlo en consola.
		/// </summary>
		/// <remarks>
		/// Este metodo solo debe ser usado en aplicaciones de consola, de lo contrario nunca se terminara de ejecutar si un proceso solicita entrada del usuario.
		/// </remarks>
		/// <param name="bash">Comando a ejecutar.</param>
		/// <exception cref="PlatformNotSupportedException">Se produce cuando no se conoce el sistema operativo.</exception>
		public static void RunCommand(string bash)
		{
			using (var process = CreateProcess(bash, false))
			{
				process.Start();
				process.WaitForExit();
			}
		}

		/// <summary>
		/// Crea un proceso de ejecucion por medio del bash nativo
		/// </summary>
		private static Process CreateProcess(string bash, bool redirect)
		{
			var path = Info.TerminalPath.Split(' ');
			string file = path[0];
			string args = $"{path[1]} \"{bash.Replace("\"", "\\\"")}\"";
			return CreateProcess(file, args, redirect, false);
		}

		/// <summary>
		/// Crea un proceso de ejecucion con un archivo especifico.
		/// </summary>
		private static Process CreateProcess(string file, string args, bool output, bool errorandinput)
		{
			if (Info.CurrentSystem == OS.Unknow)
				throw new PlatformNotSupportedException("El sistema operativo actual no es conocido por lo tanto no se pueden ejecutar ordenes por medio de la clase Bash.");

			return new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = file,
					Arguments = args,
					RedirectStandardOutput = output,
					RedirectStandardError = errorandinput,
					RedirectStandardInput = errorandinput,
					UseShellExecute = false,
					CreateNoWindow = true
				},
				EnableRaisingEvents = true
			};
		}
	}
}