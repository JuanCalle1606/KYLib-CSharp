using System;
using System.IO;
using System.Reflection;
using KYLib.Extensions;

namespace KYLib.System
{

	/// <summary>
	/// Provee información del sistema operativo y demas cosas.
	/// </summary>
	public static partial class Info
	{

		/// <summary>
		/// Obtiene la dirección del punto de entrada.
		/// </summary>
		public static readonly string EntryDll = Assembly.GetEntryAssembly().Location;

		/// <summary>
		/// Nombre del ensamblado de entrada.
		/// </summary>
		public static readonly string AppName = AppDomain.CurrentDomain.FriendlyName;

		/// <summary>
		/// Obtiene la dirección del directorio en el que se encuentra el ejecutable.
		/// </summary>
		public static readonly string InstallDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

		/// <summary>
		/// Obtiene la ubicación desde la que se cargan ensamblados por defecto.
		/// </summary>
		public static readonly string BaseDir = AppDomain.CurrentDomain.BaseDirectory;

		/// <summary>
		/// Obtiene el directorio de trabajo actual.
		/// </summary>
		public static readonly string CurrentDir = Environment.CurrentDirectory;

		/// <summary>
		/// Obtiene el sistema operativo actual.
		/// </summary>
		public static OS CurrentSystem { get; private set; }


		/// <summary>
		/// Obtiene la ruta al comando que se usa para ejecutar ordenes de consola en el sistema operativo actual.
		/// </summary>
		public static string TerminalPath => GetTerminalPath(CurrentSystem);

		/// <summary>
		/// Constructor estatico.
		/// </summary>
		static Info()
		{
			DetectSystem();
			// set vars
			if (CurrentSystem.IsLinux())
			{
				IsRoot = Linux.getuid() == 0;
				IsPrivileged = IsRoot.Value;
			}
			if (CurrentSystem.IsWindows())
			{
				IsAdmin = Windows.IsUserAnAdmin();
				IsPrivileged = IsAdmin.Value;
			}
		}

		/// <summary>
		/// Usamos esta función internamente para detectar el sistema operativo.
		/// </summary>
		private static void DetectSystem()
		{
			var os = Environment.OSVersion;
			if (os.Platform == PlatformID.Win32NT)
			{ CurrentSystem = OS.Windows; return; }

			else CurrentSystem = OS.Unix;

			string uname = Bash.GetCommand("uname -a").ToLower();
			if (uname.Contains("linux")) CurrentSystem = OS.Linux;
			if (uname.Contains("debian")) CurrentSystem = OS.Debian;
			if (uname.Contains("parrot")) CurrentSystem = OS.Parrot;
		}

		/// <summary>
		/// Indica si <paramref name="distro"/> es un sistema operativo basado en <paramref name="based"/>.
		/// </summary>
		/// <param name="distro">Sistema de origen.</param>
		/// <param name="based">Sistema padre.</param>
		/// <returns><c>true</c> si <paramref name="distro"/> es un sistema que esta basado en <paramref name="based"/> o <c>false</c> si no lo es.</returns>
		public static bool IsOSBased(OS distro, OS based) => (distro & based) == based;

		/// <summary>
		/// Obtiene la ruta al comando que se usa para ejecutar ordenes de consola en el sistema operativo dado.
		/// </summary>
		/// <param name="os">Sistema operativo para saber la ruta.</param>
		/// <returns>La ruta o comando que se sua para ejecutar ordenes de consola.</returns>
		public static string GetTerminalPath(OS os)
		{
			if (IsOSBased(os, OS.Windows)) return "cmd.exe /c";
			if (IsOSBased(os, OS.Unix)) return "/bin/bash -c";
			return string.Empty;
		}
	}
}