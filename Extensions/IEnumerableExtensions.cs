using System.Collections.Generic;
using System.Linq;
using KYLib.Utils;

namespace KYLib.Extensions
{
	/// <summary>
	/// Extensiones generales para elementos que implementen IEnumerable.
	/// </summary>
	public static class IEnumerableExtensions
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
			T[] elements = source.ToArray();
			for (int i = elements.Length - 1; i > 0; i--)
			{
				int swapIndex = Rand.GetInt(i + 1);
				yield return elements[swapIndex];
				elements[swapIndex] = elements[i];
			}
			yield return elements[0];
		}

		#endregion

		#region Listas
		/// <summary>
		/// Rellena el inicio de una lista con <paramref name="content"/> hasta que tenga longitud <paramref name="EndIn"/>.
		/// </summary>
		/// <typeparam name="T">Cualquier tipo.</typeparam>
		/// <param name="arr">Lista de origen.</param>
		/// <param name="EndIn">Longitud que debe tener la lista para dejar de insertarle <paramref name="content"/>.</param>
		/// <param name="content">Contenido con el que queremos llenar la lista.</param>
		public static void FillStart<T>(this List<T> arr, int EndIn, T content)
		{
			while (arr.Count < EndIn) arr.Insert(0, content);
		}
		/// <summary>
		/// Rellena el final de una lista con <paramref name="content"/> hasta que tenga longitud <paramref name="EndIn"/>.
		/// </summary>
		/// <typeparam name="T">Cualquier tipo.</typeparam>
		/// <param name="arr">Lista de origen.</param>
		/// <param name="EndIn">Longitud que debe tener la lista para dejar de insertarle <paramref name="content"/>.</param>
		/// <param name="content">Contenido con el que queremos llenar la lista.</param>
		public static void FillEnd<T>(this List<T> arr, int EndIn, T content)
		{
			while (arr.Count < EndIn) arr.Add(content);
		}
		#endregion

		#region Conversiones

		/// <inheritdoc/>
		public static string ToString<T>(this IEnumerable<T> arr, bool multiline) =>
			arr.ToString(null, false, multiline);
		/// <inheritdoc/>
		public static string ToString<T>(this IEnumerable<T> arr, char? separator) =>
			arr.ToString(separator, false);
		/// <inheritdoc/>
		public static string ToString<T>(this IEnumerable<T> arr, char? separator, bool showindex) =>
			arr.ToString(separator, showindex, false);

		/// <inheritdoc/>
		public static string ToString<T>(this IEnumerable<T> arr, char? separator, bool showindex, bool multiline)
		{
			T[] newArr = arr.ToArray();
			string dev = "";
			for (int i = 0; i < newArr.Length; i++)
			{
				if (multiline && i > 0)
					dev += "\n";
				if (showindex)
					dev += $"{i}: ";
				dev += $"{newArr[i]}{separator}";
			}
			dev = dev.TrimEnd(separator ?? ' ');
			return dev;
		}
		#endregion
	}
}
