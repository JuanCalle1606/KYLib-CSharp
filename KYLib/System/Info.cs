
using System;

namespace KYLib.System
{

	/// <summary>
	/// Provee informacion del sistema operativo.
	/// </summary>
	public static class Info
	{

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
		static Info() => DetectSystem();

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