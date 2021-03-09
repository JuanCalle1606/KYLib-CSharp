namespace KYLib.System
{
	/// <summary>
	/// Enumeración de sistemas operativos soportados por la libreria.
	/// </summary>
	public enum OS
	{
		/// <summary>
		/// Indica que el programa no ha reconocido el sistema operativo actual.
		/// </summary>
		Unknow = 0,
		/// <summary>
		/// Sistema operativo Windows.
		/// </summary>
		Windows = 0x100000,
		/// <summary>
		/// Sistema operativo Unix, todas las distribuciones no soportadas seran detectadas como unix.
		/// </summary>
		Unix = 1,
		/// <summary>
		/// Sistema operativo GNU/Linux.
		/// </summary> 
		Linux = 3,
		/// <summary>
		/// Sistema operativo Debian.
		/// </summary> 
		Debian = 7,
		/*
		/// <summary>
		/// Sistema operativo Ubuntu.
		/// </summary> 
		Ubuntu = 23,*/
		/// <summary>
		/// Sistema operativo Parrot OS.
		/// </summary>
		Parrot = 39,
	}
}