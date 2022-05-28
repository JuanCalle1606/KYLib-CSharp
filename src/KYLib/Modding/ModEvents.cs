namespace KYLib.Modding;
 
partial class Mod
{
	/// <summary>
	/// Se lanza cuando un tipo con <see cref="AutoLoadAttribute"/> es cargado.
	/// </summary>
	public static event TypeAutoLoadedHandler TypeAutoLoaded;

	internal static void InternalTypeAutoLoaded(Mod container, Type type)
	{
		TypeAutoLoaded?.Invoke(container, type);
	}
	
	/// <summary>
	/// Representa un metodo que puede capturar un evento de carga de un <see cref="Type"/>.
	/// </summary>
	public delegate void TypeAutoLoadedHandler(Mod container, Type type);
}
