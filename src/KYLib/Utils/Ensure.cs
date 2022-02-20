using System;
#pragma warning disable CS1573
namespace KYLib.Utils;

/// <summary>
/// Provee metodos para validaciones de parametros.
/// </summary>
public static class Ensure {

	#region SetValue
	
	/// <summary>
	/// Asigna un valor a una variable si esta aun no tiene ese valor.
	/// </summary>
	/// <param name="target">Referencia a la variable que se va a asignar.</param>
	/// <param name="value">Valor que debe ser asignado a la variable.</param>
	/// <typeparam name="T">Cualquier tipo <see cref="IEquatable{T}"/>.</typeparam>
	/// <returns>Si <paramref name="target"/> es igual a <paramref name="value"/> entonces devuelve <c>true</c>, de lo contrario le asigna el valor de <paramref name="value"/> a <paramref name="target"/> y devuelve <c>false</c>.</returns>
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
	/// Valida que un objeto debe ser mayor que otro.
	/// </summary>
	/// <param name="obj">Objeto a comparar.</param>
	/// <param name="amount">Valor de referencia.</param>
	/// <param name="name">Nombre de la variable.</param>
	/// <param name="error">Indica si se debe generar un error si <paramref name="obj"/> no es mayor que <paramref name="amount"/>.</param>
	/// <typeparam name="T">Cualquier tipo  <see cref="IComparable{T}"/>.</typeparam>
	/// <returns>Si <paramref name="obj"/> es mayor que <paramref name="amount"/> retorna <c>true</c>, en caso de que no sea mayor se retorna <c>false</c> si y solo si <paramref name="error"/> es <c>false</c>, de lo contrario se genera un error.</returns>
	/// <exception cref="ArgumentOutOfRangeException"><see cref="error"/> es <c>true</c> y <paramref name="obj"/> no es mayor que <paramref name="amount"/>.</exception>
	public static bool GreaterThan<T>(T obj, T amount, string? name, bool error = true) where T : IComparable<T>
	{
		if (obj.CompareTo(amount) > 0 && amount.CompareTo(obj) < 0)
			return true;
		if (!error)
			return false;
		throw name is null ? new() : new ArgumentOutOfRangeException(name);
	}
	
	/// <summary>
	/// Valida que un objeto no debe ser mayor que otro.
	/// </summary>
	/// <param name="obj">Objeto a comparar.</param>
	/// <param name="amount">Valor de referencia.</param>
	/// <param name="name">Nombre de la variable.</param>
	/// <param name="error">Indica si se debe generar un error si <paramref name="obj"/> es mayor que <paramref name="amount"/>.</param>
	/// <typeparam name="T">Cualquier tipo  <see cref="IComparable{T}"/>.</typeparam>
	/// <returns>Si <paramref name="obj"/> no es mayor que <paramref name="amount"/> retorna <c>true</c>, en caso de que sea mayor se retorna <c>false</c> si y solo si <paramref name="error"/> es <c>false</c>, de lo contrario se genera un error.</returns>
	/// <exception cref="ArgumentOutOfRangeException"><see cref="error"/> es <c>true</c> y <paramref name="obj"/> es mayor que <paramref name="amount"/>.</exception>
	public static bool NotGreaterThan<T>(T obj, T amount, string? name, bool error = true) where T : IComparable<T>
	{
		if (obj.CompareTo(amount) <= 0 && amount.CompareTo(obj) >= 0)
			return true;
		if (!error)
			return false;
		throw new ArgumentOutOfRangeException(name);
	}

	#endregion

	#region LessThan
	
	/// <summary>
	/// Valida que un objeto no debe ser menor que otro.
	/// </summary>
	/// <param name="obj">Objeto a comparar.</param>
	/// <param name="amount">Valor de referencia.</param>
	/// <param name="name">Nombre de la variable.</param>
	/// <param name="error">Indica si se debe generar un error si <paramref name="obj"/> es menor que <paramref name="amount"/>.</param>
	/// <typeparam name="T">Cualquier tipo  <see cref="IComparable{T}"/>.</typeparam>
	/// <returns>Si <paramref name="obj"/> no es menor que <paramref name="amount"/> retorna <c>true</c>, en caso de que sea menor se retorna <c>false</c> si y solo si <paramref name="error"/> es <c>false</c>, de lo contrario se genera un error.</returns>
	/// <exception cref="ArgumentOutOfRangeException"><see cref="error"/> es <c>true</c> y <paramref name="obj"/> es menor que <paramref name="amount"/>.</exception>
	public static bool NotLessThan<T>(T obj, T amount, string? name, bool error = true) where T : IComparable<T>
	{
		if (obj.CompareTo(amount) >= 0 && amount.CompareTo(obj) <= 0)
			return true;
		if (!error)
			return false;
		throw name is null ? new() : new ArgumentOutOfRangeException(name);
	}
	
	/// <summary>
	/// Valida que un objeto debe ser menor que otro.
	/// </summary>
	/// <param name="obj">Objeto a comparar.</param>
	/// <param name="amount">Valor de referencia.</param>
	/// <param name="name">Nombre de la variable.</param>
	/// <param name="error">Indica si se debe generar un error si <paramref name="obj"/> no es menor que <paramref name="amount"/>.</param>
	/// <typeparam name="T">Cualquier tipo  <see cref="IComparable{T}"/>.</typeparam>
	/// <returns>Si <paramref name="obj"/> es menor que <paramref name="amount"/> retorna <c>true</c>, en caso de que no sea menor se retorna <c>false</c> si y solo si <paramref name="error"/> es <c>false</c>, de lo contrario se genera un error.</returns>
	/// <exception cref="ArgumentOutOfRangeException"><see cref="error"/> es <c>true</c> y <paramref name="obj"/> no es menor que <paramref name="amount"/>.</exception>
	public static bool LessThan<T>(T obj, T amount, string? name, bool error = true) where T : IComparable<T>
	{
		if (obj.CompareTo(amount) < 0 && amount.CompareTo(obj) > 0)
			return true;
		if (!error)				
			return false;
		throw name is null ? new() : new ArgumentOutOfRangeException(name);
	}

	#endregion

	#region Null

	/// <summary>
	/// Valida que un objeto no sea <c>null</c>.
	/// </summary>
	/// <param name="obj">Objeto a validar.</param>
	/// <param name="name">Nombre de la variable.</param>
	/// <param name="error">Indica si se debe generar un error en caso de que <paramref name="obj"/> se <c>null</c>.</param>
	/// <returns>Si <paramref name="obj"/> es no nulo se retorna <c>true</c>, en caso de que si sea nulo se retorna <c>false</c> si y solo si <paramref name="error"/> es <c>false</c>, de lo contrario se genera un error.</returns>
	/// <exception cref="ArgumentException"><see cref="error"/> es <c>true</c> y <paramref name="obj"/> es nulo.</exception>
	public static bool NotNull(object? obj, string? name, bool error = true)
	{
		if (obj is not null)
			return true;
		if (!error)
			return false;
		throw name is null ? new() : new ArgumentNullException(name);
	}

	#endregion

}
