using KYLib.System;
namespace KYLib.Extensions
{
	/// <summary>
	/// Extensiones para las enumeraciones OS.
	/// </summary>
	public static class OSExtensions
	{
		/// <summary>
		/// Indica si este sistema operativo es windows.
		/// </summary>
		public static bool IsWindows(this OS os) => Info.IsOSBased(os, OS.Windows);

		/// <summary>
		/// Indica si este sistema operativo esta basado en Unix.
		/// </summary>
		public static bool IsUnix(this OS os) => Info.IsOSBased(os, OS.Unix);

		/// <summary>
		/// Indica si este sistema operativo esta basado en Linux.
		/// </summary>
		public static bool IsLinux(this OS os) => Info.IsOSBased(os, OS.Linux);

		/// <summary>
		/// Indica si este sistema operativo esta basado en <paramref name="based"/>.
		/// </summary>
		public static bool Is(this OS os, OS based) => Info.IsOSBased(os, based);
	}
}