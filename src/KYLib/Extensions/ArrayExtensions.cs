using System;
using KYLib.MathFn;

namespace KYLib.Extensions;

/// <summary>
/// Extenciones generales para los arreglos.
/// </summary>
public static class ArrayExtensions
{
	#region Metodos generales 
	/// <summary>
	/// Devuelve el indice de <paramref name="target"/> en el arreglo.
	/// </summary>
	/// <typeparam name="T">Cualquier tipo.</typeparam>
	/// <param name="arr">Arreglo de origen.</param>
	/// <param name="target">Elemento a buscar dentro del arreglo.</param>
	/// <returns>Devuelve el indice en el que se encuentra <paramref name="target"/>, -1 si no se encuentra.</returns>
	public static int IndexOf<T>(this T[] arr, T target) =>
		Array.IndexOf(arr, target);

	/// <summary>
	/// Ordena los elementos del arreglo utilizando un <see cref="Comparison{T}"/> especificado.
	/// </summary>
	/// <typeparam name="T">Cualquier tipo.</typeparam>
	/// <param name="arr">Arreglo de origen.</param>
	/// <param name="comparison"><see cref="Comparison{T}"/> que se usara para ordena el arreglo.</param>
	public static void Sort<T>(this T[] arr, Comparison<T> comparison) =>
		Array.Sort(arr, comparison);

	/// <summary>
	/// Ordena el arreglo con el <see cref="IComparable"/> por defecto de ada elemento.
	/// </summary>
	/// <param name="arr">Arreglo de origen.</param>
	public static void Sort(this Array arr) => Array.Sort(arr);

	/// <summary>
	/// Ordena los elementos del arreglo de forma ascendente.
	/// </summary>
	/// <typeparam name="T">Un tipo que implemente <see cref="IComparable{T}"/>.</typeparam>
	/// <param name="arr">Arreglo de origen.</param>
	public static void SortAsc<T>(this T[] arr) where T : IComparable<T> =>
		arr.Sort((t1, t2) => t1.CompareTo(t2));

	/// <summary>
	/// Ordena los elementos del arreglo de forma descendente.
	/// </summary>
	/// <typeparam name="T">Un tipo que implemente <see cref="IComparable{T}"/>.</typeparam>
	/// <param name="arr">Arreglo de origen.</param>
	public static void SortDesc<T>(this T[] arr) where T : IComparable<T> =>
		arr.Sort((t1, t2) => t2.CompareTo(t1));
	#endregion

	#region Conversiones
	/// <summary>
	/// Convierte un arreglo de arreglos en una matriz.
	/// </summary>
	/// <typeparam name="T">Cualquier tipo.</typeparam>
	/// <param name="arr">Arreglo de origen.</param>
	/// <returns>Una matriz bidimensional de <typeparamref name="T"/>.</returns>
	public static T[,] ToMatriz<T>(this T[][] arr)
	{
		Int cols = arr.Length;
		Int rows = arr[0].Length;
		if (!arr.TrueForAll(t => t.Length == rows))
			throw new ArgumentException("Todos lo arreglos internos deben tener la misma longitud");
		var dev = new T[cols, rows];
		for (Int i = 0; i < cols; i++)
			for (Int j = 0; j < rows; j++)
				dev[i, j] = arr[i][j];

		return dev;
	}

	/// <inheritdoc/>
	public static string ToString<T>(this T[][] arr, char separator, bool multiline, bool showindex)
	{
		var dev = "";
		for (Int i = 0; i < arr.Length; i++)
		{
			if (showindex)
				dev += $"{i}: ";
			dev += arr[i].ToString(separator) + separator;
			if (multiline && i > 0)
				dev += $"{separator}\n";
		}
		dev = dev.TrimEnd(separator);
		return dev;
	}

	/// <inheritdoc/>
	public static string ToString<T>(this T[][] arr, char separator, bool multiline) =>
		arr.ToString(separator, multiline, false);
	#endregion
}