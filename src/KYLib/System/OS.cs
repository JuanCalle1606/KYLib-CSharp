namespace KYLib.System;

/// <summary>
/// Enumeración de sistemas operativos soportados por la libreria.
/// </summary>
[Flags]
public enum Os
{
	/// <summary>
	/// Indica que el programa no ha reconocido el sistema operativo actual.
	/// </summary>
	Unknow = 0,
	/// <summary>
	/// Sistema operativo Windows.
	/// </summary>
	Windows = 0b1,
	/// <summary>
	/// Sistema operativo Unix, todas las distribuciones no soportadas seran detectadas como unix.
	/// </summary>
	Unix = 0b10,
	/// <summary>
	/// Sistema operativo GNU/Linux.
	/// </summary> 
	Linux = 0b110,
	/// <summary>
	/// Systema operativo OSX.
	/// </summary>
	Osx = 0b1010,
	/// <summary>
	/// Sistema operativo Debian.
	/// </summary> 
	Debian = 0b10110,
	/// <summary>
	/// Sistema operativo Ubuntu.
	/// </summary> 
	Ubuntu = 0b100010110,
	/// <summary>
	/// Sistema operativo Parrot OS.
	/// </summary>
	Parrot = 0b1000010110,
}