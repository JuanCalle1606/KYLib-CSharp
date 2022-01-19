
using System;

namespace KYLib.Modding;

partial class Mod
{
	public static event TypeAutoLoadedHandler OnTypeAutoLoaded;

	internal static void TypeAutoLoaded(Mod container, Type type)
	{
		if (OnTypeAutoLoaded != null)
			OnTypeAutoLoaded(container, type);
	}

	public delegate void TypeAutoLoadedHandler(Mod container, Type type);
}
