using System;
using System.Collections.Generic;
using System.Linq;
using KYLib.Interfaces;

namespace KYLib.MathFn
{
	/// <summary>
	/// Provee funciones matematicas basicas.
	/// </summary>
	public static partial class Mathf
	{
		#region Utilidades

		/// <summary>
		/// Calcula el porcentaje de <paramref name="n1"/> respecto a <paramref name="n2"/>.
		/// </summary>
		/// <param name="n1">Primer numero, este valor es la cantidad a evaluar.</param>
		/// <param name="n2">Segundo numero, este valor es el limite, es decir, el 100%.</param>
		/// <typeparam name="T">Tipo numerico que se retorna, este tipo debe ser especificado explitamente o se usara la sobrecarga que devuelve <see cref="Int"/>.</typeparam>
		/// <returns>Devuelve el porcentaje de <paramref name="n1"/> con respecto a <paramref name="n2"/>.</returns>
		public static T Percent<T>(INumber n1, INumber n2) where T : struct, INumber
		{
			Real div = n1.ToDouble(null) / n2.ToDouble(null);
			T dev = new();
			dev.UpdateValue(div);
			return dev;
		}

		/// <summary>
		/// Calcula el porcentaje de <paramref name="n1"/> respecto a <paramref name="n2"/>.
		/// </summary>
		/// <param name="n1">Primer numero, este valor es la cantidad a evaluar.</param>
		/// <param name="n2">Segundo numero, este valor es el limite, es decir, el 100%.</param>
		/// <returns>Devuelve el porcentaje de <paramref name="n1"/> con respecto a <paramref name="n2"/>.</returns>
		public static Int Percent(INumber n1, INumber n2) =>
			Percent<Int>(n1, n2);

		/// <summary>
		/// Calcula la media de un conjunto de numeros.
		/// </summary>
		/// <param name="nums">Conjunto numerico.</param>
		/// <typeparam name="T">Tipo numerico que se quiere de salida.</typeparam>
		/// <returns>La media del conjunto de datos.</returns>
		public static T MeanOf<T>(IEnumerable<INumber> nums) where T : struct, INumber
		{
			Int count = nums.Count();
			var sum = SumOf<T>(nums);
			sum.Div(count);
			return sum;
		}

		/// <summary>
		/// Calcula la media de un conjunto de numeros.
		/// </summary>
		/// <param name="nums">Conjunto numerico.</param>
		/// <returns>La media del conjunto de datos.</returns>
		public static Real MeanOf(IEnumerable<INumber> nums) =>
			MeanOf<Real>(nums);

		/// <summary>
		/// Calcula la media de un conjunto de numeros.
		/// </summary>
		/// <param name="nums">Conjunto numerico.</param>
		/// <typeparam name="T">Cualquier tipo numerico.</typeparam>
		/// <typeparam name="TOut">Tipo numerico que se quiere de salida.</typeparam>
		/// <returns>La media del conjunto de datos.</returns>
		public static TOut MeanOf<T, TOut>(IEnumerable<T> nums)
		where T : struct, INumber
		where TOut : struct, INumber
		{
			Int count = nums.Count();
			var sum = SumOf<T, TOut>(nums);
			sum.Div(count);
			return sum;
		}

		/// <summary>
		/// Calcula la media de un conjunto de numeros.
		/// </summary>
		/// <param name="nums">Conjunto numerico.</param>
		/// <typeparam name="T">Cualquier tipo numerico.</typeparam>
		/// <returns>La media del conjunto de datos.</returns>
		public static T MeanOf<T>(IEnumerable<T> nums) where T : struct, INumber =>
			MeanOf<T, T>(nums);

		/// <summary>
		/// Calcula la media de un conjunto de numeros.
		/// </summary>
		/// <param name="nums">Conjunto numerico.</param>
		/// <typeparam name="T">Tipo numerico que se quiere de salida.</typeparam>
		/// <returns>La media del conjunto de datos.</returns>
		public static T Mean<T>(params INumber[] nums) where T : struct, INumber =>
			MeanOf<T>(nums);

		/// <summary>
		/// Calcula la media de un conjunto de numeros.
		/// </summary>
		/// <param name="nums">Conjunto numerico.</param>
		/// <returns>La media del conjunto de datos.</returns>
		public static Real Mean(params INumber[] nums) =>
			MeanOf(nums);

		/// <summary>
		/// Calcula la media de un conjunto de numeros.
		/// </summary>
		/// <param name="nums">Conjunto numerico.</param>
		/// <typeparam name="T">Cualquier tipo numerico.</typeparam>
		/// <typeparam name="TOut">Tipo numerico que se quiere de salida.</typeparam>
		/// <returns>La media del conjunto de datos.</returns>
		public static TOut Mean<T, TOut>(params T[] nums)
		where T : struct, INumber
		where TOut : struct, INumber =>
			MeanOf<T, TOut>(nums);

		/// <summary>
		/// Calcula la media de un conjunto de numeros.
		/// </summary>
		/// <param name="nums">Conjunto numerico.</param>
		/// <typeparam name="T">Cualquier tipo numerico.</typeparam>
		/// <returns>La media del conjunto de datos.</returns>
		public static T Mean<T>(params T[] nums) where T : struct, INumber =>
			MeanOf(nums);

		/// <summary>
		/// Calcula la suma de los elementos de un arreglo numerico.
		/// </summary>
		/// <param name="arr">Arreglo numerico de origen.</param>
		/// <typeparam name="T">Tipo numerico que se quiere de salida.</typeparam>
		/// <returns>Devuelve la suma de los elementos del arreglo.</returns>
		public static T SumOf<T>(IEnumerable<INumber> arr) where T : struct, INumber
		{
			T sum = default;
			foreach (var item in arr)
				sum.Add(item);
			return sum;
		}

