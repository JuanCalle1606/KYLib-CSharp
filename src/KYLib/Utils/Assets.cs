using System.IO;
#if NETSTANDARD2_1
using KYLib.System;
#else
using System;
#endif

namespace KYLib.Utils;

/// <summary>
/// Provee funciones para obtener recursos.
/// </summary>
public partial class Assets {
	/// <summary>
	/// Directorio de busqueda de recursos.
	/// </summary>
	public string SearchPath { get; private set; }
	
	/// <summary>
	/// Indica si las rutas devueltas por el indexador son absolutas.
	/// </summary>
#if NETSTANDARD2_0
	[Obsolete("Esta propiedad no tiene ningun uso en esta version")]
#endif
	public bool ResolveAbsolute { get; set; } = false;

	/// <summary>
	/// Crea una nueva instancia basada en un directorio.
	/// </summary>
	/// <param name="directory">Directorio relacionado a esta instancia.</param>
	public Assets(string directory) => SearchPath = Path.GetFullPath(directory).TrimEnd('\\', '/');

#if NETSTANDARD2_1
	/// <summary>
	/// Actualiza <see cref="SearchPath"/> con una ruta relativa al directorio actual.
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
#if NETSTANDARD2_1
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
#if NETSTANDARD2_1
		ResolveAbsolute ? GetPath(filename) : GetRelPath(filename);
#else
		Path.Combine(SearchPath, filename);
#endif
}