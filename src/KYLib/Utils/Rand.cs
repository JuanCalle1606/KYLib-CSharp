using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace KYLib.Utils;

/// <summary>
/// Clase que se usa para obtención de valores al azar.
/// </summary>
public static class Rand
{
	/// <summary>
	/// Objeto que se usa para la obtención al azar de datos.
	/// </summary>
	static readonly Random _Random = new();

	/// <summary>
	/// Obtiene un entero al azar.
	/// </summary>
	public static int GetInt() => _Random.Next();

	/// <summary>
	/// Obtiene un entero al azar mayor o igual que <paramref name="min"/> y menor que <paramref name="max"/>.
	/// </summary>
	/// <param name="min">Valor minimo que puede tener el numero.</param>
	/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
	public static int GetInt(int min, int max) => _Random.Next(min, max);

	/// <summary>
	/// Obtiene un entero mayor o igual a 0 y menor que <paramref name="max"/>. 
	/// </summary>
	/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
	public static int GetInt(int max) => _Random.Next(max);

	/// <summary>
	/// Obtiene un conjunto de <paramref name="amount"/> enteros al azar mayores o iguales que <paramref name="min"/> y menores que <paramref name="max"/>.
	/// </summary>
	/// <param name="min">Valor minimo que puede tener el numero.</param>
	/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
	/// <param name="amount">Cantidad de numeros a generar.</param>
	public static int[] GetInt(int min, int max, int amount)
	{
		if (!(amount > 0))
			throw new ArgumentException("No se puede generar un numero negativo de numeros.", nameof(amount));
		var dev = new int[amount];
		for (var i = 0; i < amount; i++)
			dev[i] = GetInt(min, max);
		return dev;
	}

	/// <summary>
	/// Obtiene un arreglo de enteros al azar mayores o iguales que <paramref name="min"/> y menores que <paramref name="max"/>.
	/// </summary>
	/// <param name="min">Valor minimo que puede tener el numero.</param>
	/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
	/// <param name="minAmount">Cantidad minima de numeros que pueden ser generados.</param>
	/// <param name="maxAmout">Cantidad maxima excluyente de numeros que pueden ser generados.</param>
	/// <returns>Un arreglo cuya longitud es mayor o igual que <paramref name="minAmount"/> y menor que <paramref name="maxAmout"/> de numeros enteros al azar.</returns>
	public static int[] GetInt(int min, int max, int minAmount, int maxAmout) =>
		GetInt(min, max, GetInt(minAmount, maxAmout));

	/// <summary>
	/// Obtiene un numero flotante al azar mayor o igual a 0 y menor a 1.
	/// </summary>
	public static float Get() => Convert.ToSingle(_Random.NextDouble());

	/// <summary>
	/// Obtiene un numero flotante al azar mayor o igual que 0 y menor que <paramref name="max"/>.
	/// </summary>
	/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
	public static float Get(float max) => Get() * max;

	/// <summary>
	/// Obtiene un numero flotante al azar mayor o igual que <paramref name="min"/> y menor que <paramref name="max"/>.
	/// </summary>
	/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
	/// <param name="min">Valor minimo que puede tener el numero.</param>
	public static float Get(float min, float max) => (Get() * (max - min)) + min;

	/// <summary>
	/// Obtiene un conjunto de <paramref name="amount"/> numeros flotantes al azar mayores o iguales que <paramref name="min"/> y menores que <paramref name="max"/>.
	/// </summary>
	/// <param name="min">Valor minimo que puede tener el numero.</param>
	/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
	/// <param name="amount">Cantidad de numeros a generar.</param>
	public static float[] Get(float min, float max, int amount)
	{
		if (!(amount > 0))
			throw new ArgumentException();
		var dev = new float[amount];
		for (var i = 0; i < amount; i++)
			dev[i] = Get(min, max);
		return dev;
	}

	/// <summary>
	/// Obtiene un arreglo de numeros flotantes al azar mayores o iguales que <paramref name="min"/> y menores que <paramref name="max"/>.
	/// </summary>
	/// <param name="min">Valor minimo que puede tener el numero.</param>
	/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
	/// <param name="minAmount">Cantidad minima de numeros que pueden ser generados.</param>
	/// <param name="maxAmout">Cantidad maxima excluyente de numeros que pueden ser generados.</param>
	/// <returns>Un arreglo cuya longitud es mayor o igual que <paramref name="minAmount"/> y menor que <paramref name="maxAmout"/> de numeros enteros al azar.</returns>
	public static float[] Get(float min, float max, int minAmount, int maxAmout) =>
		Get(min, max, GetInt(minAmount, maxAmout));
	/*
	public static float GetFixed(Int decimals) => Fix(Get(), decimals);

	public static float GetFixed(Int decimals, float max) => Fix(Get() * max, decimals);

	public static float GetFixed(Int decimals, float min, float max) => Fix((Get() * (max - min)) + min, decimals);

	public static float[] GetFixed(Int decimals, float min, float max, Int amount)
	{
		if (!(amount > 0))
			throw new ArgumentException();
		float[] dev = new float[amount];
		for (Int i = 0; i < amount; i++)
			dev[i] = GetFixed(decimals, min, max);
		return dev;
	}

	public static float[] GetFixed(Int decimals, float min, float max, Int minAmount, Int maxAmout) =>
		GetFixed(decimals, min, max, GetInt(minAmount, maxAmout + 1));
	*/
	/// <summary>
	/// Obtiene un numero de doble presición mayor o igual que 0 y menor que 1.
	/// </summary>
	public static double GetDouble() => _Random.NextDouble();

	/// <summary>
	/// Escoge un elemento al azar de un enumerable de objetos.
	/// </summary>
	/// <param name="arr">Arreglo de origen.</param>
	/// <typeparam name="T">Cualquier tipo.</typeparam>
	/// <returns>Un elemento al azar escogido de <paramref name="arr"/>.</returns>
	public static T Choose<T>(IEnumerable<T> arr)
	{
		Ensure.NotNull(arr, nameof(arr));
		var enumerable = arr as T[] ?? arr.ToArray();
		return enumerable.ElementAt(GetInt(enumerable.Length));
	}

	//public static float Fix(float number, Int decimals) =>
	//	Convert.ToSingle(Math.Round(number, decimals));
}
