using System.IO;
using KYLib.System;

namespace KYLib.Utils
{
	/// <summary>
	/// Provee funciones para obtener recursos.
	/// </summary>
	partial class Assets
	{
		/// <summary>
		/// Obtiene una instnacia de <see cref="Assets"/> relacionada a la ruta de <see cref="Info.BaseDir"/>.
		/// </summary>
		public static readonly Assets BaseDir = new Assets(Info.BaseDir);

		/// <summary>
		/// Obtiene una instnacia de <see cref="Assets"/> relacionada a la ruta de <see cref="Info.CurrentDir"/>.
		/// </summary>
		public static readonly Assets CurrentDir = new Assets(Info.CurrentDir);

		/// <summary>
		/// Obtiene una instnacia de <see cref="Assets"/> relacionada a la ruta de <see cref="Info.InstallDir"/>.
		/// </summary>
		public static readonly Assets InstallDir = new Assets(Info.InstallDir);

		/// <summary>
		/// Ubicación desde la que se cargan mods con el metodo <see cref="Mod.Import(string)"/>.
		/// </summary>
		public static readonly Assets ModsDir = new Assets(Path.Combine(Info.BaseDir, "mods"));
	}
}