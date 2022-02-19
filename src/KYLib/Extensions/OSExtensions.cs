using KYLib.System;
namespace KYLib.Extensions;

/// <summary>
/// Extensiones para las enumeraciones OS.
/// </summary>
public static class OsExtensions
{
	/// <summary>
	/// Indica si este sistema operativo es windows.
	/// </summary>
	public static bool IsWindows(this Os os) => Info.IsOsBased(os, Os.Windows);

	/// <summary>
	/// Indica si este sistema operativo esta basado en Unix.
	/// </summary>
	public static bool IsUnix(this Os os) => Info.IsOsBased(os, Os.Unix);

	/// <summary>
	/// Indica si este sistema operativo esta basado en Linux.
	/// </summary>
	public static bool IsLinux(this Os os) => Info.IsOsBased(os, Os.Linux);

	/// <summary>
	/// Indica si este sistema operativo esta basado en <paramref name="based"/>.
	/// </summary>
	public static bool Is(this Os os, Os based) => Info.IsOsBased(os, based);
}