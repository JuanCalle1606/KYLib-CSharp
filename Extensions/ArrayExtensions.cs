using System;

namespace KYLib.Extensions
{
	/// <summary>
	/// Extenciones generales para los arreglos.
	/// </summary>
	public static class ArrayExtension
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
			arr.Sort((T1, T2) => T1.CompareTo(T2));
		/// <summary>
		/// Ordena los elementos del arreglo de forma descendente.
		/// </summary>
		/// <typeparam name="T">Un tipo que implemente <see cref="IComparable{T}"/>.</typeparam>
		/// <param name="arr">Arreglo de origen.</param>
		public static void SortDesc<T>(this T[] arr) where T : IComparable<T> =>
			arr.Sort((T1, T2) => T2.CompareTo(T1));
		#endregion

		#region Conversiones
		/// <summary>
		/// Convierte un arreglo de tipo <typeparamref name="T"/> en un arreglo de <see cref="int"/>.
		/// </summary>
		/// <typeparam name="T">Cualquier tipo que implemente <see cref="IConvertible"/></typeparam>.
		/// <param name="arr">Arreglo de origen.</param>
		/// <returns>Un arreglo de <see cref="int"/> resultante de la conversión.</returns>
		public static int[] ToIntArray<T>(this T[] arr) where T : IConvertible =>
			arr.ToArray(t => Convert.ToInt32(t));

		/// <summary>
		/// Convierte un arreglo de tipo <typeparamref name="T"/> en un arreglo de <see cref="float"/>.
		/// </summary>
		/// <param name="arr">Arreglo de origen.</param>
		/// <returns>Un arreglo de <see cref="float"/> resultante de la conversión.</returns>
		public static float[] ToFloatArray<T>(this T[] arr) where T : IConvertible =>
			arr.ToArray(t => Convert.ToSingle(t));

		/// <summary>
		/// Convierte un arreglo de tipo <typeparamref name="TInput"/> en un arreglo de tipo <typeparamref name="TOutput"/> usando un <see cref="Converter{TInput, TOutput}"/>.
		/// </summary>
		/// <typeparam name="TInput">Cualquier tipo.</typeparam>
		/// <typeparam name="TOutput">Cualquier tipo.</typeparam>
		/// <param name="arr">Arreglo de origen.</param>
		/// <param name="converter">Delegado que se usa para convertir cada elemento del arreglo.</param>
		/// <returns>Un nuevo arreglo con todos los elementos convertidos.</returns>
		public static TOutput[] ToArray<TInput, TOutput>(this TInput[] arr, Converter<TInput, TOutput> converter)
		{
			int len = arr.Length;
			TOutput[] dev = new TOutput[len];
			for (int i = 0; i < len; i++)
				dev[i] = converter(arr[i]);
			return dev;
		}

		/// <inheritdoc/>
		public static string ToString<T>(this T[][] arr, char separator, bool multiline, bool showindex)
		{
			string dev = "";
			for (int i = 0; i < arr.Length; i++)
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
}
