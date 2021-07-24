using System;

namespace KYLib.Utils
{
	/// <summary>
	/// 
	/// </summary>
	public static class Ensure
	{
		#region SetValue
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="target"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool SetValue<T>(ref T target, T value) where T : IEquatable<T>
		{
			if (target.Equals(value))
				return true;
			target = value;
			return false;
		}

		#endregion
		#region GreaterThan
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <param name="amount"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public static bool GreaterThan<T>(T value, T amount, bool error = true) where T : IComparable<T> =>
			GreaterThan(value, amount, null, error);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <param name="amount"></param>
		/// <param name="name"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public static bool GreaterThan<T>(T value, T amount, string name, bool error = true) where T : IComparable<T>
		{
			if (value.CompareTo(amount) > 0 && amount.CompareTo(value) < 0)
				return true;
			else if (!error)
				return false;
			throw new ArgumentOutOfRangeException(name);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <param name="amount"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public static bool NotGreaterThan<T>(T value, T amount, bool error = true) where T : IComparable<T> =>
			GreaterThan(value, amount, null, error);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <param name="amount"></param>
		/// <param name="name"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public static bool NotGreaterThan<T>(T value, T amount, string name, bool error = true) where T : IComparable<T>
		{
			if (value.CompareTo(amount) <= 0  && amount.CompareTo(value) >= 0)
				return true;
			else if (!error)
				return false;
			throw new ArgumentOutOfRangeException(name);
		}

		#endregion
		#region LessThan

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <param name="amount"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public static bool NotLessThan<T>(T value, T amount, bool error = true)
			where T : IComparable<T> =>
			NotLessThan<T>(value, amount, null, error);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <param name="amount"></param>
		/// <param name="name"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public static bool NotLessThan<T>(T value, T amount, string name, bool error = true) where T : IComparable<T>
		{
			if (value.CompareTo(amount) >= 0 && amount.CompareTo(value) <= 0)
				return true;
			else if (!error)
				return false;
			throw new ArgumentOutOfRangeException(name);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <param name="amount"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		public static bool LessThan<T>(T value, T amount, bool error = true) where T : IComparable<T> =>
			LessThan(value, amount, null, error);


		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <param name="amount"></param>
		/// <param name="name"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public static bool LessThan<T>(T value, T amount, string name, bool error = true) where T : IComparable<T>
		{
			if (value.CompareTo(amount) < 0 && amount.CompareTo(value) > 0)
				return true;
			else if (!error)
				return false;
			throw new ArgumentOutOfRangeException(name);
		}
		#endregion
		#region Null
		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static bool NotNull(object obj, bool error = true) =>
			NotNull(obj, null, error);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="name"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static bool NotNull(object obj, string name, bool error = true)
		{
			if (obj != null)
				return true;
			else if (!error)
				return false;
			throw new ArgumentNullException(name);
		}
		#endregion
	}
}
