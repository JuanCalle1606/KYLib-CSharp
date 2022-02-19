using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYLib.Interfaces;
using KYLib.MathFn;
using KYLib.Utils;

namespace KYLib.Extensions;

/// <summary>
/// Extensiones generales para elementos que implementen IEnumerable.
/// </summary>
public static class EnumerableExtensions
{
	#region Retorno nuevo
	/// <summary>
	/// Desordena el IEnumerable.
	/// </summary>
	/// <typeparam name="T">Cualquier tipo.</typeparam>
	/// <param name="source">Origen de datos.</param>
	/// <returns>Devuelve los datos desordenados al azar.</returns>
	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
	{
		var elements = source.ToArray();
		for (kint i = elements.Length - 1; i > 0; i--)
		{
			kint swapIndex = Rand.GetInt(i + 1);
			yield return elements[swapIndex];
			elements[swapIndex] = elements[i];
		}
		yield return elements[0];
	}

	#endregion

	#region Utilidades
	/// <summary>
	/// Busca un elemento que tenga como nombre <paramref name="name"/>.
	/// </summary>
	/// <typeparam name="T">Tipo que implemente <see cref="INameable"/>.</typeparam>
	/// <param name="arr">Enumerable de origen.</param>
	/// <param name="name">Nombre a buscar.</param>
	/// <returns>El primer elemento de la lista con <paramref name="name"/> como nombre, default si no hay ninguno.</returns>
	public static T FindByName<T>(this IEnumerable<T> arr, string name) where T : INameable =>
		arr.ToList().Find(t => t.Name.Equals(name));

	/// <summary>
	/// Determina si se cumple una condición para cada elemento en el <see cref="IEnumerable{T}"/>.
	/// </summary>
	/// <typeparam name="T">Cualquier tipo.</typeparam>
	/// <param name="arr">Enumerable de origen.</param>
	/// <param name="predicate"></param>
	/// <returns>Devuelve <c>true</c> si cada elemento del <see cref="IEnumerable{T}"/> cumple con la condición <paramref name="predicate"/>, de lo contrario devuelve <c>false</c>.</returns>
	/// <exception cref="ArgumentException">Se lanza cuando el predicado es nulo.</exception>
	public static bool TrueForAll<T>(this IEnumerable<T> arr, Predicate<T> predicate) =>
		Array.TrueForAll(arr.ToArray(), predicate);

	#endregion

	#region Listas
	/// <summary>
	/// Rellena el inicio de una lista con <paramref name="content"/> hasta que tenga longitud <paramref name="endIn"/>.
	/// </summary>
	/// <typeparam name="T">Cualquier tipo.</typeparam>
	/// <param name="arr">Lista de origen.</param>
	/// <param name="endIn">Longitud que debe tener la lista para dejar de insertarle <paramref name="content"/>.</param>
	/// <param name="content">Contenido con el que queremos llenar la lista.</param>
	public static void FillStart<T>(this List<T> arr, kint endIn, T content)
	{
		while (arr.Count < endIn) arr.Insert(0, content);
	}

	/// <summary>
	/// Rellena el final de una lista con <paramref name="content"/> hasta que tenga longitud <paramref name="endIn"/>.
	/// </summary>
	/// <typeparam name="T">Cualquier tipo.</typeparam>
	/// <param name="arr">Lista de origen.</param>
	/// <param name="endIn">Longitud que debe tener la lista para dejar de insertarle <paramref name="content"/>.</param>
	/// <param name="content">Contenido con el que queremos llenar la lista.</param>
	public static void FillEnd<T>(this List<T> arr, kint endIn, T content)
	{
		while (arr.Count < endIn) arr.Add(content);
	}
	#endregion

