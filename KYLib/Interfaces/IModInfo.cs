namespace KYLib.Interfaces;

/// <summary>
/// Proveedor de información acerca de un mod.
/// </summary>
public interface IModInfo : INameable
{
	/// <summary>
	/// Autor del mod.
	/// </summary>
	string Author { get; }

	/// <summary>
	/// Descripción del mod.
	/// </summary>
	string Description { get; }
}
