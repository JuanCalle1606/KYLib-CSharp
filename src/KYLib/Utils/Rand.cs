using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable SuggestVarOrType_BuiltInTypes

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
	public static kint GetInt() => _Random.Next();

	/// <summary>
	/// Obtiene un entero al azar mayor o igual que <paramref name="min"/> y menor que <paramref name="max"/>.
	/// </summary>
	/// <param name="min">Valor minimo que puede tener el numero.</param>
	/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
	public static kint GetInt(kint min, kint max) => _Random.Next(min, max);

	/// <summary>
	/// Obtiene un entero mayor o igual a 0 y menor que <paramref name="max"/>. 
	/// </summary>
	/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
	public static kint GetInt(kint max) => _Random.Next(max);

	/// <summary>
	/// Obtiene un conjunto de <paramref name="amount"/> enteros al azar mayores o iguales que <paramref name="min"/> y menores que <paramref name="max"/>.
	/// </summary>
	/// <param name="min">Valor minimo que puede tener el numero.</param>
	/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
	/// <param name="amount">Cantidad de numeros a generar.</param>
	public static kint[] GetInt(kint min, kint max, kint amount)
	{
		if (!(amount > 0))
			throw new ArgumentException("No se puede generar un numero negativo de numeros.", nameof(amount));
		var dev = new kint[amount];
		for (kint i = 0; i < amount; i++)
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
	public static kint[] GetInt(kint min, kint max, kint minAmount, kint maxAmout) =>
		GetInt(min, max, GetInt(minAmount, maxAmout));

	/// <summary>
	/// Obtiene un numero flotante al azar mayor o igual a 0 y menor a 1.
	/// </summary>
	public static kfloat Get() => Convert.ToSingle(_Random.NextDouble());

	/// <summary>
	/// Obtiene un numero flotante al azar mayor o igual que 0 y menor que <paramref name="max"/>.
	/// </summary>
	/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
	public static kfloat Get(kfloat max) => Get() * max;

	/// <summary>
	/// Obtiene un numero flotante al azar mayor o igual que <paramref name="min"/> y menor que <paramref name="max"/>.
	/// </summary>
	/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
	/// <param name="min">Valor minimo que puede tener el numero.</param>
	public static kfloat Get(kfloat min, kfloat max) => Get() * (max - min) + min;

	/// <summary>
	/// Obtiene un conjunto de <paramref name="amount"/> numeros flotantes al azar mayores o iguales que <paramref name="min"/> y menores que <paramref name="max"/>.
	/// </summary>
	/// <param name="min">Valor minimo que puede tener el numero.</param>
	/// <param name="max">Valor maximo excluyente que puede tener el numero.</param>
	/// <param name="amount">Cantidad de numeros a generar.</param>
	public static kfloat[] Get(kfloat min, kfloat max, kint amount)
	{
		if (!(amount > 0))
			throw new ArgumentException();
		var dev = new kfloat[amount];
		for (kint i = 0; i < amount; i++)
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
	public static kfloat[] Get(kfloat min, kfloat max, kint minAmount, kint maxAmout) =>
		Get(min, max, GetInt(minAmount, maxAmout));
	/*
	public static kfloat GetFixed(Int decimals) => Fix(Get(), decimals);

	public static kfloat GetFixed(Int decimals, kfloat max) => Fix(Get() * max, decimals);

	public static kfloat GetFixed(Int decimals, kfloat min, kfloat max) => Fix((Get() * (max - min)) + min, decimals);

	public static kfloat[] GetFixed(Int decimals, kfloat min, kfloat max, Int amount)
	{
		if (!(amount > 0))
			throw new ArgumentException();
		kfloat[] dev = new kfloat[amount];
		for (Int i = 0; i < amount; i++)
			dev[i] = GetFixed(decimals, min, max);
		return dev;
	}

	public static kfloat[] GetFixed(Int decimals, kfloat min, kfloat max, Int minAmount, Int maxAmout) =>
		GetFixed(decimals, min, max, GetInt(minAmount, maxAmout + 1));
	*/
	/// <summary>
	/// Obtiene un numero de doble presición mayor o igual que 0 y menor que 1.
	/// </summary>
	public static kdouble GetDouble() => _Random.NextDouble();

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

	//public static kfloat Fix(kfloat number, Int decimals) =>
	//	Convert.ToSingle(Math.Round(number, decimals));
}
