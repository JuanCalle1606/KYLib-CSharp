namespace KYLib.System
{

	/// <summary>
	/// Provee información del sistema operativo y demas cosas.
	/// </summary>
	partial class Info
	{
		/// <summary>
		/// Solo para sistemas linux, determina si la aplicación tiene o no permisos root. En sistemas distintos a linux vale <c>null</c>.
		/// </summary>
		public static readonly bool? IsRoot;

		/// <summary>
		/// Solo para sistemas windows, determina si la aplicación tiene o no permisos de administrador. En sistemas distintos a windows vale <c>null</c>.
		/// </summary>
		public static readonly bool? IsAdmin;

		/// <summary>
		/// Determina si la aplicación tiene privilegios.
		/// </summary>
		public static readonly bool IsPrivileged = false;
	}
}