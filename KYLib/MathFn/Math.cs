using System;
using KYLib.Interfaces;

namespace KYLib.MathFn
{
	// Aqui se guardan los metodos que proveen implementacion de los metodos de Math.
	partial class Mathf
	{
		#region Root

		/// <summary>
		/// Calcula la raiz <paramref name="nbase"/> de <paramref name="num"/>.
		/// </summary>
		/// <param name="num">Numero al que queremos calular la raiz.</param>
		/// <param name="nbase">Base de la raiz, 2 es raiz cuadrada, 3 es raiz cubica, etc...</param>
		/// <typeparam name="T">Tipo numerico que debe ser retornado, si no se especifica se retornara un <see cref="Real"/>.</typeparam>
		/// <returns>Devuelve la raiz <paramref name="nbase"/> positiva de <paramref name="num"/>. Si <paramref name="num"/> es un valor negativo se devolvera infinito o NaN.</returns>
		public static T Root<T>(INumber num, INumber nbase) where T : struct, INumber
		{
			var dev = default(T);
			double rnum = num.ToDouble(null), rbase = nbase.ToDouble(null), math;
			if (rbase == 2) math = Math.Sqrt(rnum);
			else if (rbase == 3) math = Math.Cbrt(rnum);
			else math = Math.Pow(rnum, 1 / rbase);

			dev.UpdateValue(math);
			return dev;
		}

		/// <summary>
		/// Calcula la raiz <paramref name="nbase"/> de <paramref name="num"/>.
		/// </summary>
		/// <param name="num">Numero al que queremos calular la raiz.</param>
		/// <param name="nbase">Base de la raiz, 2 es raiz cuadrada, 3 es raiz cubica, etc...</param>
		/// <returns>Devuelve la raiz <paramref name="nbase"/> positiva de <paramref name="num"/>. Si <paramref name="num"/> es un valor negativo se devolvera infinito o NaN.</returns>
		public static Real Root(INumber num, INumber nbase) =>
			Root<Real>(num, nbase);

		/// <summary>
		/// Calcula la raiz <paramref name="nbase"/> de <paramref name="num"/>.
		/// </summary>
		/// <param name="num">Numero al que queremos calular la raiz.</param>
		/// <param name="nbase">Base de la raiz, 2 es raiz cuadrada, 3 es raiz cubica, etc...</param>
		/// <typeparam name="T">Tipo numerico de entrada, tanto el numero como la base deben ser del mismo tipo.</typeparam>
		/// <typeparam name="TOut">Tipo numerico que debe ser retornado, si no se especifica se retornara un <see cref="Real"/>.</typeparam>
		/// <returns>Devuelve la raiz <paramref name="nbase"/> positiva de <paramref name="num"/>. Si <paramref name="num"/> es un valor negativo se devolvera infinito o NaN.</returns>
		public static TOut Root<T, TOut>(T num, T nbase)
		where T : struct, INumber
		where TOut : struct, INumber =>
			Root<TOut>(num, nbase);

		/// <summary>
		/// Calcula la raiz <paramref name="nbase"/> de <paramref name="num"/>.
		/// </summary>
		/// <param name="num">Numero al que queremos calular la raiz.</param>
		/// <param name="nbase">Base de la raiz, 2 es raiz cuadrada, 3 es raiz cubica, etc...</param>
		/// <typeparam name="T">Tipo numerico que se maneja, tanto el numero como la base deben ser del mismo tipo.</typeparam>
		/// <returns>Devuelve la raiz <paramref name="nbase"/> positiva de <paramref name="num"/>. Si <paramref name="num"/> es un valor negativo se devolvera infinito o NaN.</returns>
		public static T Root<T>(T num, T nbase) where T : struct, INumber =>
			Root<T, T>(num, nbase);

		#endregion Root

		#region Pow

		/// <summary>
		/// Calcula la potencia <paramref name="nbase"/> de <paramref name="num"/>.
		/// </summary>
		/// <param name="num">Numero al que queremos calular la potencia.</param>
		/// <param name="nbase">Base de la potencia, 2 es el cuadrado, 3 es el cubo, etc...</param>
		/// <typeparam name="T">Tipo numerico que debe ser retornado, si no se especifica se retornara un <see cref="Real"/>.</typeparam>
		/// <returns>Devuelve la potencia <paramref name="nbase"/> de <paramref name="num"/>.</returns>
		public static T Pow<T>(INumber num, INumber nbase) where T : struct, INumber
		{
			var dev = default(T);
			double rnum = num.ToDouble(null), rbase = nbase.ToDouble(null);

			var math = Math.Pow(rnum, rbase);

			dev.UpdateValue(math);
			return dev;
		}

		/// <summary>
		/// Calcula la potencia <paramref name="nbase"/> de <paramref name="num"/>.
		/// </summary>
		/// <param name="num">Numero al que queremos calular la potencia.</param>
		/// <param name="nbase">Base de la potencia, 2 es el cuadrado, 3 es el cubo, etc...</param>
		/// <returns>Devuelve la potencia <paramref name="nbase"/> de <paramref name="num"/>.</returns>
		public static Real Pow(INumber num, INumber nbase) =>
			Pow<Real>(num, nbase);

		/// <summary>
		/// Calcula la potencia <paramref name="nbase"/> de <paramref name="num"/>.
		/// </summary>
		/// <param name="num">Numero al que queremos calular la potencia.</param>
		/// <param name="nbase">Base de la potencia, 2 es el cuadrado, 3 es el cubo, etc...</param>
		/// <typeparam name="T">Tipo numerico de entrada, tanto el numero como la base deben ser del mismo tipo.</typeparam>
		/// <typeparam name="TOut">Tipo numerico que debe ser retornado, si no se especifica se retornara un <see cref="Real"/>.</typeparam>
		/// <returns>Devuelve la potencia <paramref name="nbase"/> de <paramref name="num"/>.</returns>
		public static TOut Pow<T, TOut>(T num, T nbase)
		where T : struct, INumber
		where TOut : struct, INumber =>
			Pow<TOut>(num, nbase);

		/// <summary>
		/// Calcula la potencia <paramref name="nbase"/> de <paramref name="num"/>.
		/// </summary>
		/// <param name="num">Numero al que queremos calular la potencia.</param>
		/// <param name="nbase">Base de la potencia, 2 es el cuadrado, 3 es el cubo, etc...</param>
		/// <typeparam name="T">Tipo numerico que se maneja, tanto el numero como la base deben ser del mismo tipo.</typeparam>
		/// <returns>Devuelve la potencia <paramref name="nbase"/> de <paramref name="num"/>.</returns>
		public static T Pow<T>(T num, T nbase) where T : struct, INumber =>
			Pow<T, T>(num, nbase);

		#endregion Pow
	}
}