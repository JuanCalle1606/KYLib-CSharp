using System.IO;
using KYLib.System;

namespace KYLib.Utils
{
	/// <summary>
	/// Provee funciones para obtener recursos.
	/// </summary>
	public static class Assets
	{
		/// <summary>
		/// Directorio de busqueda de recursos.
		/// </summary>
		public static string SearchPath = Path.Combine(Info.InstallDir, "Assets");

		/// <summary>
		/// Actualiza <see cref="SearchPath"/> con una ruta relativa al directorio de instalación.
		/// </summary>
		/// <param name="path">Ruta relativa nueva</param>
		public static void UpdateRelPath(string path) =>
			SearchPath = Path.Combine(Info.InstallDir, path);

		/// <summary>
		/// Obtiene la ruta de <paramref name="filename"/> buscando en <see cref="SearchPath"/>.
		/// </summary>
		/// <param name="filename">Nombre del archivo a buscar.</param>
		public static string GetPath(string filename) =>
			Path.Combine(SearchPath, filename);
	}
}