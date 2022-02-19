using KYLib.Utils;

namespace KYLib.Extensions;

/// <summary>
/// Extenciones para todo tipo de objeto
/// </summary>
public static class ObjectExtensions
{
	/// <summary>
	/// Crea un contenedor que encapcula al objeto dado.
	/// </summary>
	public static ObjectWrapper Wrap(this object obj) => new(obj);

}

