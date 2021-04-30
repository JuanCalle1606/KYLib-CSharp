using System.IO;
#if NS21
using KYLib.System;
#endif

namespace KYLib.Utils
{
	/// <summary>
	/// Provee funciones para obtener recursos.
	/// </summary>
	public partial class Assets
	{
		/// <summary>
		/// Directorio de busqueda de recursos.
		/// </summary>
		public string SearchPath { get; private set; }
#if NS21
		/// <summary>
		/// Indica si las rutas devueltas por el indexador son absolutas.
		/// </summary>
		public bool ResolveAbsolute = false;
#endif
		/// <summary>
		/// Privatizamos el constructor sin parametros, esto no estara permitido.
		/// </summary>
		private Assets() { }

		/// <summary>
		/// Crea una nueva instancia basada en un directorio.
		/// </summary>
		/// <param name="directory">Directorio relacionado a esta instancia.</param>
		public Assets(string directory) => SearchPath = Path.GetFullPath(directory);
#if NS21
		/// <summary>
		/// Actualiza <see cref="SearchPath"/> con una ruta relativa al directorio de instalación.
		/// </summary>
		/// <param name="path">Ruta relativa nueva</param>
		public void UpdateRelPath(string path) =>
			SearchPath = Path.GetFullPath(Path.GetRelativePath(SearchPath, path), SearchPath);
#endif
		/// <summary>
		/// Obtiene una instancia de <see cref="Assets"/> de una ruta relativa a <see cref="SearchPath"/>.
		/// </summary>
		/// <param name="path">Nombre de la ruta relativa.</param>
		public Assets GetAssets(string path) =>
			new(this[path]);
#if NS21
		/// <summary>
		/// Obtiene la ruta de <paramref name="filename"/> absoluta a <see cref="SearchPath"/>.
		/// </summary>
		/// <param name="filename">Nombre del archivo a buscar.</param>
		public string GetPath(string filename) =>
			Path.GetFullPath(filename, SearchPath);

		/// <summary>
		/// Obtiene la ruta de <paramref name="filename"/> absoluta a <see cref="SearchPath"/> y relativa a <see cref="Info.CurrentDir"/>.
		/// </summary>
		/// <param name="filename">Nombre del archivo a buscar.</param>
		public string GetRelPath(string filename) =>
			Path.GetRelativePath(Info.CurrentDir, GetPath(filename));
#endif
		/// <summary>
		/// Obtiene un recurso relativo a esta instancia de <see cref="Assets"/>.
		/// </summary>
		public string this[string filename] =>
#if NS21
			ResolveAbsolute ? GetPath(filename) : GetRelPath(filename);
#else
			Path.Combine(SearchPath, filename);
#endif
	}
}