		/// <summary>
		/// Calcula la suma de los elementos de un arreglo.
		/// </summary>
		/// <param name="arr">Arreglo numerico de origen.</param>
		/// <returns>Devuelve la suma de los elementos del arreglo.</returns>
		public static Real SumOf(IEnumerable<INumber> arr) =>
			SumOf<Real>(arr);

		/// <summary>
		/// Calcula la suma de los elementos de un arreglo de numeros de un tipo.
		/// </summary>
		/// <param name="arr">Arreglo numerico de origen.</param>
		/// <typeparam name="T">Cualquier tipo numerico.</typeparam>
		/// <typeparam name="TOut">Tipo numerico que se quiere de salida.</typeparam>
		/// <returns>Devuelve la suma de los elemetnos de un arreglo</returns>
		public static TOut SumOf<T, TOut>(IEnumerable<T> arr)
		where T : struct, INumber
		where TOut : struct, INumber
		{
			TOut sum = default;
			foreach (var item in arr)
				sum.Add(item);
			return sum;
		}

		/// <summary>
		/// Calcula la suma de los elementos de un arreglo de numeros de un tipo.
		/// </summary>
		/// <param name="arr">Arreglo numerico de origen.</param>
		/// <typeparam name="T">Cualquier tipo numerico.</typeparam>
		/// <returns>Devuelve la suma de los elemetnos de un arreglo</returns>
		public static T SumOf<T>(IEnumerable<T> arr) where T : struct, INumber =>
			SumOf<T, T>(arr);

		/// <summary>
		/// Calcula la suma de los elementos de un arreglo numerico.
		/// </summary>
		/// <param name="nums">Arreglo numerico de origen.</param>
		/// <typeparam name="T">Tipo numerico que se quiere de salida.</typeparam>
		/// <returns>Devuelve la suma de los elementos del arreglo.</returns>
		public static T Sum<T>(params INumber[] nums) where T : struct, INumber =>
			SumOf<T>(nums);

		/// <summary>
		/// Calcula la suma de los elementos de un arreglo.
		/// </summary>
		/// <param name="nums">Arreglo numerico de origen.</param>
		/// <returns>Devuelve la suma de los elementos del arreglo.</returns>
		public static Real Sum(params INumber[] nums) =>
			Sum<Real>(nums);

		/// <summary>
		/// Calcula la suma de los elementos de un arreglo de numeros de un tipo.
		/// </summary>
		/// <param name="nums">Arreglo numerico de origen.</param>
		/// <typeparam name="T">Cualquier tipo numerico.</typeparam>
		/// <typeparam name="TOut">Tipo numerico que se quiere de salida.</typeparam>
		/// <returns>Devuelve la suma de los elemetnos de un arreglo</returns>
		public static TOut Sum<T, TOut>(params T[] nums)
		where T : struct, INumber where TOut : struct, INumber =>
			SumOf<T, TOut>(nums);

		/// <summary>
		/// Calcula la suma de los elementos de un arreglo de numeros de un tipo.
		/// </summary>
		/// <param name="nums">Arreglo numerico de origen.</param>
		/// <typeparam name="T">Cualquier tipo numerico.</typeparam>
		/// <returns>Devuelve la suma de los elemetnos de un arreglo</returns>
		public static T Sum<T>(params T[] nums) where T : struct, INumber =>
			SumOf<T>(nums);

		#endregion

		#region  Trabajo con cadenas.
		/// <summary>
		/// Devuelve la <paramref name="power"/> potencia de 2.
		/// </summary>
		/// <param name="power">Potencia a la que se va a elevar el 2.</param>
		/// <returns>2 elevado a la <paramref name="power"/> en formato de cadenas.</returns>
		public static string Pow2To(Int power)
		{
			if (power < 0)
				throw new ArgumentOutOfRangeException(nameof(power), power, "La potencia no puede ser negativa en esta función");
			var dev = "1";

			for (Int i = 0; i < power; i++)
				dev = Mult2(dev);
			return dev;
		}

		/// <summary>
		/// Multiplica un numero por dos.
		/// </summary>
		/// <remarks>
		/// Debe tener en cuenta que esta función no intenta convertir la cadena en un numero si no que directamente interpreta cada caracter como un numero por separado.
		/// </remarks>
		/// <param name="n">Una cadena de texto que representa un numero.</param>
		/// <returns>El numero <paramref name="n"/> multiplicado por dos.</returns>
		public static string Mult2(string n)
		{
			var chars = n.ToCharArray().ToList();
			List<char> output = new();
			Int count = chars.Count - 1;
			var res = '0';
			for (var i = count; i >= 0; i--)
				output.Insert(0, Mult2core(chars[i], res, out res));
			if (res == '1')
				output.Insert(0, res);
			return new string(output.ToArray());
		}

		private static char Mult2core(char n, char res, out char outRes)
		{
			var nr = byte.Parse($"{n}");
			byte temp;
			if (nr == 0 || nr == 5)
				temp = 0;
			else if (nr == 1 || nr == 6)
				temp = 2;
			else if (nr == 2 || nr == 7)
				temp = 4;
			else if (nr == 3 || nr == 8)
				temp = 6;
			else
				temp = 8;
			temp += res == '1' ? (byte)1 : (byte)0;
			if (nr < 5)
				outRes = '0';
			else
				outRes = '1';
			return char.Parse($"{temp}");

		}
		#endregion
	}
}
