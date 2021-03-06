namespace KYLib.System
{
	/// <summary>
	/// Lista de enumeración de sistemas operativos soportados por la libreria.
	/// </summary>
	public enum OS
	{
		/// <summary>
		/// Indica que el programa no ha reconocido el sistema operativo actual.
		/// </summary>
		Unknow,
		/// <summary>
		/// Sistema operativo Unix, todas las distribuciones no soportadas seran detectadas como unix.
		/// </summary>
		Unix,
		/// <summary>
		/// Sistema operativo Windows.
		/// </summary>
		Windows,
		/// <summary>
		/// Sistema operativo GNU/Linux.
		/// </summary> 
		Linux,
		/// <summary>
		/// Sistema operativo Parrot OS.
		/// </summary>
		Parrot = 12,
	}
}