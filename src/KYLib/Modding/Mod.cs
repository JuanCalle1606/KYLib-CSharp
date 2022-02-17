using System;
using System.Reflection;
using KYLib.Interfaces;
using KYLib.Utils;

namespace KYLib.Modding;

/// <summary>
/// Representa un ensamblado y provee metodos para facilitar el manejo.
/// </summary>
public partial class Mod : IEquatable<Mod> {
	/// <summary>
	/// Ensamblado relacionado a este objeto.
	/// </summary>
	public readonly Assembly Dll;

	/// <summary>
	/// Intancia de <see cref="IModInfo"/> que se crea si el ensamblado tiene un atributo <see cref="ModInfoAttribute"/>.
	/// </summary>
	public readonly IModInfo? ModInfo;

	Mod(Assembly assembly)
	{
		Ensure.NotNull(assembly, nameof(assembly));
		var att = assembly.GetCustomAttribute<ModInfoAttribute>();
		if (att != null)
			ModInfo = (IModInfo)Activator.CreateInstance(att.Type);
		Dll = assembly;
	}

	/// <inheritdoc/>
	public bool Equals(Mod other) =>
		Dll.Equals(other?.Dll);

	/// <inheritdoc/>
	public override bool Equals(object obj) => Dll.Equals(obj);

	/// <inheritdoc/>
	public override int GetHashCode() => Dll.GetHashCode();

	/// <inheritdoc/>
	public override string ToString() => Dll.ToString();
}
