using KYLib.Utils;

namespace KYLib.Extensions;

public static class TypeExtensions
{
	public static object? Instance(this Type type)
	{
		Ensure.NotNull(type, nameof(type));
		return Activator.CreateInstance(type, true);
	}

	public static T? Instance<T>(this Type type)
	{
		return (T?)type.Instance();
	}

	public static T RequiredInstance<T>(this Type type)
	{
		var instance = type.Instance<T>();
		Ensure.NotNull(instance, nameof(instance));
		return instance!;
	}
}
