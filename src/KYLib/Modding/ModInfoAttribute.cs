using KYLib.Interfaces;
using System;

namespace KYLib.Modding;

[AttributeUsage(AttributeTargets.Assembly)]
public class ModInfoAttribute : Attribute
{
	public Type Type { get; private set; }

	public ModInfoAttribute(Type type)
	{
		if (!typeof(IModInfo).IsAssignableFrom(type))
			throw new ArgumentException("type need to implement the IModInfo interface");
		Type = type;
	}
}
