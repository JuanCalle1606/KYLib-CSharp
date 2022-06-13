using System.Collections.Generic;
using KYLib.MathFn;

namespace KYLib.Extensions;

/// <summary>
/// Provee todas las extensiones que tienen que ver con cosas numericas.
/// </summary>
public static class MathExtensions
{

	/// <summary>
	/// Calcula la suma de los elementos de este arreglo.
	/// </summary>
	/// <param name="arr">Arreglo numerico de origen.</param>
	/// <returns>Devuelve la suma de los elementos del arreglo.</returns>
	public static T Sum<T>(this IEnumerable<T> arr) where T : INumber<T> =>
		Mathf.SumOf(arr);

	/// <summary>
	/// Calcula la media de este conjunto de numerico.
	/// </summary>
	/// <param name="arr">Arreglo numerico de origen.</param>
	/// <returns>Devuelve la media del conjunto numerico.</returns>
	public static T Mean<T>(this IEnumerable<T> arr) where T : INumber<T> =>
		Mathf.MeanOf(arr);
}