using KYLib.Modding;
using KYLib.System;
using System.IO;

namespace KYLib.Utils;

/// <summary>
/// Provee funciones para obtener recursos.
/// </summary>
partial class Assets
{
	/// <summary>
	/// Obtiene una instnacia de <see cref="Assets"/> relacionada a la ruta de <see cref="Info.BaseDir"/>.
	/// </summary>
	public static readonly Assets BaseDir = new(Info.BaseDir);

	/// <summary>
	/// Obtiene una instnacia de <see cref="Assets"/> relacionada a la ruta de <see cref="Info.CurrentDir"/>.
	/// </summary>
	public static readonly Assets CurrentDir = new(Info.CurrentDir);

	/// <summary>
	/// Obtiene una instnacia de <see cref="Assets"/> relacionada a la ruta de <see cref="Info.InstallDir"/>.
	/// </summary>
	public static readonly Assets InstallDir = Info.InstallDir != null ? new(Info.InstallDir) : null;

	/// <summary>
	/// Ubicación desde la que se cargan mods con el metodo <see cref="Mod.LoadMods"/>.
	/// </summary>
	public static readonly Assets ModsDir = new(Path.Combine(Info.BaseDir, "mods"));

	/// <summary>
	/// Convierte el objeto <see cref="Assets"/> en un  string, este valor es igual <see langword="abstract"/><see cref="Assets.SearchPath"/>.
	/// </summary>
	/// <param name="assets">Objecto <see cref="Assets"/> el cual se convierte.</param>
	public static implicit operator string(Assets assets) => assets.SearchPath;

	/// <summary>
	/// Convierte un directorio en un objecto <see cref="Assets"/>.
	/// </summary>
	/// <param name="path">Directorio que estara relacionado con el objecto <see cref="Assets"/>.</param>
	public static implicit operator Assets(string path) => new Assets(path);

	public static bool operator ==(Assets a, Assets b)
	{
		return a.SearchPath == b.SearchPath;
	}

	public static bool operator !=(Assets a, Assets b)
	{
		return a.SearchPath != b.SearchPath;
	}
}