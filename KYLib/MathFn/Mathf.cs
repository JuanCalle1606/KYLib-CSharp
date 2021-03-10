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
		/// Calcula la media de un conjunto de numeros enteros.
		/// </summary>
		/// <param name="source">Conjunto de numeros de origen.</param>
		/// <returns>Media del conjunto numerico.</returns>
		public static int MeanOf(IEnumerable<int> source)
		{
			int count = source.Count();
			int sum = source.Sum();
			sum /= count;
			return sum;
		}

		/// <summary>
		/// Calcula la media de un conjunto de numeros.
		/// </summary>
		/// <param name="source">Conjunto numerico.</param>
		/// <typeparam name="T">Algun tipo numerico.</typeparam>
		/// <returns>La media del conjunto de datos.</returns>
		public static T MeanOf<T>(IEnumerable<T> source) where T : struct, INumber
		{
			Int count = source.Count();
			INumber sum = source.Sum();
			sum.Div(count);
			return (T)sum;
		}

		/// <summary>
		/// Calcula la media de un conjunto de numeros.
		/// </summary>
		/// <param name="source">Conjunto numerico.</param>
		/// <typeparam name="T">Algun tipo numerico.</typeparam>
		/// <returns>La media del conjunto de datos.</returns>
		public static T Mean<T>(params T[] source) where T : struct, INumber =>
			MeanOf(source);



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

			for (int i = 0; i < power; i++)
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
			int count = chars.Count - 1;
			char res = '0';
			for (int i = count; i >= 0; i--)
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
