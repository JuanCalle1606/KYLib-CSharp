﻿using System.IO;
using KYLib.System;

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
	public bool ResolveAbsolute { get; set; }
	
	/// <summary>
	/// Indica si este objeto se peude modificar con los llamados a 
	/// </summary>
	public bool ReadOnly { get; }

	/// <summary>
	/// Crea una nueva instancia basada en un directorio.
	/// </summary>
	/// <param name="directory">Directorio relacionado a esta instancia.</param>
	/// <param name="readOnly">Indica si este objeto assets se podra modificar.</param>
	public Assets(string directory, bool readOnly = false)
	{
		SearchPath = Path.GetFullPath(directory).TrimEnd('\\', '/');
		ReadOnly = readOnly;
	}

	/// <summary>
	/// Actualiza <see cref="SearchPath"/> con una ruta relativa al directorio actual.
	/// </summary>
	/// <param name="path">Ruta relativa nueva</param>
	public void UpdateRelPath(string path)
	{
		if(ReadOnly) return;
		SearchPath = Path.GetFullPath(Path.GetRelativePath(SearchPath, path), SearchPath);
	}

	/// <summary>
	/// Obtiene una instancia de <see cref="Assets"/> de una ruta relativa a <see cref="SearchPath"/>.
	/// </summary>
	/// <param name="path">Nombre de la ruta relativa.</param>
	public Assets GetAssets(string path) =>
		new(this[path]);

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

	/// <summary>
	/// Obtiene un recurso relativo a esta instancia de <see cref="Assets"/>.
	/// </summary>
	public string this[string filename] =>
		ResolveAbsolute ? GetPath(filename) : GetRelPath(filename);
}