
using System.Collections.Generic;
using KYLib.Interfaces;
using KYLib.MathFn;

namespace KYLib.Extensions
{
	/// <summary>
	/// Provee todas las extensiones que tienen que ver con cosas numericas.
	/// </summary>
	public static class MathExtensions
	{
		/// <summary>
		/// Calcula la suma de los elementos de este arreglo.
		/// </summary>
		/// <param name="arr">Arreglo numerico de origen.</param>
		/// <typeparam name="T">Cualquier tipo numerico.</typeparam>
		/// <returns>Devuelve la suma de los elementos del arreglo.</returns>
		public static T Sum<T>(this IEnumerable<INumber> arr) where T : struct, INumber =>
			Mathf.Sum<T>(arr);

		/// <summary>
		/// Calcula la suma de los elementos de este arreglo.
		/// </summary>
		/// <param name="arr">Arreglo numerico de origen.</param>
		/// <returns>Devuelve la suma de los elementos del arreglo.</returns>
		public static Real Sum(this IEnumerable<INumber> arr) =>
			Mathf.Sum(arr);
	}
}