	#region Conversiones
	/// <summary>
	/// Convierte este <see cref="IEnumerable"/> en uno de tipo <see cref="IEnumerable{T}"/> casteando sus elementos con el tipo <see langword="dynamic"/>.
	/// </summary>
	/// <typeparam name="T">Tipo al cual se intentara convertir cada elemento original.</typeparam>
	/// <param name="arr">Arreglo de origen.</param>
	/// <returns>Devuelve un nuevo enumerable con todos sus elementos convertidos al tipo <typeparamref name="T"/>.</returns>
	public static IEnumerable<T> To<T>(this IEnumerable arr)
	{
		foreach (dynamic item in arr)
			yield return (T)item;			
	}

	/// <summary>
	/// Convierte un arreglo de tipo <typeparamref name="TInput"/> en un arreglo de tipo <typeparamref name="TOutput"/> usando un <see cref="Converter{TInput, TOutput}"/>.
	/// </summary>
	/// <typeparam name="TInput">Cualquier tipo.</typeparam>
	/// <typeparam name="TOutput">Cualquier tipo.</typeparam>
	/// <param name="arr">Arreglo de origen.</param>
	/// <param name="converter">Delegado que se usa para convertir cada elemento del arreglo.</param>
	/// <returns>Un nuevo arreglo con todos los elementos convertidos.</returns>
	/// <exception cref="ArgumentNullException">El delegado de conversión es nulo.</exception>
	public static TOutput[] ToArray<TInput, TOutput>(
		this IEnumerable<TInput> arr, Converter<TInput, TOutput> converter)
	{
		Ensure.NotNull(converter, nameof(converter));
		return arr.Select(i => converter(i)).ToArray();
	}

	/// <summary>
	/// Convierte un enumerable de tipo <typeparamref name="T"/> en un arreglo de <see cref="Int"/>.
	/// </summary>
	/// <typeparam name="T">Cualquier tipo que implemente <see cref="IConvertible"/></typeparam>.
	/// <param name="arr">Enumerable de origen.</param>
	/// <returns>Un arreglo de <see cref="Int"/> resultante de la conversión.</returns>
	public static Int[] ToIntArray<T>(this IEnumerable<T> arr) where T : IConvertible =>
		arr.ToArray(t => (Int)Convert.ToInt32(t));

	/// <summary>
	/// Convierte un enumerable de tipo <typeparamref name="T"/> en un arreglo de <see cref="float"/>.
	/// </summary>
	/// <typeparam name="T">Cualquier tipo.</typeparam>
	/// <param name="arr">Enumerable de origen.</param>
	/// <returns>Un arreglo de <see cref="float"/> resultante de la conversión.</returns>
	public static float[] ToFloatArray<T>(this IEnumerable<T> arr) where T : IConvertible =>
		arr.ToArray(t => Convert.ToSingle(t));

	/// <inheritdoc/>
	public static string ToString<T>(this IEnumerable<T> arr, bool showindex, bool multiline) =>
		arr.ToString(null, showindex, multiline);
		
	/// <inheritdoc/>
	public static string ToString<T>(this IEnumerable<T> arr, bool showindex) =>
		arr.ToString(null, showindex, false);
		
	/// <inheritdoc/>
	public static string ToString<T>(this IEnumerable<T> arr, char? separator) =>
		arr.ToString(separator, false);
		
	/// <inheritdoc/>
	public static string ToString<T>(this IEnumerable<T> arr, char? separator, bool showindex) =>
		arr.ToString(separator, showindex, false);

	/// <inheritdoc/>
	public static string ToString<T>(this IEnumerable<T> arr, char? separator, bool showindex, bool multiline)
	{
		StringBuilder stringBuilder = new();
		kint i = 0;
		foreach (var item in arr)
		{
			if (multiline && i > 0)
				stringBuilder.Append("\n");
			if (showindex)
				stringBuilder.Append($"{i}: ");
			stringBuilder.Append($"{item}{separator}");
			i++;
		}
		return stringBuilder.ToString().TrimEnd(separator ?? ' ');
	}
	#endregion
}