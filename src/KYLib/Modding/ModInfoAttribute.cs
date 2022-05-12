using KYLib.Abstractions;

namespace KYLib.Modding;

/// <summary>
/// Indica un tipo que sera instanciado y asignado a la propiedad <see cref="Mod.ModInfo"/> cuando el ensamblado sea cargado.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
public class ModInfoAttribute : Attribute
{
	/// <summary>
	/// Obtiene el tipo <see cref="IModInfo"/> que debe ser instanciado.
	/// </summary>
	public Type? Type { get; }

	/// <summary>
	/// Crea una nueva instancia con un tipo dado.
	/// </summary>
	/// <param name="type">El tipo que debe ser instanciado.</param>
	/// <exception cref="ArgumentException"><paramref name="type"/> no implementa <see cref="IModInfo"/>.</exception>
	public ModInfoAttribute(Type type)
	{
		if (!typeof(IModInfo).IsAssignableFrom(type))
		{
			Type = null;
			throw new ArgumentException("type need to implement the IModInfo interface");
		}
		Type = type;
	}
}

/// <summary>
/// Indica un tipo que sera instanciado y asignado a la propiedad <see cref="Mod.ModInfo"/> cuando el ensamblado sea cargado.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
public class ModInfoAttribute<T> : ModInfoAttribute where T : IModInfo
{
	/// <summary>
	/// Crea una nueva instancia con un tipo dado.
	/// </summary>
	public ModInfoAttribute() : base(typeof(T))
	{
	}
}