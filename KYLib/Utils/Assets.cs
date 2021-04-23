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
		/// Privatizamos el constructor sin parametros, esto no estara permitido.
		/// </summary>
		private Assets() { }

		/// <summary>
		/// Crea una nueva instancia basada en un directorio.
		/// </summary>
		/// <param name="directory">Directorio relacionado a esta instancia.</param>
		public Assets(string directory) => Directory = directory;

		/// <summary>
		/// Actualiza <see cref="Directory"/> con una ruta relativa al directorio de instalación.
		/// </summary>
		/// <param name="path">Ruta relativa nueva</param>
		public void UpdateRelPath(string path) =>
			Directory = Path.Combine(Directory, path);

		/// <summary>
		/// Obtiene la ruta de <paramref name="filename"/> buscando en <see cref="Directory"/>.
		/// </summary>
		/// <param name="filename">Nombre del archivo a buscar.</param>
		public string GetPath(string filename) =>
			Path.Combine(Directory, filename);

		/// <summary>
		/// Obtiene un recurso relativo a esta instancia de <see cref="Assets"/>.
		/// </summary>
		public string this[string filename] => GetPath(filename);
	}
}