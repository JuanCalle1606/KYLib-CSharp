using System;
using System.Dynamic;

namespace KYLib.Utils;

public class ObjectWrapper : DynamicObject
{

	public object Value { get; }

	public ObjectWrapper(object @object)
	{
		Ensure.NotNull(@object, nameof(@object));
		Value = @object;
	}

	public virtual T As<T>()
	{
		return (T)Value;
	}

	public virtual bool Is<T>()
	{
		return Value is T;
	}

	public virtual bool Is(Type type)
	{
		if (Value is null) return false;
		return type.IsAssignableFrom(Value.GetType());
	}

}
