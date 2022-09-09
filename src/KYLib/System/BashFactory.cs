using System;
using System.Diagnostics;
using KYLib.Utils;
namespace KYLib.System;

//Aqui se guardan los metodos que crean procesos
partial class Bash
{
	/// <summary>
	/// Crea un proceso de terminal que ejecutara un comando dado.
	/// </summary>
	/// <param name="bash">Instrucciones que se le pasaran al terminal, con argumentos incluidos.</param>
	/// <returns>Devuelve el proceso generado.</returns>
	/// <exception cref="PlatformNotSupportedException">Se produce cuando no se conoce el sistema operativo.</exception>
	public static Process CreateBashProcess(string bash) =>
		CreateBashProcess(bash, Environment.CurrentDirectory);

	/// <summary>
	/// Crea un proceso de terminal que ejecutara un comando dado.
	/// </summary>
	/// <param name="bash">Instrucciones que se le pasaran al terminal, con argumentos incluidos.</param>
	/// <param name="runin">Directorio en el cual se ejecutara el proceso.</param>
	/// <returns>Devuelve el proceso generado.</returns>
	/// <exception cref="PlatformNotSupportedException">Se produce cuando no se conoce el sistema operativo.</exception>
	public static Process CreateBashProcess(string bash, string? runin) =>
		CreateBashProcess(bash, runin, false);

	/// <summary>
	/// Crea un proceso de terminal que ejecutara un comando dado.
	/// </summary>
	/// <param name="bash">Instrucciones que se le pasaran al terminal, con argumentos incluidos.</param>
	/// <param name="runin">Directorio en el cual se ejecutara el proceso.</param>
	/// <param name="redirect">Indica si deben redirigir las entradas y salida estandar del proceso.</param>
	/// <returns>Devuelve el proceso generado.</returns>
	/// <exception cref="PlatformNotSupportedException">Se produce cuando no se conoce el sistema operativo.</exception>
	public static Process CreateBashProcess(string bash, string? runin, bool redirect) =>
		CreateBashProcess(bash, runin, redirect, redirect);

	/// <summary>
	/// Crea un proceso de terminal que ejecutara un comando dado.
	/// </summary>
	/// <param name="bash">Instrucciones que se le pasaran al terminal, con argumentos incluidos.</param>
	/// <param name="runin">Directorio en el cual se ejecutara el proceso.</param>
	/// <param name="output">Indica si debe redirigir la salida estandar del proceso.</param>
	/// <param name="errorandinput">Indica si debe redirigir la salida de error y la entrada estandar del proceso.</param>
	/// <returns>Devuelve el proceso generado.</returns>
	/// <exception cref="PlatformNotSupportedException">Se produce cuando no se conoce el sistema operativo.</exception>
	public static Process CreateBashProcess(string bash, string? runin, bool output, bool errorandinput) =>
		CreateBashProcess(bash, runin, output, errorandinput, errorandinput);

	/// <summary>
	/// Crea un proceso de terminal que ejecutara un comando dado.
	/// </summary>
	/// <param name="bash">Instrucciones que se le pasaran al terminal, con argumentos incluidos.</param>
	/// <param name="runin">Directorio en el cual se ejecutara el proceso.</param>
	/// <param name="output">Indica si debe redirigir la salida estandar del proceso.</param>
	/// <param name="error">Indica si debe redirigir la salida de error estandar del proceso.</param>
	/// <param name="input">Indica si debe redirigir la entrada estandar del proceso.</param>
	/// <returns>Devuelve el proceso generado.</returns>
	/// <exception cref="PlatformNotSupportedException">Se produce cuando no se conoce el sistema operativo.</exception>
	public static Process CreateBashProcess(string bash, string? runin, bool output, bool error, bool input)
	{
		if (Info.CurrentSystem == Os.Unknow)
			throw new PlatformNotSupportedException("El sistema operativo actual no es conocido por lo tanto no se pueden ejecutar ordenes por medio de bash, intente ejecutar el archivo directamente.");
		var path = Info.TerminalPath.Split(' ');
		var file = path[0];
		var args = $"{path[1]} \"{bash}\"";
		return CreateProcess(file, args, runin, output, error, input);
	}

