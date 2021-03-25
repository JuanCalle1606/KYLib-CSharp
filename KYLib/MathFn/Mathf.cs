using System;
using System.Collections.Generic;
using System.Linq;
using KYLib.Extensions;
using KYLib.Interfaces;
using xint = KYLib.MathFn.BitArray;

namespace KYLib.MathFn
{
	/// <summary>
	/// Provee funciones matematicas basicas.
	/// </summary>
	public static class Mathf
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
			T dev = new T();
			dev.UpdateValue(div * 100);
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
		/// <param name="source">Conjunto numerico.</param>
		/// <typeparam name="T">Algun tipo numerico.</typeparam>
		/// <returns>La media del conjunto de datos.</returns>
		public static T MeanOf<T>(IEnumerable<INumber> source) where T : struct, INumber
		{
			Int count = source.Count();
			INumber sum = source.Sum<T>();
			sum.Div(count);
			return (T)sum;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static Real MeanOf(IEnumerable<INumber> source) =>
			MeanOf<Real>(source);

		/// <summary>
		/// Calcula la media de un conjunto de numeros.
		/// </summary>
		/// <param name="source">Conjunto numerico.</param>
		/// <typeparam name="T">Algun tipo numerico.</typeparam>
		/// <returns>La media del conjunto de datos.</returns>
		public static T Mean<T>(params T[] source) where T : struct, INumber =>
			MeanOf<T>(source.ToArray<T, INumber>(i => i));

		/// <summary>
		/// Calcula la suma de los elementos de este arreglo.
		/// </summary>
		/// <param name="arr">Arreglo numerico de origen.</param>
		/// <typeparam name="T">Cualquier tipo numerico.</typeparam>
		/// <returns>Devuelve la suma de los elementos del arreglo.</returns>
		public static T Sum<T>(IEnumerable<INumber> arr) where T : struct, INumber
		{
			T sum = default(T);
			foreach (var item in arr)
				sum.Add(item);
			return sum;
		}

		/// <summary>
		/// Calcula la suma de los elementos de este arreglo.
		/// </summary>
		/// <param name="arr">Arreglo numerico de origen.</param>
		/// <returns>Devuelve la suma de los elementos del arreglo.</returns>
		public static Real Sum(IEnumerable<INumber> arr) =>
			arr.Sum<Real>();

		#endregion

		#region Overrides System.Math
		/// <summary>
		/// Devuelve el mayor de dos enteros sin signo.
		/// </summary>
		/// <param name="val1">Primer entero.</param>
		/// <param name="val2">Segundo entero.</param>
		/// <returns>El mayor entre <paramref name="val1"/> y <paramref name="val2"/>, si son iguales se devuelve <paramref name="val1"/>.</returns>
		[Obsolete("La clase BitArray tiene problemas de rendimiento por lo que sera eliminada", true)]
		public static xint Max(xint val1, xint val2) => val1 < val2 ? val2 : val1;

		/// <summary>
		/// Devuelve el menor de dos enteros sin signo.
		/// </summary>
		/// <param name="val1">Primer entero.</param>
		/// <param name="val2">Segundo entero.</param>
		/// <returns>El menor entre <paramref name="val1"/> y <paramref name="val2"/>, si son iguales se devuelve <paramref name="val2"/>.</returns>
		[Obsolete("La clase BitArray tiene problemas de rendimiento por lo que sera eliminada", true)]
		public static xint Min(xint val1, xint val2) => val1 < val2 ? val1 : val2;

		/// <summary>
		/// Devuelve la raiz cuadrada de <paramref name="n"/>.
		/// </summary>
		/// <param name="n">Numero que queremos obtener la raiz cuadrada.</param>
		/// <returns>La raiz cuadrada de <paramref name="n"/>.</returns>
		public static float Sqrt(float n) => (float)Math.Sqrt(n);

		#endregion

		#region  Trabajo con cadenas.
		/// <summary>
		/// Devuelve la <paramref name="power"/> potencia de 2.
		/// </summary>
		/// <param name="power">Potencia a la que se va a elevar el 2.</param>
		/// <returns>2 elevado a la <paramref name="power"/> en formato de cadenas.</returns>
		public static string Pow2To(int power)
		{
			if (power < 0)
				throw new ArgumentOutOfRangeException(nameof(power), power, "La potencia no puede ser negativa en esta función");
			string dev = "1";

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
			List<char> chars = n.ToCharArray().ToList();
			List<char> output = new List<char>();
			Int count = chars.Count - 1;
			char res = '0';
			for (Int i = count; i >= 0; i--)
				output.Insert(0, Mult2core(chars[i], res, out res));
			if (res == '1')
				output.Insert(0, res);
			return new string(output.ToArray());
		}

		private static char Mult2core(char n, char res, out char outRes)
		{
			byte nr = byte.Parse($"{n}");
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
