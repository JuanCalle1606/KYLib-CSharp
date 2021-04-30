using System;
using System.Collections.Generic;
using System.Reflection;

namespace KYLib.Utils
{
	/// <summary>
	/// 
	/// </summary>
	public static class TypeValidator
	{
		private static readonly Dictionary<Type, object> ObjectDic = new();

		private static readonly Type ThisType = typeof(InternalValidator<>);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool Is(object instance, Type type)
		{
			Type tempType = ThisType.MakeGenericType(new Type[] { type });
			MethodInfo method = tempType.GetMethod("Is");
			return method.Invoke(
				ObjectOfType(tempType),
				new object[] { instance })
			.Equals(true);
		}
		/*
		public static bool Invoke(out object output, Type type, string name, object parameter) =>
			Invoke(out output, type, name, new object[] { parameter });

		public static bool Invoke(out object output, Type type, string name, object[] parameters)
		{
			MethodInfo method = type.GetMethod(name);

			if (method != null)
			{
				output = method.Invoke(ObjectOfType(type), parameters);
				return true;
			}
			else
			{
				output = null;
				return false;
			}
		}
		*/
		private static object ObjectOfType(Type type)
		{
			if (ObjectDic.ContainsKey(type))
			{
				return ObjectDic[type];
			}
			ObjectDic.Add(type, Activator.CreateInstance(type));
			return ObjectDic[type];
		}
		private class InternalValidator<T>
		{
			public bool Is(object instance) => instance is T;
		}
	}
}