	/// <summary>
	/// Crea un nuevo proceso con la información dada.
	/// </summary>
	/// <param name="file">Indica el archivo que va a ser ejecutado.</param>
	/// <returns>Devuelve el proceso generado.</returns>
	public static Process CreateProcess(string file) =>
		CreateProcess(file, string.Empty);

	/// <summary>
	/// Crea un nuevo proceso con la información dada.
	/// </summary>
	/// <param name="file">Indica el archivo que va a ser ejecutado.</param>
	/// <param name="args">Argumentos que se le pasaran al programa.</param>
	/// <returns>Devuelve el proceso generado.</returns>
	public static Process CreateProcess(string file, string args) =>
		CreateProcess(file, args, Environment.CurrentDirectory);

	/// <summary>
	/// Crea un nuevo proceso con la información dada.
	/// </summary>
	/// <param name="file">Indica el archivo que va a ser ejecutado.</param>
	/// <param name="args">Argumentos que se le pasaran al programa.</param>
	/// <param name="runin">Directorio en el cual se ejecutara el proceso.</param>
	/// <returns>Devuelve el proceso generado.</returns>
	public static Process CreateProcess(string file, string args, string? runin) =>
		CreateProcess(file, args, runin, false);

	/// <summary>
	/// Crea un nuevo proceso con la información dada.
	/// </summary>
	/// <param name="file">Indica el archivo que va a ser ejecutado.</param>
	/// <param name="args">Argumentos que se le pasaran al programa.</param>
	/// <param name="runin">Directorio en el cual se ejecutara el proceso.</param>
	/// <param name="redirect">Indica si deben redirigir las entradas y salida estandar del proceso.</param>
	/// <returns>Devuelve el proceso generado.</returns>
	public static Process CreateProcess(string file, string args, string? runin, bool redirect) =>
		CreateProcess(file, args, runin, redirect, redirect);

	/// <summary>
	/// Crea un nuevo proceso con la información dada.
	/// </summary>
	/// <param name="file">Indica el archivo que va a ser ejecutado.</param>
	/// <param name="args">Argumentos que se le pasaran al programa.</param>
	/// <param name="runin">Directorio en el cual se ejecutara el proceso.</param>
	/// <param name="output">Indica si debe redirigir la salida estandar del proceso.</param>
	/// <param name="errorandinput">Indica si debe redirigir la salida de error y la entrada estandar del proceso.</param>
	/// <returns>Devuelve el proceso generado.</returns>
	public static Process CreateProcess(string file, string args, string? runin, bool output, bool errorandinput) =>
		CreateProcess(file, args, runin, output, errorandinput, errorandinput);

	/// <summary>
	/// Crea un nuevo proceso con la información dada.
	/// </summary>
	/// <param name="file">Indica el archivo que va a ser ejecutado.</param>
	/// <param name="args">Argumentos que se le pasaran al programa.</param>
	/// <param name="runin">Directorio en el cual se ejecutara el proceso.</param>
	/// <param name="output">Indica si debe redirigir la salida estandar del proceso.</param>
	/// <param name="error">Indica si debe redirigir la salida de error estandar del proceso.</param>
	/// <param name="input">Indica si debe redirigir la entrada estandar del proceso.</param>
	/// <returns>Devuelve el proceso generado.</returns>
	public static Process CreateProcess(string file, string args, string? runin, bool output, bool error, bool input)
	{
		Ensure.NotNull(file, nameof(file));
		Ensure.NotNull(args, nameof(args));
		
		return CreateProcess(new ProcessStartInfo
		{
			FileName = file,
			Arguments = args,
			RedirectStandardOutput = output,
			RedirectStandardError = error,
			RedirectStandardInput = input,
			UseShellExecute = false,
			CreateNoWindow = false,
			WorkingDirectory = string.IsNullOrWhiteSpace(runin) ? Environment.CurrentDirectory : runin
		});
	}

	/// <summary>
	/// Crea un nuevo proceso con una información dada.
	/// </summary>
	/// <param name="info">Información de proceso inicial.</param>
	/// <returns>El proceso generado.</returns>
	public static Process CreateProcess(ProcessStartInfo info)
	{
		Ensure.NotNull(info, nameof(info));
		return new()
		{
			EnableRaisingEvents = true,
			StartInfo = info
		};
	}
}