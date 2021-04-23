using System.IO;
using KYLib.System;

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
		public string Directory { get; private set; }

		/// <summary>
		/// Indica si las rutas devueltas por el indexador son absolutas.
		/// </summary>
		public bool ResolveAbsolute = false;

		/// <summary>
		/// Privatizamos el constructor sin parametros, esto no estara permitido.
		/// </summary>
		private Assets() { }

		/// <summary>
		/// Crea una nueva instancia basada en un directorio.
		/// </summary>
		/// <param name="directory">Directorio relacionado a esta instancia.</param>
		public Assets(string directory) => Directory = Path.GetFullPath(directory);

		/// <summary>
		/// Actualiza <see cref="Directory"/> con una ruta relativa al directorio de instalación.
		/// </summary>
		/// <param name="path">Ruta relativa nueva</param>
		public void UpdateRelPath(string path) =>
			Directory = Path.GetFullPath(Path.GetRelativePath(Directory, path), Directory);

		/// <summary>
		/// Obtiene una instancia de <see cref="Assets"/> de una ruta relativa a <see cref="Directory"/>.
		/// </summary>
		/// <param name="path">Nombre de la ruta relativa.</param>
		public Assets GetAssets(string path) =>
			new Assets(this[path]);

		/// <summary>
		/// Obtiene la ruta de <paramref name="filename"/> absoluta a <see cref="Directory"/>.
		/// </summary>
		/// <param name="filename">Nombre del archivo a buscar.</param>
		public string GetPath(string filename) =>
			Path.GetFullPath(filename, Directory);

		/// <summary>
		/// Obtiene la ruta de <paramref name="filename"/> absoluta a <see cref="Directory"/> y relativa a <see cref="Info.CurrentDir"/>.
		/// </summary>
		/// <param name="filename">Nombre del archivo a buscar.</param>
		public string GetRelPath(string filename) =>
			Path.GetRelativePath(Info.CurrentDir, GetPath(filename));

		/// <summary>
		/// Obtiene un recurso relativo a esta instancia de <see cref="Assets"/>.
		/// </summary>
		public string this[string filename] => ResolveAbsolute ?
			GetPath(filename) : GetRelPath(filename);
	}
}