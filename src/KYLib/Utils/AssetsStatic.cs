using System;
using KYLib.Modding;
using KYLib.System;

namespace KYLib.Utils;

/// <summary>
/// Provee funciones para obtener recursos.
/// </summary>
partial class Assets: IEquatable<Assets?>
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
	public static readonly Assets? InstallDir = Info.InstallDir != null ? new Assets(Info.InstallDir) : null;

	/// <summary>
	/// Ubicación desde la que se cargan mods con el metodo <see cref="Mod.LoadMods"/>.
	/// </summary>
	public static readonly Assets ModsDir = BaseDir.GetAssets("mods");

	/// <summary>
	/// Convierte el objeto <see cref="Assets"/> en un  string, este valor es igual <see langword="abstract"/><see cref="Assets.SearchPath"/>.
	/// </summary>
	/// <param name="assets">Objecto <see cref="Assets"/> el cual se convierte.</param>
	public static implicit operator string(Assets assets) => assets.SearchPath;

	/// <summary>
	/// Convierte un directorio en un objecto <see cref="Assets"/>.
	/// </summary>
	/// <param name="path">Directorio que estara relacionado con el objecto <see cref="Assets"/>.</param>
	public static implicit operator Assets(string path) => new(path);

	public static bool operator ==(Assets? a, Assets? b)
	{
		if (a is null && b is null) return true;
		if (a is not null) return a.Equals(b);
		return b is not null && b.Equals(a);
	}

	public static bool operator !=(Assets? a, Assets? b)
	{
		return a.SearchPath != b.SearchPath;
	}

	/// <inheritdoc/>
	public bool Equals(Assets? other)
	{
		if (other is null) return false;
		return SearchPath == other.SearchPath;
	}

	/// <inheritdoc/>
	public override bool Equals(object? obj)
	{
		var other = obj as Assets;
		return Equals(other);
	}

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		// ReSharper disable once NonReadonlyMemberInGetHashCode
		return SearchPath.GetHashCode();
	}
}