using System;
using System.Threading.Tasks;

namespace KYLib.System;

//Aqui se guardan los metodos que son de ayuda para la ejecución de procesos.
partial class Bash
{
	/// <summary>
	/// Ejecuta un proceso en el terminal y devuelve la salida de ese proceso.
	/// </summary>
	/// <remarks>
	/// NO se debe utilizar este metodo para ejecutar procesos que soliciten entrada de datos ya que este metodo solo captura las salidas, si se usa en un metodo que solicita entrada se producira un bloqueo porque siempre se estara esperando por una entrada.
	/// </remarks>
	/// <param name="bash">Comando a ejecutar.</param>
	/// <returns>Se devuelve lo capturado de la salida estandar del programa ejecutado.</returns>
	/// <exception cref="PlatformNotSupportedException">Se produce cuando no se conoce el sistema operativo.</exception>
	public static string GetCommand(string bash)
	{
		using var process = CreateBashProcess(bash, null, true, false);
		process.Start();
		process.WaitForExit();
		return process.StandardOutput.ReadToEnd();
	}

	/// <summary>
	/// Ejecuta un proceso en el terminal y devuelve la salida de forma asincronica de ese proceso.
	/// </summary>
	/// <remarks>
	/// NO se debe utilizar este metodo para ejecutar procesos que soliciten entrada de datos ya que este metodo solo captura las salidas, si se usa en un metodo que solicita entrada se producira un bloqueo porque siempre se estara esperando por una entrada.
	/// </remarks>
	/// <param name="bash">Comando a ejecutar.</param>
	/// <returns>Se devuelve lo capturado de la salida estandar del programa ejecutado.</returns>
	/// <exception cref="PlatformNotSupportedException">Se produce cuando no se conoce el sistema operativo.</exception>
	public static async Task<string> GetCommandAsync(string bash) =>
		await Task.Run(async () =>
		{
			using var process = CreateBashProcess(bash, null, true, false);
			process.Start();
			process.WaitForExit();
			var dev = await process.StandardOutput.ReadToEndAsync();
			return dev;
		});

	/// <summary>
	/// Ejecuta un proceso en el terminal y espera por sus salidas.
	/// </summary>
	/// <remarks>
	/// NO se debe utilizar este metodo para ejecutar procesos que soliciten entrada de datos ya que este metodo solo captura las salidas, si se usa en un metodo que solicita entrada se producira un bloqueo porque siempre se estara esperando por una entrada.
	/// </remarks>
	/// <param name="bash">Comando a ejecutar.</param>
	/// <param name="callback">Acción que recibe las salidas del proceso cada vez que son recibidas.</param>
	/// <exception cref="PlatformNotSupportedException">Se produce cuando no se conoce el sistema operativo.</exception>
	public static async Task CommandAsync(string bash, Action<string> callback) =>
		await Task.Run(() =>
		{
			using var process = CreateBashProcess(bash, null, true, false);
			process.OutputDataReceived += (o, e) => callback?.Invoke(e.Data);
			process.Start();
			process.BeginOutputReadLine();
			process.WaitForExit();
		});

	/// <summary>
	/// Ejecuta un comando en consola y no intercepta ni su salida ni su entrada por lo que es interactivo y el usuario puede verlo en consola.
	/// </summary>
	/// <remarks>
	/// Este metodo solo debe ser usado en aplicaciones de consola, de lo contrario nunca se terminara de ejecutar si un proceso solicita entrada del usuario.
	/// </remarks>
	/// <param name="file">Programa a ejecutar.</param>
	/// <returns>Devuelve el codigo de salida del programa luego de que termina de ejecutarse. En caso de que el proceso no s epueda iniciar se devolvera -1.</returns>
	public static kint RunCommand(string file) =>
		RunCommand(file, string.Empty);

	/// <summary>
	/// Ejecuta un comando en consola y no intercepta ni su salida ni su entrada por lo que es interactivo y el usuario puede verlo en consola.
	/// </summary>
	/// <remarks>
	/// Este metodo solo debe ser usado en aplicaciones de consola, de lo contrario nunca se terminara de ejecutar si un proceso solicita entrada del usuario.
	/// </remarks>
	/// <param name="file">Programa a ejecutar.</param>
	/// <param name="args">Argumentos opcionales para pasar al programa.</param>
	/// <returns>Devuelve el codigo de salida del programa luego de que termina de ejecutarse. En caso de que el proceso no s epueda iniciar se devolvera -1.</returns>
	public static kint RunCommand(string file, string args) =>
		RunCommand(file, args, null);

	/// <summary>
	/// Ejecuta un comando en consola y no intercepta ni su salida ni su entrada por lo que es interactivo y el usuario puede verlo en consola.
	/// </summary>
	/// <remarks>
	/// Este metodo solo debe ser usado en aplicaciones de consola, de lo contrario nunca se terminara de ejecutar si un proceso solicita entrada del usuario.
	/// </remarks>
	/// <param name="file">Programa a ejecutar.</param>
	/// <param name="args">Argumentos opcionales para pasar al programa.</param>
	/// <param name="runin">Directorio en el cual se ejecutara el proceso</param>
	/// <returns>Devuelve el codigo de salida del programa luego de que termina de ejecutarse. En caso de que el proceso no se pueda iniciar se devolvera -1.</returns>
	public static kint RunCommand(string file, string args, string? runin)
	{
		kint dev;
		using var process = CreateProcess(file, args, runin);
		try
		{
			process.Start();
			process.WaitForExit();
		}
		catch (Exception) { /* ignored */ }
		finally
		{
			dev = process.ExitCode;
		}
		return dev;
	}
}