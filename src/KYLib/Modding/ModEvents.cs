
using System;
namespace KYLib.Modding;

partial class Mod
{
	/// <summary>
	/// Se lanza cuando un tipo con <see cref="AutoLoadAttribute"/> es cargado.
	/// </summary>
	public static event TypeAutoLoadedHandler OnTypeAutoLoaded;

	internal static void TypeAutoLoaded(Mod container, Type type)
	{
		OnTypeAutoLoaded?.Invoke(container, type);
	}

	public delegate void TypeAutoLoadedHandler(Mod container, Type type);
}
